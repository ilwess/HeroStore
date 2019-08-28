using BLL.Abstract;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace HeroStore.Areas.Shop
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
            this.productService.GetAll();
        }

        //public ProductController()
        //{

        //}
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get()
        {
            var prods = productService.GetAll();

            return Ok(prods);
        }

        //public async Task<ProductDTO> Get(int id)
        //{
        //    var prod = await productService.Get(id);
        //    return prod.FirstOrDefault();
        //}

    }
}
