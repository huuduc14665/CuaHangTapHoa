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
    public class NhaCungCapController : Controller
    {

        private readonly ApplicationDbContext _db;
        public NhaCungCapController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.NhaCungCaps.ToList());
        }
        //Get Create Action method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _db.Add(nhaCungCap);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        //Get Edit Action method
        public async Task<IActionResult> Edit(int? ma)
        {
            if (ma == null)
            {
                return NotFound();
            }
            var nhaCungCap = await _db.NhaCungCaps.FindAsync(ma);
            if (nhaCungCap == null)
                return NotFound();

            return View(nhaCungCap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ma, NhaCungCap nhaCungCap)
        {
            if (ma != nhaCungCap.MaNCC)
                return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(nhaCungCap);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        //Get Details Action method
        public async Task<ActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();
            var nhaCungCap = await _db.NhaCungCaps.FindAsync(ma);
            if (nhaCungCap == null)
                return NotFound();

            return View(nhaCungCap);
        }
        //Get Delete Action method
        public async Task<ActionResult> Delete(int ma)
        {
            if (ma == null)
                return NotFound();
            var nhaCungCap = await _db.NhaCungCaps.FindAsync(ma);
            if (nhaCungCap == null)
                return NotFound();

            return View(nhaCungCap);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            var nhaCungCap = await _db.NhaCungCaps.FindAsync(ma);
            _db.Remove(nhaCungCap);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}