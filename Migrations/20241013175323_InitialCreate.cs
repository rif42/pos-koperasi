using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_koperasi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaBarang = table.Column<string>(type: "TEXT", nullable: true),
                    TerakhirUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Harga = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barang", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barang");
        }
    }
}
