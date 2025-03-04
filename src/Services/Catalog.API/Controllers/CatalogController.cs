
using Catalog.API.Interfaces.Manager;
using Catalog.API.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        [ResponseCache(Duration = 30)]
        //[ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Product Loaded Successfully", products);
            }
            catch (Exception ex)
            {
               return CustomResult("Error Occured", ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(string id)
        //{
        //    var product = await prouctManager.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}
        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateProduct(Product product)
        //{
        //    await prouctManager.Create(product);
        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(string id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }
        //    await _productRepository.Update(product);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(string id)
        //{
        //    var product = await _productRepository.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    await _productRepository.Delete(product);
        //    return NoContent();
        //}
    }
}
