using Microsoft.EntityFrameworkCore.Migrations;

namespace MintGarage.Migrations
{
    public partial class ConsultationForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeService",
                table: "ConsultationForm");

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "ConsultationForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "ConsultationForm");

            migrationBuilder.AddColumn<string>(
                name: "TypeService",
                table: "ConsultationForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
