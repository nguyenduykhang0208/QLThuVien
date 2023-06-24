using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using ThuVien.Models.ViewModels;

namespace ThuVien.Controllers
{
    public class CartController : Controller
    {
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
        public ActionResult CheckOutSuccess()
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
            return PartialView();
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
            if (checkProduct != null)
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
                    theloai = checkProduct.theloais.ToString(),
                    soluong = quantity
                };
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sách thành công!", code = 1, Count = cart.Items.Count };
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
        //[HttpPost]
        //public ActionResult Update(int id, int quantity)
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart != null)
        //    {
        //        cart.UpdateQuantity(id, quantity);
        //        return Json(new { Success = true });
        //    }
        //    return Json(new { Success = false });
        //}
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