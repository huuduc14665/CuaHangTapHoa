using CuaHangTapHoa.Models;
using CuaHangTapHoa.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuaHangTapHoa.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
            if (_db.Roles.Any(r => r.Name == SD.QuanLi)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.NhanVien)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.QuanLi)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new NhanVien
            {
                UserName = "nhhd@gmail.com",
                Email = "nhhd@gmail.com",
                TenNV = "Nguyễn Hoàng Hữu Đức",
                DiaChi="Thủ Đức",
                SoDT="0397535625",
                EmailConfirmed = true
            }, "Duc123*").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.Where(u => u.Email == "nhhd@gmail.com").FirstOrDefaultAsync();

            await _userManager.AddToRoleAsync(user, SD.QuanLi);
        }
    }
}
