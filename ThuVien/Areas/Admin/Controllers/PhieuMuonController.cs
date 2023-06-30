using Antlr.Runtime.Misc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using ThuVien.Models.ViewModels;
using Filter = ThuVien.Models.Common.Filter;
using System.Data.Entity.Validation;

namespace ThuVien.Areas.Admin.Controllers
{
    public class PhieuMuonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string Searchtext, int? page, int? Selectedindex)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }

            List<SelectListItem> options_list = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Tất cả"},
            new SelectListItem { Value = "2", Text = "Phiếu đã từ chối" },
            new SelectListItem { Value = "3", Text = "Phiếu đang mượn" },
            new SelectListItem { Value = "4", Text = "Phiếu đã trả" }
        };
            ViewBag.options = new SelectList(options_list, "Value", "Text","1");

            if (Selectedindex.HasValue)
            {
                GetIndexByOption(Selectedindex.Value, pageSize, page);         
            }
            IEnumerable<phieumuon> items = db.phieumuons.OrderByDescending(x => x.ngaymuon);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.UserName).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.UserName).Contains(searchKeyword) ||
                                         x.UserName.Contains(Searchtext));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        public ActionResult GetIndexByOption(int op_id, int? pageSize, int? page)
        {
            IEnumerable<phieumuon> items = db.phieumuons.OrderByDescending(x => x.ngaymuon);
            if (op_id == 2)
                items = items.Where(x => x.tuchoi == true);
            if (op_id == 3)
                items = items.Where(x => x.tuchoi == false && x.trangthaiduyet == true && x.trangthaitra == false);
            if (op_id == 4)
                items = items.Where(x => x.tuchoi == false && x.trangthaiduyet == true && x.trangthaitra == true);

            ViewBag.PageSize = pageSize;
            ViewBag.page = page;

            return PartialView("_PartialPhieuMuon", items);
        }


        public ActionResult DoiDuyet(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }


            IEnumerable<phieumuon> items = db.phieumuons.Where(x => x.trangthaiduyet == false && x.tuchoi == false).ToList();
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.UserName.StartsWith(Searchtext, StringComparison.OrdinalIgnoreCase) ||x.UserName.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        [HttpPost]
        public ActionResult Confirm(int id, DateTime hethan)
        {
            var item = db.phieumuons.Find(id);
            if (item != null)
            {
                item.trangthaiduyet = true;
                item.ngaymuon = DateTime.Now;
                item.ngayhethan = hethan;
                db.phieumuons.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public ActionResult ConfirmAll(string id, DateTime hethan)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var items = id.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.phieumuons.Find(Convert.ToInt32(item));
                        obj.trangthaiduyet = true;
                        obj.ngaymuon = DateTime.Now;
                        obj.ngayhethan = hethan;
                        db.phieumuons.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        //foreach (var chitiet in obj.chitietmuons)
                        //{
                        //    chitiet.n = DateTime.Now;
                        //    db.Entry(chitiet).State = EntityState.Modified;
                        //}
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Detail(int id, int? page)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath; // Lấy đường dẫn của trang trước đó
            if (returnUrl.Contains("DoiDuyet"))
            {
                ViewBag.ReturnUrl = "/Admin/PhieuMuon/DoiDuyet";
            }
            else
            {
                ViewBag.ReturnUrl = "/Admin/PhieuMuon";
            }

            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            IEnumerable<ChitietmuonViewModel> chitietmuonList = (from ct in db.chitietmuons
                                   join s in db.saches on ct.masach equals s.masach
                                   where ct.maphieumuon == id
                                   select new ChitietmuonViewModel
                                   {
                                       Sach = s,
                                       soluongmuon = ct.soluong
                                   }).ToList();
            chitietmuonList = chitietmuonList.ToPagedList(pageIndex, pageSize);

            return View(chitietmuonList);
        }

        public ActionResult Modal_Reject(int? t_id)
        {
            phieumuon item = db.phieumuons.Where(p => p.maphieumuon == t_id).FirstOrDefault();
            return PartialView(item);
        }

        [HttpPost]
        public ActionResult Modal_Reject(phieumuon temp)
        {
            db.phieumuons.Attach(temp);
            temp.tuchoi = true;
            db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Partial_Accept()
        {
            return PartialView();
        }

        public ActionResult Partial_Accept_All()
        {
            return PartialView();
        }

        public ActionResult bookReturn(int? id)
        {
            var viewModel = new bookReturnViewModel();
            List<ChitietmuonViewModel> chitietmuonList = (from ct in db.chitietmuons
                                                    join s in db.saches on ct.masach equals s.masach
                                                    where ct.maphieumuon == id
                                                    select new ChitietmuonViewModel
                                                    {
                                                        Sach = s,
                                                        soluongmuon = ct.soluong,
                                                        sotralai = ct.sotralai
                                                    }).ToList();

            viewModel.p_muon = db.phieumuons.Where(p => p.maphieumuon == id).FirstOrDefault();
            viewModel.ct_muon = db.chitietmuons.Where(ctm => ctm.maphieumuon == id).ToList();
            viewModel.ctmuon_vmd = chitietmuonList;

            return PartialView(viewModel);
        }


        [HttpPost]
        public ActionResult bookReturn(bookReturnViewModel temp)
        {
            var p_muon = temp.p_muon;
            db.phieumuons.Attach(p_muon);
            p_muon.trangthaitra = true;
            db.Entry(p_muon).State = EntityState.Modified;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(p_muon.userID);
            if (user != null && p_muon.sosachmat.HasValue)
            {
                user.matsach = p_muon.sosachmat.Value;
                userManager.Update(user);
            }
            foreach (var chitiet in temp.ctmuon_vmd)
            {
                sach t = (sach)db.saches.Where(x => x.masach == chitiet.Sach.masach);

                if (t != null)
                {
                    t.soluong += chitiet.soluongmuon;
                    db.Entry(t).State = EntityState.Modified;
                }
            }

            try
            {
                // Existing code...

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        // Log or handle each validation error here
                        var errorMessage = $"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}";
                        // Example: logger.LogError(errorMessage);
                    }
                }

                // Handle the exception or rethrow if necessary
                throw;
            }

        }


    }
}
