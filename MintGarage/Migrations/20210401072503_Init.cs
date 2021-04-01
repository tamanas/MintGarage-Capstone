using Microsoft.EntityFrameworkCore.Migrations;

namespace MintGarage.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationForm_TypeService_ServiceID",
                table: "ConsultationForm");

            migrationBuilder.DropTable(
                name: "TypeService");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationForm_ServiceID",
                table: "ConsultationForm");

            migrationBuilder.DropColumn(
                name: "ServiceID",
                table: "ConsultationForm");

            migrationBuilder.AddColumn<string>(
                name: "TypeService",
                table: "ConsultationForm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeService",
                table: "ConsultationForm");

            migrationBuilder.AddColumn<int>(
                name: "ServiceID",
                table: "ConsultationForm",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeService",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeService", x => x.ServiceID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationForm_ServiceID",
                table: "ConsultationForm",
                column: "ServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationForm_TypeService_ServiceID",
                table: "ConsultationForm",
                column: "ServiceID",
                principalTable: "TypeService",
                principalColumn: "ServiceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
