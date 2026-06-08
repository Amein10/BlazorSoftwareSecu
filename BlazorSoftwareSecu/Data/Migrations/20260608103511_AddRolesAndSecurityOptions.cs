using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSoftwareSecu.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndSecurityOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPR",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPR",
                table: "AspNetUsers");
        }
    }
}
