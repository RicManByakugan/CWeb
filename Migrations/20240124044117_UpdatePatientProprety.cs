using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientProprety : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Accueil",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accueil",
                table: "Patient");
        }
    }
}
