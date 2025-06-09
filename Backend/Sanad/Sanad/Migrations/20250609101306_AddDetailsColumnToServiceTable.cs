using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanad.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailsColumnToServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Services");
        }
    }
}
