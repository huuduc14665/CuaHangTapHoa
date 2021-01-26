using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CuaHangTapHoa.Models;
using CuaHangTapHoa.Data;
using Microsoft.EntityFrameworkCore;
using CuaHangTapHoa.Extensions;
using CuaHangTapHoa.Utility;

namespace CuaHangTapHoa.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var sanPhamList = await _db.SanPhams.Include(m => m.MatHang).Include(m => m.Tag).ToListAsync();
            return View(sanPhamList);
        }

        //GET Details
        public async Task<IActionResult> Details(int ma)
        {
            //List<SLSP> lstGiohang = new List<SLSP>();
            //SLSP sp = new SLSP { MaSP = 1, SoLuong = 1 };
            //lstGiohang.Add(sp);
            //HttpContext.Session.Set("ssGioHang", lstGiohang);
            //List<SLSP> lstGioHang2 = HttpContext.Session.Get<List<SLSP>>("ssGioHang");
            //SLSP sp2 = lstGioHang2.Find(s => s.MaSP == 1);
            //bool kq=lstGioHang2.Contains(sp2);
            var sanPham = await _db.SanPhams.Include(m => m.MatHang).Include(m => m.Tag).Include(m=>m.NhaCungCap).Where(m => m.MaSP == ma).FirstOrDefaultAsync();

            return View(sanPham);
        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int ma, int soLuong)
        {
            List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");

            if (lstGioHang == null)
            {
                lstGioHang = new List<SLSP>();
            }
            SLSP sp = new SLSP { MaSP = ma, SoLuong = soLuong}; // Sử dụng struct SLSP để lưu mã sp và số lượng
            lstGioHang.Add(sp);
            HttpContext.Session.Set("ssGioHang", lstGioHang);

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult Remove(int ma)
        {
            List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");
            var result = lstGioHang.ToList();
            if (lstGioHang.Count > 0)
            {
                SLSP sp = lstGioHang.Find(a => a.MaSP == ma);
                if (lstGioHang.Contains(sp))
                {
                    lstGioHang.Remove(sp);
                }
            }
            HttpContext.Session.Set("ssGioHang", lstGioHang);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
