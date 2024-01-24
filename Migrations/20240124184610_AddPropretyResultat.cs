using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddPropretyResultat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultatConsultation",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultatConsultation",
                table: "Patient");
        }
    }
}
