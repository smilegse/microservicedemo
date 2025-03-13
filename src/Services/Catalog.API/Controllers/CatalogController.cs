
using Catalog.API.Interfaces.Manager;
using Catalog.API.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
                return CustomResult("Products Loaded Successfully.", products);
            }
            catch (Exception ex)
            {
               return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);
                if (isSaved)
                {
                    return CustomResult("Product Created Successfully.", product, HttpStatusCode.Created);
                }
                else 
                {
                    return CustomResult("Product Saved Failed.", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if(string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Product Id is Required.", HttpStatusCode.NotFound);
                }

                bool isUpdated = _productManager.Update(product.Id, product);
                if (isUpdated)
                {
                    return CustomResult("Product Updated Successfully.", product, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Update Failed.", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                bool isDeleted = _productManager.Delete(id);
                if (isDeleted)
                {
                    return CustomResult("Product Deleted Successfully.", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Deleted Failed.", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = _productManager.GetById(id);               
                return CustomResult("Product Loaded Successfully.", product);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }           
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                return CustomResult("Products Loaded Successfully.", products);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
