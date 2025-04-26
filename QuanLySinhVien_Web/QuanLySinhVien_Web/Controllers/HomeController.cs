using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien_Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuanLySinhVien_Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ConnectionDatabase _connectionDatabase;
        private readonly SinhVienRepos _sinhVienRepos;
        private readonly LopSinhHoatRepos _lopSinhHoatRepos;
        private readonly LopSinhHoatSinhVienRepos _lopSinhHoatSinhVienRepos;
        private readonly GiaoVienRepos _GiaoVienRepos;

        public HomeController(ConnectionDatabase connectionDatabase)
        {
            _connectionDatabase = connectionDatabase;
            _sinhVienRepos = new SinhVienRepos(_connectionDatabase);
            _lopSinhHoatRepos = new LopSinhHoatRepos(_connectionDatabase);
            _lopSinhHoatSinhVienRepos = new LopSinhHoatSinhVienRepos(connectionDatabase);
            _GiaoVienRepos = new GiaoVienRepos(_connectionDatabase);
        }

        //-- Sinh Viên --//
        public IActionResult Index()
        {
            var listSinhVien = _sinhVienRepos.GetAllSinhVien(); 
            return View(listSinhVien);
        }

        [HttpGet]
        public IActionResult TimKiemSinhVien(string TuKhoa)
        {
            List<SinhVien> sinhVien;
            if (string.IsNullOrEmpty(TuKhoa))
            {
                sinhVien = _sinhVienRepos.GetAllSinhVien();
            } else
            {
                sinhVien = _sinhVienRepos.TimKiemSinhVien(TuKhoa);
            }

            if (sinhVien == null || !sinhVien.Any())
            {
                TempData["Message"] = "Không tìm thấy kết quả nào.";
            }

            return View(sinhVien);
        }

        public IActionResult ThemSinhVien()
        {
            var sinhVien = new SinhVien();
            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSinhVien(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = _sinhVienRepos.AddSinhVien(sinhVien);

                if (isAdded)
                {
                    TempData["SuccessMessage"] = "Sinh viên đã được thêm thành công!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm sinh viên!";
                }
            }
            return View(sinhVien); 
        }

        public IActionResult SuaSinhVien(string maSV)
        {
            if (string.IsNullOrEmpty(maSV))
            {
                return NotFound();
            }

            var sinhVien = _sinhVienRepos.GetSinhVienByMaSV(maSV);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSinhVien(SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _sinhVienRepos.UpdateSinhVien(sinhVien);

                if (isUpdated)
                {
                    TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi khi cập nhật sinh viên!";
                }
            }

            return View(sinhVien);
        }

        public IActionResult XoaSinhVien(string maSV)
        {
            bool isDeleted = _sinhVienRepos.DeleteSinhVien(maSV);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy sinh viên hoặc có lỗi khi xóa!";
            }

            return RedirectToAction("Index");
        }
        //--- End Sinh Viên--//


        public IActionResult LopSinhHoat()
        {
            var listLopSinhHoat = _lopSinhHoatRepos.GetAllLopSinhHoat();
            return View(listLopSinhHoat);
        }

        public IActionResult LopSinhHoatSinhVien()
        {
            var listLopSinhHoatSinhVien = _lopSinhHoatSinhVienRepos.GetAllLopSinhHoatSinhVien();
            return View(listLopSinhHoatSinhVien);
        }

        //--- View Giáo Viên--//
        public IActionResult GiaoVien()
        {
            var listGiaoVien = _GiaoVienRepos.GetAllGiaoVien();
            return View(listGiaoVien);
        }

        public IActionResult ThemGiaoVien()
        {
            var giaoVien = new GiaoVien();
            return View(giaoVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult ThemGiaoVien(GiaoVien giaoVien)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = _GiaoVienRepos.AddGiaoVien(giaoVien);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Giáo viên đã được thêm thành công!";
                    return RedirectToAction("GiaoVien", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm Giáo viên!";
                }
            }
            return View(giaoVien);
        }
        //--- End Giáo Viên--//

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
