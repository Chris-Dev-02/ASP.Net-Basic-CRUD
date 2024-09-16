using Basic_CRUD.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basic_CRUD.Controllers
{
    [EnableCors("CustomCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly BasicCrudContext _dbContext;

        public CategoriesController(BasicCrudContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("list")]
        public IActionResult getCategoryList()
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories = _dbContext.Categories.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "OK", Response = categories });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }

        }

        [HttpGet]
        [Route("{idCategory:long}")]
        public IActionResult getCategoryByIndex(long idCategory)
        {
            Category category = _dbContext.Categories.Find(idCategory);

            if (category == null)
            {
                return BadRequest("Product not found");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "OK", Response = category });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }

        }

        [HttpPost]
        [Route("")]
        public IActionResult addCategory([FromBody] Category newCategory)
        {
            try
            {
                _dbContext.Categories.Add(newCategory);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{idCategory:long}")]
        public IActionResult updateCategoryByIndex(long idCategory, [FromBody] Category category)
        {
            Category foundCategory = _dbContext.Categories.Find(idCategory);
            if (foundCategory == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                foundCategory.Title = category.Title is null ? foundCategory.Title : category.Title;
                foundCategory.Description = category.Description is null ? foundCategory.Description : category.Description;

                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{idCategory:long}")]
        public IActionResult deleteCategoryByIndex(long idCategory)
        {
            Category category = _dbContext.Categories.Find(idCategory);
            if (category == null)
            {
                return BadRequest("Product not found");
            }
            try
            {
                _dbContext.Categories.Remove(category);
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
