using System;
using System.Collections.Generic;
using System.Text;
using CuaHangTapHoa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CuaHangTapHoa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoaiMatHang> LoaiMatHangs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }

        public virtual DbSet<CT_DonHang> CT_DonHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }

    }
}
