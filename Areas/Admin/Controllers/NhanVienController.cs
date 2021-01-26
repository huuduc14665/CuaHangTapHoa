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
    [Authorize(Roles =SD.QuanLi)]
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NhanVienController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.NhanViens.ToList());
        }

        //GET Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var nvFromDb = await _db.NhanViens.FindAsync(id);
            if (nvFromDb == null)
                return NotFound();
            return View(nvFromDb);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                NhanVien nvFromDb = _db.NhanViens.Where(u => u.Id == id).FirstOrDefault();

                nvFromDb.TenNV = nvFromDb.TenNV;
                nvFromDb.SoDT = nvFromDb.SoDT;
                nvFromDb.DiaChi = nvFromDb.DiaChi;

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(nhanVien);
        }

        //GET Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var nvFromDb = await _db.NhanViens.FindAsync(id);
            if (nvFromDb == null)
                return NotFound();
            return View(nvFromDb);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string id)
        {

            NhanVien nvFromDb = _db.NhanViens.Where(u => u.Id == id).FirstOrDefault();

            nvFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}