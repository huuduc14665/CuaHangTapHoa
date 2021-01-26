using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuaHangTapHoa.Data.Migrations
{
    public partial class themMatHangvaNhaCungCap_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    MaNCC = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNCC = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false),
                    DiaChi = table.Column<string>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "MatHangs",
                columns: table => new
                {
                    MaMH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenMH = table.Column<string>(nullable: false),
                    MaLMH = table.Column<int>(nullable: false),
                    MaNCC = table.Column<int>(nullable: false),
                    MoTa = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatHangs", x => x.MaMH);
                    table.ForeignKey(
                        name: "FK_MatHangs_LoaiMatHangs_MaLMH",
                        column: x => x.MaLMH,
                        principalTable: "LoaiMatHangs",
                        principalColumn: "MaLMH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatHangs_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatHangs_MaLMH",
                table: "MatHangs",
                column: "MaLMH");

            migrationBuilder.CreateIndex(
                name: "IX_MatHangs_MaNCC",
                table: "MatHangs",
                column: "MaNCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatHangs");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

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
    }
}
