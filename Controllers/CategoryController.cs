using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories/")]
public class CategoryController : ControllerBase
{
    private static List<Category> categories = new List<Category>();

    //GET REQUEST : /api/categories ==> Read Categories
    [HttpGet]
    public IActionResult GetCategories([FromQuery] string searchValue = "")
    {
        var result = categories;
        if (!string.IsNullOrEmpty(searchValue))
        {
            result = categories.Where(c => !string.IsNullOrEmpty(c.CategoryName) && c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }
       var allCategories = result.Select(c => new GetAllCategoryDto
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            CategoryDescription = c.CategoryDescription
        }).ToList();
        return Ok(allCategories);
    }

    //POST REQUEST : /api/categories ==> Create a Categories
    [HttpPost]
    public IActionResult CreateCategories([FromBody] CategoryCreateDto categoryData)
    {
        if (string.IsNullOrEmpty(categoryData.CategoryName))
        {
            return BadRequest("Category Name is required and can not empty");
        }
    ;
        var newCategory = new Category
        {
            CategoryId = Guid.NewGuid(),
            CategoryName = categoryData.CategoryName,
            CategoryDescription = categoryData.CategoryDescription,
            CreatedAt = DateTime.UtcNow,
        };
        categories.Add(newCategory);
        return Created($"/api/categories/v1/{newCategory.CategoryId}", newCategory);
    }

    //PUT REQUEST : /api/categories/id ==> Update a Categories
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCategoryById(Guid id, [FromBody] CategoryUpdateDto categoryData)
    {
        var foundCategory = categories.FirstOrDefault(category => category.CategoryId == id);

        if (foundCategory == null)
        {
            return NotFound("Category With This Id Not Found");
        }

        if (categoryData == null)
        {
            return BadRequest("Category Data is Missing");
        }

        if (!string.IsNullOrEmpty(categoryData.CategoryName))
        {
            foundCategory.CategoryName = categoryData.CategoryName ?? foundCategory.CategoryName;
            foundCategory.CategoryDescription = categoryData.CategoryDescription ?? foundCategory.CategoryDescription;

            return NoContent();
        }

        return BadRequest("Category Update Failed");
    }

    private IActionResult Ok(string v, Category foundCategory)
    {
        throw new NotImplementedException();
    }


    //DELETE REQUEST : /api/categories/{id} ==> Delete a Categories
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCategoryById(Guid id)
    {
        var foundCategory = categories.FirstOrDefault(category => category.CategoryId == id);
        if (foundCategory == null)
        {
            return NotFound("Category With This Id Not Found");
        }
        categories.Remove(foundCategory);

        return NoContent();
    }


}