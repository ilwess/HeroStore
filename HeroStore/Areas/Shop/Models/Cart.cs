using BLL.DTO;
using HeroStore.Areas.Shop.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroStore.Areas.Shop.Models
{
    public class Cart : ICart
    {
        private ICollection<ProductDTO> products
            = new List<ProductDTO>();

        public IEnumerable<ProductDTO> Products
        { get
            { return products; }
        }

        public void AddToCart(ProductDTO prod)
        {
            products.Add(prod);
        }

        public void ClearCart()
        {
            products.Clear();
        }

        public void DeleteFromCart(ProductDTO prod)
        {
            products.Remove(prod);
        }
    }
}