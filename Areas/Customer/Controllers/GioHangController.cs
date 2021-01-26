using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuaHangTapHoa.Data;
using CuaHangTapHoa.Models.ViewModels;
using CuaHangTapHoa.Models;
using Microsoft.AspNetCore.Mvc;
using CuaHangTapHoa.Extensions;
using Microsoft.EntityFrameworkCore;
using CuaHangTapHoa.Utility;

namespace CuaHangTapHoa.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public GioHangViewModel GioHangVM { get; set; }

        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
            GioHangVM = new GioHangViewModel()
            {
                  SanPhams = new List<Models.SanPham>()
            };        
        }
        public async Task<IActionResult> Index()
        {
            List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");

            if(lstGioHang!=null)
            {
                if (lstGioHang.Count > 0)
                {
                    foreach (var item in lstGioHang)
                    {
                        SanPham sanpham = _db.SanPhams.Include(p => p.MatHang).Include(p => p.Tag).Include(p => p.NhaCungCap).Where(p => p.MaSP == item.MaSP).FirstOrDefault();
                        sanpham.SoLuong = item.SoLuong;
                        GioHangVM.SanPhams.Add(sanpham);
                    }
                }
            }
            return View(GioHangVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPOST()
        {
            List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");
            GioHangVM.DonHang.NgayNhanHang = GioHangVM.DonHang.NgayNhanHang
                .AddHours(GioHangVM.DonHang.GioNhanHang.Hour)
                .AddMinutes(GioHangVM.DonHang.GioNhanHang.Minute);
            GioHangVM.DonHang.NgayLapDH = DateTime.Now;
            //Tính tổng tiền của đơn hàng
            double tongTien = 0;
            foreach (var item in lstGioHang)
            {
                var sanPham = _db.SanPhams.Where(s => s.MaSP == item.MaSP).FirstOrDefault();
                tongTien += sanPham.Gia * item.SoLuong;
            }
            GioHangVM.DonHang.TongTien = tongTien;
            DonHang donHang = GioHangVM.DonHang;
            _db.DonHangs.Add(donHang);
            _db.SaveChanges();

            int maDH = donHang.MaDH;

            foreach (var sanpham in lstGioHang)
            {
                CT_DonHang cT_Don = new CT_DonHang()
                {
                    MaDH = maDH,
                    MaSP = sanpham.MaSP,
                    SoLuong= sanpham.SoLuong 
                };
                _db.CT_DonHangs.Add(cT_Don);

            }
            _db.SaveChanges();
            lstGioHang = new List<SLSP>();
            HttpContext.Session.Set("ssGioHang", lstGioHang);

            return RedirectToAction("XacNhanDonHang", "GioHang", new { ma = maDH });
        }

        public IActionResult Remove(int ma)
        {
            List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");

            if (lstGioHang.Count > 0)
            {
                SLSP sp = lstGioHang.Find(s => s.MaSP == ma);
                if (lstGioHang.Contains(sp))
                {
                    lstGioHang.Remove(sp);
                }
            }

            HttpContext.Session.Set("ssGioHang", lstGioHang);

            return RedirectToAction("Index");
        }
        //public IActionResult Update(int ma, int soLuong)
        //{
        //    int sanPham = GioHangVM.SanPhams.Count();
        //    List<SLSP> lstGioHang = HttpContext.Session.Get<List<SLSP>>("ssGioHang");
        //    if (lstGioHang.Count > 0)
        //    {
        //        SLSP sp = lstGioHang.Find(s => s.MaSP == ma);
        //        if (lstGioHang.Contains(sp))
        //        {
        //            lstGioHang.Remove(sp);
        //            sp.SoLuong = GioHangVM.SanPhams.Where(s => s.MaSP == ma).FirstOrDefault().SoLuong;
        //            lstGioHang.Add(sp);
        //        }
        //    }

        //    HttpContext.Session.Set("ssGioHang", lstGioHang);

        //    return RedirectToAction("Index");
        //}
        public IActionResult XacNhanDonHang(int ma)
        {
            GioHangVM.DonHang = _db.DonHangs.Where(a => ma == a.MaDH).FirstOrDefault();
            List<CT_DonHang> lstDon = _db.CT_DonHangs.Where(p => p.MaDH == ma).ToList();

            foreach (CT_DonHang obj in lstDon)
            {
                GioHangVM.SanPhams.Add(_db.SanPhams.Include(p=>p.MatHang).Include(p => p.Tag).Include(p => p.NhaCungCap).Where(p => p.MaSP == obj.MaSP).FirstOrDefault());
                GioHangVM.SanPhams.Find(p => p.MaSP == obj.MaSP).SoLuong = obj.SoLuong; //Sử dụng thuộc tính NotMapped SoLuong để lưu tạm
            }

            return View(GioHangVM);
        }
    }
}