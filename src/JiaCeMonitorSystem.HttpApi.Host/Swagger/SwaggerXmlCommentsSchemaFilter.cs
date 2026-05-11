using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JiaCeMonitorSystem.Swagger
{
    /// <summary>
    /// 自定义 Swagger XML 注释 SchemaFilter
    /// 支持从 XML 注释中读取 DTO 类型的 Summary 和属性注释
    /// </summary>
    public class SwaggerXmlCommentsSchemaFilter : ISchemaFilter
    {
        private readonly List<XPathNavigator> _xmlNavigators;

        public SwaggerXmlCommentsSchemaFilter(IEnumerable<string> xmlFilePaths)
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
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type == null) return;

            // 设置类型的 Description（来自类型的 XML 注释）
            var typeNode = GetTypeNode(type);
            if (typeNode != null && string.IsNullOrEmpty(schema.Description))
            {
                var summaryNode = typeNode.SelectSingleNode("summary");
                if (summaryNode != null)
                {
                    schema.Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
                }
            }

            // 为每个属性设置注释
            if (schema.Properties != null && typeNode != null)
            {
                // 从 members 节点中查找该类型的所有属性注释
                var membersNode = typeNode.SelectSingleNode("..");
                if (membersNode != null)
                {
                    var typeFullName = type.FullName;
                    if (typeFullName != null)
                    {
                        var propertyPrefix = $"P:{typeFullName}.";
                        var propertyNodes = membersNode.Select($"member[starts-with(@name, '{propertyPrefix}')]");
                        foreach (XPathNavigator propNode in propertyNodes)
                        {
                            var memberName = propNode.GetAttribute("name", "");
                            var propName = memberName.Substring(propertyPrefix.Length);
                            if (string.IsNullOrEmpty(propName)) continue;

                            // 处理属性名大小写转换（Swagger 默认使用 camelCase）
                            var schemaPropKey = schema.Properties.Keys.FirstOrDefault(k =>
                                string.Equals(k, propName, StringComparison.OrdinalIgnoreCase));

                            if (schemaPropKey != null && string.IsNullOrEmpty(schema.Properties[schemaPropKey].Description))
                            {
                                var summaryNode = propNode.SelectSingleNode("summary");
                                if (summaryNode != null)
                                {
                                    schema.Properties[schemaPropKey].Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
                                }
                            }
                        }
                    }
                }
            }
        }

        private XPathNavigator? GetTypeNode(Type type)
        {
            var memberName = XmlCommentsNodeNameHelper.GetMemberNameForType(type);
            foreach (var navigator in _xmlNavigators)
            {
                var node = navigator.SelectSingleNode($"/doc/members/member[@name='{memberName}']");
                if (node != null) return node;
            }
            return null;
        }
    }
}
