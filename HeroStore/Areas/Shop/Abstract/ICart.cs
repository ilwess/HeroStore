using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroStore.Areas.Shop.Abstract
{
    public interface ICart
    {
        IEnumerable<ProductDTO> Products { get; }
        void AddToCart(ProductDTO prod);
        void DeleteFromCart(ProductDTO prod);
        void ClearCart();
        
    }
}