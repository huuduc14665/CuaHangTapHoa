using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangTapHoa.Data;
using CuaHangTapHoa.Models;
using CuaHangTapHoa.Models.ViewModels;
using CuaHangTapHoa.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuaHangTapHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.QuanLi)]
    [Area("Admin")]
    public class SanPhamController : Controller
    {
       
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public SanPhamViewModel sanPhamVM { get; set; }

        int PageSize = 4;
        public SanPhamController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            sanPhamVM = new SanPhamViewModel()
            {
                MatHangs = _db.MatHangs.ToList(),
                Tags = _db.Tags.ToList(),
                NhaCungCaps = _db.NhaCungCaps.ToList(),
                SanPham = new Models.SanPham()
            };
        }
        public async Task<IActionResult> Index(int productPage=1, string searchTen=null, string searchMH=null, string searchTag=null, string searchNCC=null)
        {
            DSSanPhamViewModel dssanPhamVM = new DSSanPhamViewModel()
            {
                SanPhams = _db.SanPhams.Include(m => m.MatHang).Include(m => m.Tag).Include(m => m.NhaCungCap)
            };

            StringBuilder param = new StringBuilder();

            param.Append("/Admin/SanPham?productPage=:");
            param.Append("&searchTen=");
            if (searchTen != null)
            {
                param.Append(searchTen);
            }
            param.Append("&searchMH=");
            if (searchMH != null)
            {
                param.Append(searchMH);
            }
            param.Append("&searchTag=");
            if (searchTag != null)
            {
                param.Append(searchTag);
            }
            param.Append("&searchNCC=");
            if (searchNCC != null)
            {
                param.Append(searchNCC);
            }


            if (searchTen != null)
            {
                dssanPhamVM.SanPhams = dssanPhamVM.SanPhams.Where(a => a.TenSP.ToLower().Contains(searchTen.ToLower())).ToList();
            }
            if (searchMH != null)
            {
                dssanPhamVM.SanPhams = dssanPhamVM.SanPhams.Where(a => a.MatHang.TenMH.ToLower().Contains(searchMH.ToLower())).ToList();
            }
            if (searchTag != null)
            {
                dssanPhamVM.SanPhams = dssanPhamVM.SanPhams.Where(a => a.Tag.TenTag.ToLower().Contains(searchTag.ToLower())).ToList();
            }
            if (searchNCC != null)
            {
                dssanPhamVM.SanPhams = dssanPhamVM.SanPhams.Where(a => a.NhaCungCap.TenNCC.ToLower().Contains(searchNCC.ToLower())).ToList();
            }


            var count = dssanPhamVM.SanPhams.Count();
            dssanPhamVM.SanPhams = dssanPhamVM.SanPhams.OrderBy(p => p.TenSP)
                                 .Skip((productPage - 1) * PageSize)
                                .Take(PageSize).ToList();

            dssanPhamVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                ToTalItems = count,
                urlPram = param.ToString()
            };

            return View(dssanPhamVM);
        }

        //GET: MatHang CREATE
        public IActionResult Create()
        {
            return View(sanPhamVM);
        }

        //POST: Prduct CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
                return View(sanPhamVM);
            _db.SanPhams.Add(sanPhamVM.SanPham);
            await _db.SaveChangesAsync();
            //Hình đang được lưu
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var sanPhamFromDb =_db.SanPhams.Find(sanPhamVM.SanPham.MaSP);
            if (files.Count != 0)
            {
                //Image has ben uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, sanPhamVM.SanPham.MaSP + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                sanPhamFromDb.Hinh = @"\" + SD.ImageFolder + @"\" + sanPhamVM.SanPham.MaSP + extension;
            }
            else
            {
                //when user doesn't upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + sanPhamVM.SanPham.MaSP + ".png");
                sanPhamFromDb.Hinh = @"\" + SD.ImageFolder + @"\" + sanPhamVM.SanPham.MaSP + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET: Product EDIT
        public async Task<IActionResult> Edit(int? ma)
        {
            if (ma == null)
                return NotFound();

            sanPhamVM.SanPham = await _db.SanPhams.Include(m => m.MatHang).Include(m=>m.Tag).Include(m=>m.NhaCungCap).SingleOrDefaultAsync(m => m.MaSP == ma);

            if (sanPhamVM.SanPham == null)
                return NotFound();

            return View(sanPhamVM);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ma)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var sanPhamFromDb = _db.SanPhams.Where(m => m.MaSP == sanPhamVM.SanPham.MaSP).FirstOrDefault();

                if (files.Count > 0 && files[0] != null)
                {
                    //if user upload a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(sanPhamFromDb.Hinh);

                    if (System.IO.File.Exists(Path.Combine(uploads, sanPhamVM.SanPham.MaSP + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, sanPhamVM.SanPham.MaSP + extension_old));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, sanPhamVM.SanPham.MaSP + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    sanPhamVM.SanPham.Hinh = @"\" + SD.ImageFolder + @"\" + sanPhamVM.SanPham.MaSP + extension_new;
                }
                if (sanPhamVM.SanPham.Hinh != null)
                {
                    sanPhamFromDb.Hinh = sanPhamVM.SanPham.Hinh;
                }


                sanPhamFromDb.TenSP = sanPhamVM.SanPham.TenSP;
                sanPhamFromDb.Gia = sanPhamVM.SanPham.Gia;
                sanPhamFromDb.MaMH = sanPhamVM.SanPham.MaMH;
                sanPhamFromDb.MaTag = sanPhamVM.SanPham.MaTag;
                sanPhamFromDb.MaNCC = sanPhamVM.SanPham.MaNCC;
                sanPhamFromDb.MoTa = sanPhamVM.SanPham.MoTa;
                sanPhamFromDb.TrangThai = sanPhamVM.SanPham.TrangThai;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPhamVM);
        }
        //Get Details
        public async Task<IActionResult> Details(int? ma)
        {
            if (ma == null)
                return NotFound();

            sanPhamVM.SanPham = await _db.SanPhams.Include(m => m.MatHang).Include(m => m.Tag).Include(m => m.NhaCungCap).SingleOrDefaultAsync(m => m.MaSP == ma);

            if (sanPhamVM.SanPham == null)
                return NotFound();

            return View(sanPhamVM);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? ma)
        {
            if (ma == null)
                return NotFound();

            sanPhamVM.SanPham = await _db.SanPhams.Include(m => m.MatHang).Include(m => m.Tag).Include(m => m.NhaCungCap).SingleOrDefaultAsync(m => m.MaSP == ma);

            if (sanPhamVM.SanPham == null)
                return NotFound();

            return View(sanPhamVM);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ma)
        {
            SanPham sanPham = await _db.SanPhams.FindAsync(ma);
            string webRootPath = _hostingEnvironment.WebRootPath;

            if (sanPham == null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(sanPham.Hinh);

                if (System.IO.File.Exists(Path.Combine(uploads, sanPham.MaSP + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, sanPham.MaSP + extension));
                }
                _db.SanPhams.Remove(sanPham);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }


    }
}