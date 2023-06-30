using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuVien.Models.ViewModels
{
    public class bookReturnViewModel
    {
        public phieumuon p_muon { get; set; }
        //public List<chitietmuon> ct_muon { get; set; }
        public List<ChitietmuonViewModel>  ctmuon_vmd{ get; set; }
    }
}