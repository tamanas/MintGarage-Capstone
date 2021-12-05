using Microsoft.EntityFrameworkCore.Migrations;

namespace MintGarage.Migrations
{
    public partial class footerSocialMediaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMediaLogo",
                table: "FooterSocialMedias");

            migrationBuilder.AddColumn<string>(
                name: "SocialMediaIcon",
                table: "FooterSocialMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMediaIcon",
                table: "FooterSocialMedias");

            migrationBuilder.AddColumn<string>(
                name: "SocialMediaLogo",
                table: "FooterSocialMedias",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
