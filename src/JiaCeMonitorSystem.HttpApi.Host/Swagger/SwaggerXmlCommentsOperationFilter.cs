using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JiaCeMonitorSystem.Swagger
{
    /// <summary>
    /// 自定义 Swagger XML 注释 OperationFilter
    /// 支持从应用服务接口 XML 注释中读取 Summary 和 Param 描述
    /// </summary>
    public class SwaggerXmlCommentsOperationFilter : IOperationFilter
    {
        private readonly List<XPathNavigator> _xmlNavigators;
        private readonly Dictionary<string, Type> _appServiceInterfaces;

        public SwaggerXmlCommentsOperationFilter(IEnumerable<string> xmlFilePaths)
        {
            _xmlNavigators = new List<XPathNavigator>();
            foreach (var xmlPath in xmlFilePaths.Where(File.Exists))
            {
                try
                {
                    var doc = new XPathDocument(xmlPath);
                    _xmlNavigators.Add(doc.CreateNavigator());
                }
                catch
                {
                    // 忽略无法解析的 XML 文件
                }
            }

            // 缓存所有应用服务接口类型，用于快速查找
            _appServiceInterfaces = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes().Where(t => t.IsInterface && t.Name.EndsWith("AppService")))
                    {
                        var controllerName = type.Name.Substring(1).Replace("AppService", "");
                        _appServiceInterfaces[controllerName] = type;
                    }
                }
                catch
                {
                    // 忽略反射加载失败的程序集
                }
            }
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodInfo = context.MethodInfo;
            if (methodInfo == null) return;

            // 1. 先尝试直接从 MethodInfo 获取 XML 注释（适用于手动编写的控制器）
            var methodNode = GetMethodNode(methodInfo);
            if (methodNode == null)
            {
                // 2. 尝试从应用服务接口获取 XML 注释（适用于 ABP 传统控制器）
                var interfaceMethod = TryGetAppServiceInterfaceMethod(methodInfo);
                if (interfaceMethod != null)
                {
                    methodNode = GetMethodNode(interfaceMethod);
                }
            }

            if (methodNode == null) return;

            // 设置 Summary
            var summaryNode = methodNode.SelectSingleNode("summary");
            if (summaryNode != null && string.IsNullOrEmpty(operation.Summary))
            {
                operation.Summary = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
            }

            // 设置 Remarks
            var remarksNode = methodNode.SelectSingleNode("remarks");
            if (remarksNode != null && string.IsNullOrEmpty(operation.Description))
            {
                operation.Description = XmlCommentsTextHelper.Humanize(remarksNode.InnerXml);
            }

            // 设置参数注释
            var paramNodes = methodNode.Select("param");
            foreach (XPathNavigator paramNode in paramNodes)
            {
                var paramName = paramNode.GetAttribute("name", "");
                var param = operation.Parameters?.FirstOrDefault(p => p.Name == paramName);
                if (param != null && string.IsNullOrEmpty(param.Description))
                {
                    param.Description = XmlCommentsTextHelper.Humanize(paramNode.InnerXml);
                }

                // 也尝试匹配请求体中的属性
                if (operation.RequestBody?.Content != null)
                {
                    foreach (var content in operation.RequestBody.Content.Values)
                    {
                        if (content.Schema?.Properties != null && content.Schema.Properties.TryGetValue(paramName, out var propSchema))
                        {
                            if (string.IsNullOrEmpty(propSchema.Description))
                            {
                                propSchema.Description = XmlCommentsTextHelper.Humanize(paramNode.InnerXml);
                            }
                        }
                    }
                }
            }

            // 设置返回值注释
            var returnsNode = methodNode.SelectSingleNode("returns");
            if (returnsNode != null && operation.Responses.TryGetValue("200", out var successResponse))
            {
                if (string.IsNullOrEmpty(successResponse.Description) || successResponse.Description == "Success")
                {
                    successResponse.Description = XmlCommentsTextHelper.Humanize(returnsNode.InnerXml);
                }
            }
        }

        private XPathNavigator? GetMethodNode(MethodInfo methodInfo)
        {
            var memberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(methodInfo);
            return FindMemberNode(memberName);
        }

        private XPathNavigator? FindMemberNode(string memberName)
        {
            foreach (var navigator in _xmlNavigators)
            {
                var node = navigator.SelectSingleNode($"/doc/members/member[@name='{memberName}']");
                if (node != null) return node;
            }
            return null;
        }

        private MethodInfo? TryGetAppServiceInterfaceMethod(MethodInfo controllerMethod)
        {
            try
            {
                // 获取控制器类型
                var controllerType = controllerMethod.DeclaringType;
                if (controllerType == null) return null;

                // 从控制器名称推断接口名称
                // ABP 约定：IPointAppService -> PointController
                var controllerName = controllerType.Name.Replace("Controller", "");
                if (string.IsNullOrEmpty(controllerName)) return null;

                if (!_appServiceInterfaces.TryGetValue(controllerName, out var interfaceType))
                    return null;

                // 在接口中查找同名方法（考虑 Async 后缀）
                var methodName = controllerMethod.Name;
                var interfaceMethods = interfaceType.GetMethods()
                    .Where(m => m.Name == methodName || m.Name == methodName + "Async" || methodName == m.Name + "Async")
                    .ToList();

                if (interfaceMethods.Count == 1)
                    return interfaceMethods[0];

                // 如果有多个重载，尝试按参数数量匹配
                var parameterTypes = controllerMethod.GetParameters().Select(p => p.ParameterType).ToArray();
                return interfaceMethods.FirstOrDefault(m =>
                {
                    var intfParams = m.GetParameters();
                    if (intfParams.Length != parameterTypes.Length) return false;
                    for (int i = 0; i < intfParams.Length; i++)
                    {
                        if (!IsParameterTypeCompatible(intfParams[i].ParameterType, parameterTypes[i]))
                            return false;
                    }
                    return true;
                });
            }
            catch
            {
                return null;
            }
        }

        private static bool IsParameterTypeCompatible(Type interfaceParam, Type controllerParam)
        {
            // 允许 Nullable<T> 与 T 的匹配
            if (interfaceParam == controllerParam) return true;
            if (Nullable.GetUnderlyingType(interfaceParam) == controllerParam) return true;
            if (Nullable.GetUnderlyingType(controllerParam) == interfaceParam) return true;
            return interfaceParam.IsAssignableFrom(controllerParam) || controllerParam.IsAssignableFrom(interfaceParam);
        }
    }
}
