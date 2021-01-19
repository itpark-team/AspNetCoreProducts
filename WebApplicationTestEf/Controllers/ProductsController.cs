using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTestEf.Models;

namespace WebApplicationTestEf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /*
         * GET site.ru/api/products
         * GET site.ru/api/products/6
         * PUT site.ru/api/products/7
         * POST site.ru/api/products
         * DELETE site.ru/api/products/7
         */

        private ProductMarketContext db;

        public ProductsController()
        {
            db = new ProductMarketContext();
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, db.Products);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Product product, int id)
        {
            try
            {
                Product findProduct = db.Products.FirstOrDefault(s => s.Id == id);
                if (findProduct == null)
                {
                    throw new Exception("Product Not Found");
                }

                findProduct.Price = product.Price;
                findProduct.Name = product.Name;
                findProduct.IdCategory = product.IdCategory;
                db.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, findProduct);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Product findProduct = db.Products.FirstOrDefault(s => s.Id == id);
                if (findProduct == null)
                {
                    throw new Exception("Product Not Found");
                }

                db.Products.Remove(findProduct);
                db.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
