using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ThuVien.Models;

namespace ThuVien.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index(int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<sach> sachList = db.saches.Where(x => x.TrangThai).ToList();

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            sachList = sachList.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(sachList);
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