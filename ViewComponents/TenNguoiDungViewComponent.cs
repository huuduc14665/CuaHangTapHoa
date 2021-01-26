using CuaHangTapHoa.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CuaHangTapHoa.ViewComponents
{
    public class TenNguoiDungViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public TenNguoiDungViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userFromDb = await _db.NhanViens.Where(u => u.Id == claims.Value).FirstOrDefaultAsync();
            return View(userFromDb);
        }
    }
}
