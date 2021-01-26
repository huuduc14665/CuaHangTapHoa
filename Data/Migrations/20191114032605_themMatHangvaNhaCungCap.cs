using Microsoft.EntityFrameworkCore.Migrations;

namespace CuaHangTapHoa.Data.Migrations
{
    public partial class themMatHangvaNhaCungCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoaiMatHangs",
                table: "LoaiMatHangs");

            migrationBuilder.RenameTable(
                name: "LoaiMatHangs",
                newName: "LoaiMatHang");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoaiMatHang",
                table: "LoaiMatHang",
                column: "MaLMH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoaiMatHang",
                table: "LoaiMatHang");

            migrationBuilder.RenameTable(
                name: "LoaiMatHang",
                newName: "LoaiMatHangs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoaiMatHangs",
                table: "LoaiMatHangs",
                column: "MaLMH");
        }
    }
}
