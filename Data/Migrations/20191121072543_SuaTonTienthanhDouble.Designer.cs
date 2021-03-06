﻿// <auto-generated />
using System;
using CuaHangTapHoa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CuaHangTapHoa.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191121072543_SuaTonTienthanhDouble")]
    partial class SuaTonTienthanhDouble
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CuaHangTapHoa.Models.CT_DonHang", b =>
                {
                    b.Property<int>("Ma")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaDH");

                    b.Property<int>("MaSP");

                    b.Property<int>("SoLuong");

                    b.HasKey("Ma");

                    b.HasIndex("MaDH");

                    b.HasIndex("MaSP");

                    b.ToTable("CT_DonHangs");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.DonHang", b =>
                {
                    b.Property<int>("MaDH")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChiKH");

                    b.Property<DateTime>("NgayLapDH");

                    b.Property<DateTime>("NgayNhanHang");

                    b.Property<string>("SoDienThoai");

                    b.Property<string>("TenKH");

                    b.Property<double>("TongTien");

                    b.Property<bool>("TrangThai");

                    b.HasKey("MaDH");

                    b.ToTable("DonHangs");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.LoaiMatHang", b =>
                {
                    b.Property<int>("MaLMH")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MoTa");

                    b.Property<string>("TenLMH")
                        .IsRequired();

                    b.Property<bool>("TrangThai");

                    b.HasKey("MaLMH");

                    b.ToTable("LoaiMatHangs");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.MatHang", b =>
                {
                    b.Property<int>("MaMH")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaLMH");

                    b.Property<string>("MoTa");

                    b.Property<string>("TenMH")
                        .IsRequired();

                    b.Property<bool>("TrangThai");

                    b.HasKey("MaMH");

                    b.HasIndex("MaLMH");

                    b.ToTable("MatHangs");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.NhaCungCap", b =>
                {
                    b.Property<int>("MaNCC")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .IsRequired();

                    b.Property<string>("SoDienThoai")
                        .IsRequired();

                    b.Property<string>("TenNCC")
                        .IsRequired();

                    b.Property<bool>("TrangThai");

                    b.HasKey("MaNCC");

                    b.ToTable("NhaCungCaps");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.SanPham", b =>
                {
                    b.Property<int>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Gia");

                    b.Property<string>("Hinh");

                    b.Property<int>("MaMH");

                    b.Property<int>("MaNCC");

                    b.Property<int>("MaTag");

                    b.Property<string>("MoTa");

                    b.Property<string>("TenSP");

                    b.Property<bool>("TrangThai");

                    b.HasKey("MaSP");

                    b.HasIndex("MaMH");

                    b.HasIndex("MaNCC");

                    b.HasIndex("MaTag");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.Tag", b =>
                {
                    b.Property<int>("MaTag")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenTag")
                        .IsRequired();

                    b.HasKey("MaTag");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.CT_DonHang", b =>
                {
                    b.HasOne("CuaHangTapHoa.Models.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("MaDH")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CuaHangTapHoa.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.MatHang", b =>
                {
                    b.HasOne("CuaHangTapHoa.Models.LoaiMatHang", "LoaiMatHang")
                        .WithMany()
                        .HasForeignKey("MaLMH")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CuaHangTapHoa.Models.SanPham", b =>
                {
                    b.HasOne("CuaHangTapHoa.Models.MatHang", "MatHang")
                        .WithMany()
                        .HasForeignKey("MaMH")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CuaHangTapHoa.Models.NhaCungCap", "NhaCungCap")
                        .WithMany()
                        .HasForeignKey("MaNCC")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CuaHangTapHoa.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("MaTag")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
