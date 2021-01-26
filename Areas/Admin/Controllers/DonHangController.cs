using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuaHangTapHoa.Utility;
using CuaHangTapHoa.Data;
using System.Security.Claims;
using CuaHangTapHoa.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using CuaHangTapHoa.Models;
using System.Text;

namespace CuaHangTapHoa.Areas.Admin.Controllers
{
    [Authorize(Roles =SD.QuanLi+","+SD.NhanVien)]
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int PageSize = 5;
        public DonHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(int productPage=1, string searchName = null, string searchAddress = null, string searchPhone = null, string searchDate = null)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);

            DonHangViewModel donHangVM = new DonHangViewModel()
            {
                DonHangs = new List<Models.DonHang>()
            };

            StringBuilder param = new StringBuilder();

            param.Append("/Admin/DonHang?productPage=:");
            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }
            param.Append("&searchEmail=");
            if (searchAddress != null)
            {
                param.Append(searchAddress);
            }
            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }
            param.Append("&searchDate=");
            if (searchDate != null)
            {
                param.Append(searchDate);
            }


            donHangVM.DonHangs = _db.DonHangs.Include(a => a.NhanVien).ToList();
            //donHangVM.DonHangs.OrderBy(p => p.NgayLapDH);
            if (User.IsInRole(SD.NhanVien))
            {
                donHangVM.DonHangs = donHangVM.DonHangs.Where(a => a.MaNV == claim.Value).ToList();

            }

            if (searchName != null)
            {
                donHangVM.DonHangs = donHangVM.DonHangs.Where(a => a.TenKH.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (searchAddress != null)
            {
                donHangVM.DonHangs = donHangVM.DonHangs.Where(a => a.DiaChiKH.ToLower().Contains(searchAddress.ToLower())).ToList();
            }
            if (searchPhone != null)
            {
                donHangVM.DonHangs = donHangVM.DonHangs.Where(a => a.SoDienThoai.ToLower().Contains(searchPhone.ToLower())).ToList();
            }
            if (searchDate != null)
            {
                try
                {
                    DateTime appDate = Convert.ToDateTime(searchDate);
                    donHangVM.DonHangs = donHangVM.DonHangs.Where(a => a.NgayNhanHang.ToShortDateString().Equals(appDate.ToShortDateString())).ToList();


                }
                catch (Exception ex)
                {

                }
            }
            var count = donHangVM.DonHangs.Count;

            donHangVM.DonHangs = donHangVM.DonHangs.OrderByDescending(p => p.NgayLapDH)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();

            donHangVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                ToTalItems = count,
                urlPram = param.ToString()
            };

            return View(donHangVM);
        }
        //GET Edit
        public async Task<IActionResult> Edit(int? ma)
        {
            if (ma == null)
                return NotFound();
            var danhsachSP = (IEnumerable<SanPham>)(from p in _db.SanPhams
                                                      join a in _db.CT_DonHangs
                                                      on p.MaSP equals a.MaSP
                                                      where a.DonHang.MaDH == ma
                                                      select p).Include("MatHang").Include("NhaCungCap");
            foreach(var item in danhsachSP)
            {
                item.SoLuong = (from p in _db.SanPhams
                                join a in _db.CT_DonHangs
                                on p.MaSP equals a.MaSP
                                where a.DonHang.MaDH == ma && a.MaSP == item.MaSP
                                select a.SoLuong).FirstOrDefault();  //Sử dụng thuộc tính NotMapeed SoLuong để lưu số lượng
            }
            CTDonHangViewModel objVM = new CTDonHangViewModel()
            {
                DonHang = _db.DonHangs.Include(a => a.NhanVien).Where(a => a.MaDH == ma).FirstOrDefault(),
                NhanViens = _db.NhanViens.ToList(),
                SanPhams = danhsachSP.ToList()
            };
            return View(objVM);
        }
        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int ma, CTDonHangViewModel objVM)
        {
            if (ModelState.IsValid)
            {
                objVM.DonHang.NgayNhanHang = objVM.DonHang.NgayNhanHang
                    .AddHours(objVM.DonHang.GioNhanHang.Hour)
                    .AddMinutes(objVM.DonHang.GioNhanHang.Minute);

                var donHangFromDb = _db.DonHangs.Where(a => a.MaDH == objVM.DonHang.MaDH).FirstOrDefault();

                donHangFromDb.TenKH = objVM.DonHang.TenKH;
                donHangFromDb.SoDienThoai = objVM.DonHang.SoDienThoai;
                donHangFromDb.DiaChiKH = objVM.DonHang.DiaChiKH;
                donHangFromDb.NgayNhanHang = objVM.DonHang.NgayNhanHang;
                donHangFromDb.TrangThai = objVM.DonHang.TrangThai;

                if (User.IsInRole(SD.QuanLi))
                {
                    donHangFromDb.MaNV = objVM.DonHang.MaNV;
                }
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            return View(objVM);
        }
        //GET Details
        public async Task<IActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();
            var danhsachSP = (IEnumerable<SanPham>)(from p in _db.SanPhams
                                                    join a in _db.CT_DonHangs
                                                    on p.MaSP equals a.MaSP
                                                    where a.DonHang.MaDH == ma
                                                    select p).Include("MatHang").Include("NhaCungCap");
            foreach (var item in danhsachSP)
            {
                item.SoLuong = (from p in _db.SanPhams
                                join a in _db.CT_DonHangs
                                on p.MaSP equals a.MaSP
                                where a.DonHang.MaDH == ma && a.MaSP == item.MaSP
                                select a.SoLuong).FirstOrDefault();
            }
            CTDonHangViewModel objVM = new CTDonHangViewModel()
            {
                DonHang = _db.DonHangs.Include(a => a.NhanVien).Where(a => a.MaDH == ma).FirstOrDefault(),
                NhanViens = _db.NhanViens.ToList(),
                SanPhams = danhsachSP.ToList()
            };
            return View(objVM);
        }
        //GET Delete
        public async Task<IActionResult> Delete(int? ma)
        {
            if (ma == null)
                return NotFound();
            var danhsachSP = (IEnumerable<SanPham>)(from p in _db.SanPhams
                                                    join a in _db.CT_DonHangs
                                                    on p.MaSP equals a.MaSP
                                                    where a.DonHang.MaDH == ma
                                                    select p).Include("MatHang").Include("NhaCungCap");
            foreach (var item in danhsachSP)
            {
                item.SoLuong = (from p in _db.SanPhams
                                join a in _db.CT_DonHangs
                                on p.MaSP equals a.MaSP
                                where a.DonHang.MaDH == ma && a.MaSP == item.MaSP
                                select a.SoLuong).FirstOrDefault();
            }
            CTDonHangViewModel objVM = new CTDonHangViewModel()
            {
                DonHang = _db.DonHangs.Include(a => a.NhanVien).Where(a => a.MaDH == ma).FirstOrDefault(),
                NhanViens = _db.NhanViens.ToList(),
                SanPhams = danhsachSP.ToList()
            };
            return View(objVM);
        }
        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            var donHang = await _db.DonHangs.FindAsync(ma);
            _db.DonHangs.Remove(donHang);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}