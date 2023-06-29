using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using ThuVien.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.Entity;

namespace ThuVien.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {

            Cart cart = (Cart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public ActionResult CheckOut()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult ConfirmCheckOut()
        {
            return View();
        }

        public ActionResult Partial_Item_Cart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        public ActionResult Partial_Cus_CheckOut()
        {
            string id = User.Identity.GetUserId();
            var customer = db.Users.Find(id);
            return PartialView(customer);
        }

        public ActionResult Partial_Item_CheckOut()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.saches.FirstOrDefault(x => x.masach == id);
            if(checkProduct != null && checkProduct.soluong < quantity || checkProduct.soluong == 0)
            {
                code = new { Success = false, msg = "Sách tạm hết!", code = -1, Count = 0 };
            }
            else
            {
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    cart = new Cart();
                }
                CartItem item = new CartItem
                {
                    masach = checkProduct.masach,
                    tensach = checkProduct.tensach,
                    anh = checkProduct.anh,
                    theloai = string.Join(", ", checkProduct.theloais.Select(t => t.tentheloai)),
                    soluong = quantity
                };
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sách thành công!", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(ApplicationUser user)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                Cart cart = (Cart)Session["Cart"];
                if (cart != null)
                {
                    phieumuon order = new phieumuon();
                    order.userID = user.Id.ToString();
                    order.tongtienphat = 0;
                    order.UserName = user.UserName;
                    order.ngaymuon = DateTime.Now;
                    order.soluongchitietmuon = cart.GetCountOfItem();
                    order.trangthaiduyet = false;
                    order.trangthaitra = false;
                    order.tuchoi = false;
                    cart.Items.ForEach(x =>
                    {
                        order.chitietmuons.Add(new chitietmuon
                        {
                            masach = x.masach,
                            soluong = x.soluong,
                        });

                        // Trừ số lượng sách trong kho
                        var book = db.saches.Find(x.masach);
                        if (book != null)
                        {
                            book.soluong -= x.soluong;
                            db.Entry(book).State = EntityState.Modified;
                        }
                    });

                    db.phieumuons.Add(order);
                    db.SaveChanges();                 
                    cart.ClearCart();
                    //code = new { Success = true, Code = 1, Url = url };
                    return RedirectToAction("ConfirmCheckOut");
                }
            }
            return Json(code);
        }

        public ActionResult ShowCount()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        } 

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.masach == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }



        [HttpPost]
        public ActionResult DeleteAll()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

    }
}