using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using Filter = ThuVien.Models.Common.Filter;
using System.Data.Entity;

namespace ThuVien.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class SachController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }


            IEnumerable<sach> items = db.saches.OrderByDescending(x => x.masach);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchKeyword = Filter.ChuyenCoDauThanhKhongDau(Searchtext);
                items = items.Where(x => Filter.ChuyenCoDauThanhKhongDau(x.tensach).StartsWith(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                         Filter.ChuyenCoDauThanhKhongDau(x.tensach).Contains(searchKeyword) ||
                                         x.tensach.Contains(Searchtext));

            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.NXB = new SelectList(db.nhaxuatbans.ToList(), "manxb", "tennxb");
            // Tạo SelectList
            SelectList select_tl = new SelectList(db.theloais.ToList(), "matheloai", "tentheloai");
            ViewBag.selectedTheLoais = new List<SelectListItem>();
            // Set vào ViewBag
            ViewBag.list_TheLoai = select_tl;

            SelectList select_tg = new SelectList(db.tacgias.ToList(), "matacgia", "tentacgia");
            ViewBag.selectedTacGia = new List<SelectListItem>();
            // Set vào ViewBag
            ViewBag.list_TacGia = select_tg;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(sach temp, string[] selectedNXB, string[] selectedTheLoais, string[] selectedTacGia)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                // Loại bỏ phần tử rỗng (giá trị "")
                selectedTheLoais = selectedTheLoais.Where(s => !string.IsNullOrEmpty(s)).ToArray();

                if (selectedTheLoais.Length > 0)
                {
                    temp.theloais = db.theloais.Where(t => selectedTheLoais.Contains(t.matheloai.ToString())).ToList();
                }
                else
                {
                    temp.theloais = new List<theloai>();
                }

                // Loại bỏ phần tử rỗng (giá trị "")
                selectedTacGia = selectedTacGia.Where(s => !string.IsNullOrEmpty(s)).ToArray();

                if (selectedTacGia.Length > 0)
                {
                    temp.tacgias = db.tacgias.Where(t => selectedTacGia.Contains(t.matacgia.ToString())).ToList();
                }
                else
                {
                    temp.tacgias = new List<tacgia>();
                }
                int selectedNXBid = int.Parse(selectedNXB[0]);
                nhaxuatban selectedNXBObj = db.nhaxuatbans.FirstOrDefault(p => p.manxb == selectedNXBid);
                temp.nhaxuatban = selectedNXBObj;
                db.saches.Add(temp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NXB = new SelectList(db.nhaxuatbans.ToList(), "manxb", "tennxb");
            // Tạo SelectList
            SelectList select_tl = new SelectList(db.theloais.ToList(), "matheloai", "tentheloai");
            ViewBag.selectedTheLoais = new List<SelectListItem>();
            // Set vào ViewBag
            ViewBag.list_TheLoai = select_tl;

            SelectList select_tg = new SelectList(db.tacgias.ToList(), "matacgia", "tentacgia");
            ViewBag.selectedTacGia = new List<SelectListItem>();
            // Set vào ViewBag
            ViewBag.list_TacGia = select_tg;
            return View(temp);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy thông tin phim từ cơ sở dữ liệu
            sach sach_t = db.saches.Find(id);
            if (sach_t == null)
            {
                return HttpNotFound();
            }

            ViewBag.NXB = new SelectList(db.nhaxuatbans.ToList(), "manxb", "tennxb");
            // Tạo SelectList
            SelectList select_tl = new SelectList(db.theloais.ToList(), "matheloai", "TenTheLoai");
            //ViewBag.selectedTheLoais = new List<SelectListItem>();
            // Set vào ViewBag
            ViewBag.list_TheLoai = select_tl;

            // Lấy danh sách thể loại đã chọn của phim
            var selectedTheLoais = sach_t.theloais.Select(t => t.matheloai.ToString()).ToArray();
            ViewBag.selectedTheLoais = selectedTheLoais;
            var selectedTacGia = sach_t.tacgias.Select(t => t.matacgia.ToString()).ToArray();
            ViewBag.selectedTacGia = selectedTacGia;


            SelectList select_tg = new SelectList(db.tacgias.ToList(), "matacgia", "tentacgia");
            // Set vào ViewBag
            ViewBag.list_TacGia = select_tg;

            return View(sach_t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sach temp, string[] selectedNXB, string[] selectedTheLoais, string[] selectedTacGia)
        {
            if (ModelState.IsValid)
            {
                //var a = db.saches.FirstOrDefault(p => p.masach == temp.masach);
                var a = db.saches.Include(p=>p.theloais).FirstOrDefault(p => p.masach == temp.masach);
                if (a == null)
                {
                    return HttpNotFound();
                }

                // Cập nhật thông tin phim
                a.tensach = temp.tensach;
                a.mieuta = temp.mieuta;
                a.namxb = temp.namxb;
                a.soluong = temp.soluong;
                a.sotrang = temp.sotrang;
                a.giabia = temp.giabia;
                a.anh = temp.anh;
                a.TrangThai = temp.TrangThai;

                int selectedNXBId = int.Parse(selectedNXB[0]);

                // Loại bỏ phần tử rỗng (giá trị "")
                selectedTheLoais = selectedTheLoais.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                selectedTacGia = selectedTacGia.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                nhaxuatban selectedNXBObj = db.nhaxuatbans.FirstOrDefault(p => p.manxb == selectedNXBId);
                // Cập nhật danh sách thể loại của sach
                if (selectedTheLoais.Length > 0)
                {
                    // Xóa tất cả các thể loại hiện tại
                    a.theloais.Clear();

                    // Thêm từng thể loại mới vào danh sách
                    foreach (var theLoaiId in selectedTheLoais)
                    {
                        var theLoai = db.theloais.Find(int.Parse(theLoaiId));
                        if (theLoai != null)
                        {
                            a.theloais.Add(theLoai);
                        }
                    }
                }
                else
                {
                    // Xóa tất cả các thể loại
                    a.theloais.Clear();
                }

                if (selectedTacGia.Length > 0)
                {
                    // Xóa tất cả các thể loại hiện tại
                    a.tacgias.Clear();

                    // Thêm từng thể loại mới vào danh sách
                    foreach (var tacgiaId in selectedTacGia)
                    {
                        var tacgia = db.tacgias.Find(int.Parse(tacgiaId));
                        if (tacgia != null)
                        {
                            a.tacgias.Add(tacgia);
                        }
                    }
                }
                else
                {
                    // Xóa tất cả các thể loại
                    a.tacgias.Clear();
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(temp);
        }




        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.saches.Find(id);
            if (item != null)
            {
                db.saches.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.saches.Find(id);
            if (item != null)
            {
                item.TrangThai = !item.TrangThai;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.TrangThai });
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
                        var obj = db.saches.Find(Convert.ToInt32(item));
                        db.saches.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
