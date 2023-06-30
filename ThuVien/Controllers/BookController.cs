using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ThuVien.Models;
using Filter = ThuVien.Models.Common.Filter;

namespace ThuVien.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index(int? page,int? cateID,string searchString)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.CateID = cateID;
            TempData["ActiveCategory"] = cateID;
            if (cateID != null)
            {
                IEnumerable<sach> sachList = db.saches.Where(x => x.TrangThai && x.theloais.Any(tl=>tl.matheloai == cateID)).ToList();
                if (!string.IsNullOrEmpty(searchString))
                {
                    string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(searchString);
                    sachList = sachList.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.tensach).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             Filter.ChuyenCoDauThanhKhongDau(x.tensach).Contains(searchKeyword) ||
                                             x.tensach.Contains(searchString));
                }
                sachList = sachList.ToPagedList(pageIndex, pageSize);
                return View(sachList);
            }
            else
            {
                IEnumerable<sach> sachList = db.saches.Where(x => x.TrangThai).ToList();
                if (!string.IsNullOrEmpty(searchString))
                {
                    string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(searchString);
                    sachList = sachList.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.tensach).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                             Filter.ChuyenCoDauThanhKhongDau(x.tensach).Contains(searchKeyword) ||
                                             x.tensach.Contains(searchString));
                }
                sachList = sachList.ToPagedList(pageIndex, pageSize);
                return View(sachList);

            }

        }

        public ActionResult Detail(int id)
        {
            var item = db.saches.Find(id);
            return View(item);
        }

        public ActionResult _PartialCate()
        {
            var item = db.theloais.ToList();
            return PartialView(item);
        }
    }
}