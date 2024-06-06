using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly IRepository<Category> _CategoryRepository;
        private readonly IConfiguration _configuration;


        public CategoryController(IRepository<Category> CategoryRepository, ILogger<CategoryController> logger, IConfiguration configuration)
        {
            _CategoryRepository = CategoryRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllCategorys
        //[Categoryize(Roles = "Admin, Category, Owner")]
        [HttpGet]
        [Route("GetAllCategorys")]
        public async Task<ActionResult<List<Category>>> GetAllCategorysAsync()
        {
            try
            {
                var Categorys = await _CategoryRepository.GetAllAsync();

                if (Categorys == null || Categorys.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Categorys
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Categorys."
                });
            }
        }


        //GetCategoryById
        //[Categoryize(Roles = "Admin, Category, Owner")]
        [HttpGet]
        [Route("GetCategoryById/{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Category Id"
                    });
                }
                var Category = await _CategoryRepository.GetAsync(Category => Category.CategoryID == id, false);


                if (Category == null)
                {
                    _logger.LogError("Category not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Category' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Category
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Category."
                });
            }
        }


      

        //CreateCategory
        //[Categoryize(Roles = "Category")]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<ActionResult<Category>> CreateCategoryAsync([FromBody] Category model)
        {
            try
            {
                if (model == null)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Null Object"
                    });
                }

                var createdCategory = await _CategoryRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdCategory.CategoryId }, Category);
                if (createdCategory == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Category"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdCategory
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Category."
                });
            }
        }


        //UpdateCategory
        //[Categoryize(Roles = "Category")]
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategoryAsync([FromBody] Category model)
        {
            try
            {
                if (model == null || model.CategoryID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Category = await _CategoryRepository.GetAsync(item => item.CategoryID == model.CategoryID, true);

                if (Category == null)
                {
                    _logger.LogError("Category not found with given Id");
                    return NotFound("Category not found");
                }



                var updateCategory = await _CategoryRepository.UpdateAsync(model);


                if (updateCategory != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Category updated successfully",
                        Category = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Category not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Category."
                });
            }
        }


        //DeleteCategory
        //[Categoryize(Roles = "Admin, Category")]
        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Category Id"
                    });
                }

                var Category = await _CategoryRepository.GetAsync(Category => Category.CategoryID == id, false);

                if (Category == null)
                {
                    _logger.LogError("Category not found with given Id");
                    return false;
                }

                var deleteStatus = await _CategoryRepository.DeleteAsync(Category);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Category deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Category Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Category."
                });
            }
        }
    }


}
