using Microsoft.EntityFrameworkCore.Migrations;

namespace CuaHangTapHoa.Data.Migrations
{
    public partial class suaMatHang_xoaNCCkhoiMatHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatHangs_NhaCungCaps_MaNCC",
                table: "MatHangs");

            migrationBuilder.DropIndex(
                name: "IX_MatHangs_MaNCC",
                table: "MatHangs");

            migrationBuilder.DropColumn(
                name: "MaNCC",
                table: "MatHangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaNCC",
                table: "MatHangs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MatHangs_MaNCC",
                table: "MatHangs",
                column: "MaNCC");

            migrationBuilder.AddForeignKey(
                name: "FK_MatHangs_NhaCungCaps_MaNCC",
                table: "MatHangs",
                column: "MaNCC",
                principalTable: "NhaCungCaps",
                principalColumn: "MaNCC",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
