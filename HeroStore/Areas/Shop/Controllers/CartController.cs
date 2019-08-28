using BLL.Abstract;
using BLL.DTO;
using HeroStore.Areas.Shop.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HeroStore.Areas.Shop.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private ICart cart;
        private IProductService prodService;

        public CartController(ICart cart,
            IProductService service)
        {
            this.cart = cart;
            prodService = service;
            this.cart.AddToCart(prodService.GetAll().FirstOrDefault());
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetProductsFromCart()
        {
            return Ok(cart.Products);
        }
        
        [HttpPut]
        [Route("put")]
        public async Task<IHttpActionResult> PutToCart(int prodId)
        {
            IEnumerable<ProductDTO> prod = await
                prodService
                .Get(prodId);
            cart.AddToCart(prod.FirstOrDefault());
            return Ok();
        }

        [HttpDelete]
        [Route("del")]
        public async Task<IHttpActionResult> DeleteFromCart(int prodId)
        {
            IEnumerable<ProductDTO> prod = await
                prodService
                .Get(prodId);
            cart.DeleteFromCart(prod.FirstOrDefault());
            return Ok();
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult ClearCart()
        {
            cart.ClearCart();
            return Ok();
        }




    }
}
