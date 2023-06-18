using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using Filter = ThuVien.Models.Common.Filter;

namespace ThuVien.Areas.Admin.Controllers
{
    public class PhieuMuonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }


            IEnumerable<phieumuon> items = db.phieumuons.OrderByDescending(x => x.username);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.username).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.username).Contains(searchKeyword) ||
                                         x.username.Contains(Searchtext));

            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        public ActionResult ListOrder(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }


            IEnumerable<phieumuon> items = db.phieumuons.Where(x => x.trangthaiduyet == false).ToList();
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.username).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.username).Contains(searchKeyword) ||
                                         x.username.Contains(Searchtext));

            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

    }
}
