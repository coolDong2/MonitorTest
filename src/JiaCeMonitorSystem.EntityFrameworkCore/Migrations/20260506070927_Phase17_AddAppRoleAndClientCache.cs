using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiaCeMonitorSystem.Migrations
{
    /// <inheritdoc />
    public partial class Phase17_AddAppRoleAndClientCache : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JC_AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "所属公司名称"),
                    EnCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "角色编号"),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "角色名称"),
                    Category = table.Column<int>(type: "integer", nullable: false, comment: "角色类型"),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "角色类型名称"),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false, comment: "允许编辑"),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "允许删除"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "描述"),
                    PermissionButtonIds = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "权限按钮ID"),
                    PermissionFieldsIds = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "权限字段ID"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_AppRoles", x => x.Id);
                },
                comment: "业务角色");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_CompanyId",
                table: "JC_AppRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_EnCode",
                table: "JC_AppRoles",
                column: "EnCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JC_AppRoles");
        }
    }
}
