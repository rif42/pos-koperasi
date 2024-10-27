using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos_koperasi.Migrations
{
    /// <inheritdoc />
    public partial class deskripsi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Harga",
                table: "Barang",
                type: "decimal(18, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Deskripsi",
                table: "Barang",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deskripsi",
                table: "Barang");

            migrationBuilder.AlterColumn<decimal>(
                name: "Harga",
                table: "Barang",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 3)");
        }
    }
}
