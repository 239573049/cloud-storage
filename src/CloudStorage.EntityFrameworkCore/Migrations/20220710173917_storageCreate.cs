using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudStorage.EntityFrameworkCore.Migrations
{
    public partial class storageCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Storage",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "Account", "BriefIntroduction", "CloudStorageRoot", "ConcurrencyStamp", "HeadPortraits", "IsDeleted", "Name", "Password", "Sex", "Status", "WeChatOpenId" },
                values: new object[] { new Guid("5e57cebf-2d4f-48cb-8e0d-141a9e2bca41"), "admin", null, "D:\\NetCore\\CloudStorage\\src\\CloudStorage.HttpApi.Host\\bin\\Debug\\net6.0\\./wwwroot/CloudStorage\\admin", "5fc828fcd6fd4577ac7c4a4c83c52f3d", null, false, "admin", "admin", 0, 0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: new Guid("5e57cebf-2d4f-48cb-8e0d-141a9e2bca41"));

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Storage");
        }
    }
}
