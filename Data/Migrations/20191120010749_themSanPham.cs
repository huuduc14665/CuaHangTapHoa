using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuaHangTapHoa.Data.Migrations
{
    public partial class themSanPham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    MaSP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenSP = table.Column<string>(nullable: true),
                    Gia = table.Column<double>(nullable: false),
                    MaMH = table.Column<int>(nullable: false),
                    MaTag = table.Column<int>(nullable: false),
                    MaNhaCC = table.Column<int>(nullable: false),
                    Hinh = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    TrangThai = table.Column<bool>(nullable: false),
                    MaNCC = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPhams_MatHangs_MaMH",
                        column: x => x.MaMH,
                        principalTable: "MatHangs",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhams_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SanPhams_Tags_MaTag",
                        column: x => x.MaTag,
                        principalTable: "Tags",
                        principalColumn: "MaTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaMH",
                table: "SanPhams",
                column: "MaMH");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaNCC",
                table: "SanPhams",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaTag",
                table: "SanPhams",
                column: "MaTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPhams");
        }
    }
}
