using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookChangelog.API.Infrastructure.Migrations
{
    public partial class AddAuthorNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_author_name",
                table: "author",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_author_name",
                table: "author");
        }
    }
}
