using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuVien.Models.ViewModels
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            this.Items = new List<CartItem>();
        }

        public void AddToCart(CartItem item, int Quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.masach == item.masach);
            if (checkExits != null)
            {
                checkExits.soluong += Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExits = Items.SingleOrDefault(x => x.masach == id);
            if (checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkExits = Items.SingleOrDefault(x => x.masach == id);
            if (checkExits != null)
            {
                checkExits.soluong = quantity;
            }
        }

        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.soluong);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

        public int GetCountOfItem()
        {
            return Items.Count;
        }
    }

    public class CartItem
    {
        public int masach { get; set; }
        public string tensach { get; set; }
        public string theloai { get; set; }
        public string anh { get; set; }
        public int soluong { get; set; }
    }
}