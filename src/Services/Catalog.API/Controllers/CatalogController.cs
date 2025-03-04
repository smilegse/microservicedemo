
using Catalog.API.Interfaces.Manager;
using Catalog.API.Manager;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products =  _productManager.GetAll();
            return Ok(products);
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
