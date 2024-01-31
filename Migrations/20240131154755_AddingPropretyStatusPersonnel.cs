using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddingPropretyStatusPersonnel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Personnel");
        }
    }
}
