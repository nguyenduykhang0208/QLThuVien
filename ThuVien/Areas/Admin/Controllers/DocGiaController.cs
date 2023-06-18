using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using Filter = ThuVien.Models.Common.Filter;

namespace ThuVien.Areas.Admin.Controllers
{
    public class DocGiaController : Controller
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
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            IEnumerable<ApplicationUser> items = (from user in db.Users
                                                  where user.Roles.Any(ur => db.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Customer"))
                                                  orderby user.Id // Sắp xếp theo một trường nào đó, ví dụ: Id
                                                  select user)
                                                  .Skip(pageSize * (pageIndex - 1))
                                                  .Take(pageSize);



            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.UserName).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.UserName).Contains(searchKeyword) ||
                                         x.UserName.Contains(Searchtext));

            }
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }


        public ActionResult Edit(string id)
        {
            var item = db.Users.Find(id);
            ViewBag.GioiTinhList = new SelectList(new[]
          {
                new { Value = "Nam", Text = "Nam" },
                new { Value = "Nữ", Text = "Nữ" }
                }, "Value", "Text", item.gioitinh);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser temp)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(temp);
                db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temp);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                db.Users.Remove(item);
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
                        var obj = db.Users.Find(Convert.ToInt32(item));
                        db.Users.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
