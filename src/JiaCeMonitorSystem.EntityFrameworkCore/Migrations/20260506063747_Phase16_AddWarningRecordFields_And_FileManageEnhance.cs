using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiaCeMonitorSystem.Migrations
{
    /// <inheritdoc />
    public partial class Phase16_AddWarningRecordFields_And_FileManageEnhance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectorName",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                comment: "采集人姓名（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "DataRemark",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "数据备注（冗余）");

            migrationBuilder.AddColumn<int>(
                name: "DataState",
                schema: "public",
                table: "JC_WarningRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "数据状态（冗余）");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarningTime",
                schema: "public",
                table: "JC_WarningRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "预警时间");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectorName",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "DataRemark",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "DataState",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "WarningTime",
                schema: "public",
                table: "JC_WarningRecords");
        }
    }
}
