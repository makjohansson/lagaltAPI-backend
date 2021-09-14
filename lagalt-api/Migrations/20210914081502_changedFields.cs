using Microsoft.EntityFrameworkCore.Migrations;

namespace lagalt_api.Migrations
{
    public partial class changedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "Fields",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "Fields");

            migrationBuilder.AddColumn<int>(
                name: "FieldType",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
