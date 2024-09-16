using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Basic_CRUD.Models;

using Microsoft.AspNetCore.Cors;

namespace Basic_CRUD.Controllers
{
    [EnableCors("CustomCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {   
        public readonly BasicCrudContext _dbContext;

        public ProductsController(BasicCrudContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("list")]
        public IActionResult getProductList()
        {
            List<Product> products = new List<Product>();

            try
            {
                //products = _dbContext.Products.ToList();
                products = _dbContext.Products.Include(c => c.CategoryNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "OK", Response = products});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{idProduct:long}")]
        public IActionResult getProductByIndex(long idProduct)
        {
            Product product = _dbContext.Products.Find(idProduct);

            if(product == null)
            {
                return BadRequest("Product not found");
            }

            try
            {
                product = _dbContext.Products.Include(c => c.CategoryNavigation).Where(p => p.IdProducts == idProduct).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "OK", Response = product});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult addProduct([FromBody] Product newProduct)
        {
            try
            {
                _dbContext.Products.Add(newProduct);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{idProduct:long}")]
        public IActionResult updateProductByIndex(long idProduct, [FromBody] Product product)
        {
            Product foundProduct = _dbContext.Products.Find(idProduct);
            if (foundProduct == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                foundProduct.Barcode = product.Barcode is null ? foundProduct.Barcode : product.Barcode;
                foundProduct.Description = product.Description is null ? foundProduct.Description : product.Description;
                foundProduct.Brand = product.Brand is null ? foundProduct.Brand : product.Brand;
                foundProduct.Category = product.Category is null ? foundProduct.Category : product.Category;
                foundProduct.Price = product.Price is null ? foundProduct.Price : product.Price;

                _dbContext.Products.Update(foundProduct);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{idProduct:long}")]
        public IActionResult deleteProductByIndex(long idProduct)
        {
            Product product = _dbContext.Products.Find(idProduct);
            if (product == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }
    }
}
