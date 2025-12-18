using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeckBuilder.Migrations
{
    /// <inheritdoc />
    public partial class InitDecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    IdDeck = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Format = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Visibility = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.IdDeck);
                });

            migrationBuilder.CreateTable(
                name: "DeckEntries",
                columns: table => new
                {
                    CardScryfallId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Section = table.Column<int>(type: "integer", nullable: false),
                    IdDeck = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckEntries", x => new { x.IdDeck, x.CardScryfallId, x.Section });
                    table.ForeignKey(
                        name: "FK_DeckEntries_Decks_IdDeck",
                        column: x => x.IdDeck,
                        principalTable: "Decks",
                        principalColumn: "IdDeck",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckEntries_CardScryfallId",
                table: "DeckEntries",
                column: "CardScryfallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckEntries");

            migrationBuilder.DropTable(
                name: "Decks");
        }
    }
}
