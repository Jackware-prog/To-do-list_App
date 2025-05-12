using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do_list_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    listId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.listId);
                });

            migrationBuilder.CreateTable(
                name: "ListItem",
                columns: table => new
                {
                    listItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    listId = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dueDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItem", x => x.listItemId);
                    table.ForeignKey(
                        name: "FK_ListItem_List_listId",
                        column: x => x.listId,
                        principalTable: "List",
                        principalColumn: "listId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_listId",
                table: "ListItem",
                column: "listId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListItem");

            migrationBuilder.DropTable(
                name: "List");
        }
    }
}
