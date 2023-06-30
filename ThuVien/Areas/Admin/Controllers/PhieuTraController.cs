using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThuVien.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class PhieuTraController : Controller
    {
        // GET: Admin/PhieuTra
        public ActionResult Index()
        {
            return View();
        }
    }
}