using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuaHangTapHoa.Data.Migrations
{
    public partial class themDonHangVaCT_DonHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_NhaCungCaps_MaNCC",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "MaNhaCC",
                table: "SanPhams");

            migrationBuilder.AlterColumn<int>(
                name: "MaNCC",
                table: "SanPhams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    MaDH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenKH = table.Column<string>(nullable: true),
                    DiaChiKH = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    NgayLapDH = table.Column<DateTime>(nullable: false),
                    TongTien = table.Column<float>(nullable: false),
                    NgayNhanHang = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.MaDH);
                });

            migrationBuilder.CreateTable(
                name: "CT_DonHangs",
                columns: table => new
                {
                    Ma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaDH = table.Column<int>(nullable: false),
                    MaSP = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_DonHangs", x => x.Ma);
                    table.ForeignKey(
                        name: "FK_CT_DonHangs_DonHangs_MaDH",
                        column: x => x.MaDH,
                        principalTable: "DonHangs",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_DonHangs_SanPhams_MaSP",
                        column: x => x.MaSP,
                        principalTable: "SanPhams",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_DonHangs_MaDH",
                table: "CT_DonHangs",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_CT_DonHangs_MaSP",
                table: "CT_DonHangs",
                column: "MaSP");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_NhaCungCaps_MaNCC",
                table: "SanPhams",
                column: "MaNCC",
                principalTable: "NhaCungCaps",
                principalColumn: "MaNCC",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_NhaCungCaps_MaNCC",
                table: "SanPhams");

            migrationBuilder.DropTable(
                name: "CT_DonHangs");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.AlterColumn<int>(
                name: "MaNCC",
                table: "SanPhams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MaNhaCC",
                table: "SanPhams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_NhaCungCaps_MaNCC",
                table: "SanPhams",
                column: "MaNCC",
                principalTable: "NhaCungCaps",
                principalColumn: "MaNCC",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
