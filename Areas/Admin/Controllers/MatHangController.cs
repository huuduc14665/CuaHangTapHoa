using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CuaHangTapHoa.Data;
using CuaHangTapHoa.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using CuaHangTapHoa.Models;
using Microsoft.AspNetCore.Authorization;
using CuaHangTapHoa.Utility;

namespace CuaHangTapHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.QuanLi)]
    [Area("Admin")]
    public class MatHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public MatHangViewModel matHangsVM { get; set; }
        public MatHangController(ApplicationDbContext db)
        {
            _db = db;
            matHangsVM = new MatHangViewModel()
            {
                LoaiMatHang = _db.LoaiMatHangs.ToList(),
                MatHang = new Models.MatHang()
            };
        }
        public async Task<IActionResult> Index()
        {
            var matHangs = _db.MatHangs.Include(m => m.LoaiMatHang);
            return View(await matHangs.ToListAsync());
        }

        //GET: MatHang CREATE
        public IActionResult Create()
        {
            return View(matHangsVM);
        }

        //POST: Prduct CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
                return View(matHangsVM);
            _db.MatHangs.Add(matHangsVM.MatHang);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET: Product EDIT
        public async Task<IActionResult> Edit(int? ma)
        {
            if (ma == null)
                return NotFound();

            matHangsVM.MatHang = await _db.MatHangs.Include(m => m.LoaiMatHang).SingleOrDefaultAsync(m => m.MaMH == ma);

            if (matHangsVM.MatHang == null)
                return NotFound();

            return View(matHangsVM);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ma)
        {
            if (ModelState.IsValid)
            {

                var productFromDb = _db.MatHangs.Where(m => m.MaMH == matHangsVM.MatHang.MaMH).FirstOrDefault();

                productFromDb.TenMH = matHangsVM.MatHang.TenMH;
                productFromDb.MaLMH = matHangsVM.MatHang.MaLMH;
                productFromDb.MoTa = matHangsVM.MatHang.MoTa;
                productFromDb.TrangThai = matHangsVM.MatHang.TrangThai;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matHangsVM);
        }
        //Get Details
        public async Task<IActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();

            matHangsVM.MatHang = await _db.MatHangs.Include(m => m.LoaiMatHang).SingleOrDefaultAsync(m => m.MaMH == ma);

            if (matHangsVM.MatHang == null)
                return NotFound();

            return View(matHangsVM);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? ma)
        {
            if (ma == null)
                return NotFound();

            matHangsVM.MatHang = await _db.MatHangs.Include(m => m.LoaiMatHang).SingleOrDefaultAsync(m => m.MaMH == ma);

            if (matHangsVM.MatHang == null)
                return NotFound();

            return View(matHangsVM);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            MatHang matHang = await _db.MatHangs.FindAsync(ma);

            if (matHang == null)
            {
                return NotFound();
            }
            else
            {

                _db.MatHangs.Remove(matHang);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }


    }
}