using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;

namespace ThuVien.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            List<sach> sachList = db.saches.ToList();
            return View(sachList);
        }
    }
}