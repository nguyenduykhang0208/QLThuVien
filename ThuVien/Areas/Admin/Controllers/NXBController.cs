using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using Filter = ThuVien.Models.Common.Filter;

namespace ThuVien.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class NXBController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Phim
        public ActionResult Index(string Searchtext, int? page)
        {

            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<nhaxuatban> items = db.nhaxuatbans.OrderByDescending(x => x.manxb);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.tennxb).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.tennxb).Contains(searchKeyword) ||
                                         x.tennxb.Contains(Searchtext));

            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(nhaxuatban temp)
        {
            if (ModelState.IsValid)
            {
                db.nhaxuatbans.Add(temp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temp);
        }

        public ActionResult Edit(int? id)
        {
            var item = db.nhaxuatbans.Find(id);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(nhaxuatban temp)
        {
            if (ModelState.IsValid)
            {
                db.nhaxuatbans.Attach(temp);
                db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temp);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.nhaxuatbans.Find(id);
            if (item != null)
            {
                db.nhaxuatbans.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.nhaxuatbans.Find(Convert.ToInt32(item));
                        db.nhaxuatbans.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}