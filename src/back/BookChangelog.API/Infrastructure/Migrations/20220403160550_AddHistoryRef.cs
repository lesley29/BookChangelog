using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookChangelog.API.Infrastructure.Migrations
{
    public partial class AddHistoryRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "fk_book_change_history_books_book_id",
                table: "book_change_history",
                column: "book_id",
                principalTable: "book",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_change_history_books_book_id",
                table: "book_change_history");
        }
    }
}
