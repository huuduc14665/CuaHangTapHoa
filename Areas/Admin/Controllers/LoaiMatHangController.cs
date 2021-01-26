using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuaHangTapHoa.Data;
using CuaHangTapHoa.Models;
using CuaHangTapHoa.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuaHangTapHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.QuanLi)]
    [Area("Admin")]
    public class LoaiMatHangController : Controller
    {

        private readonly ApplicationDbContext _db;
        public LoaiMatHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.LoaiMatHangs.ToList()); 
        }
        //Get Create Action method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiMatHang loaiMatHang)
        {
            if (ModelState.IsValid)
            {
                _db.Add(loaiMatHang);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiMatHang);
        }

        //Get Edit Action method
        public async Task<IActionResult> Edit(int? ma)
        {
            if (ma == null)
            {
                return NotFound();
            }
            var loaiMatHang = await _db.LoaiMatHangs.FindAsync(ma);
            if (loaiMatHang == null)
                return NotFound();

            return View(loaiMatHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ma, LoaiMatHang loaiMatHang)
        {
            if (ma != loaiMatHang.MaLMH)
                return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(loaiMatHang);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiMatHang);
        }

        //Get Details Action method
        public async Task<ActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();
            var loaiMatHang = await _db.LoaiMatHangs.FindAsync(ma);
            if (loaiMatHang == null)
                return NotFound();

            return View(loaiMatHang);
        }
        //Get Delete Action method
        public async Task<ActionResult> Delete(int? ma)
        {
            if (ma == null)
                return NotFound();
            var loaiMatHang = await _db.LoaiMatHangs.FindAsync(ma);
            if (loaiMatHang == null)
                return NotFound();

            return View(loaiMatHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            var loaiMatHang = await _db.LoaiMatHangs.FindAsync(ma);
            _db.Remove(loaiMatHang);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}