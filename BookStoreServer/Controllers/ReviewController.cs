using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IRepository<Review> _ReviewRepository;
        private readonly IConfiguration _configuration;


        public ReviewController(IRepository<Review> ReviewRepository, ILogger<ReviewController> logger, IConfiguration configuration)
        {
            _ReviewRepository = ReviewRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllReviews
        //[Reviewize(Roles = "Admin, Review, Owner")]
        [HttpGet]
        [Route("GetAllReviews")]
        public async Task<ActionResult<List<Review>>> GetAllReviewsAsync()
        {
            try
            {
                var Reviews = await _ReviewRepository.GetAllAsync();

                if (Reviews == null || Reviews.Count <= 0)
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
                    data = Reviews
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Reviews."
                });
            }
        }


        //GetReviewById
        //[Reviewize(Roles = "Admin, Review, Owner")]
        [HttpGet]
        [Route("GetReviewById/{id}", Name = "GetReviewById")]
        public async Task<ActionResult<Review>> GetReviewByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Review Id"
                    });
                }
                var Review = await _ReviewRepository.GetAsync(Review => Review.ReviewID == id, false);


                if (Review == null)
                {
                    _logger.LogError("Review not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Review."
                });
            }
        }


      

        //CreateReview
        //[Reviewize(Roles = "Review")]
        [HttpPost]
        [Route("CreateReview")]
        public async Task<ActionResult<Review>> CreateReviewAsync([FromBody] Review model)
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

                var createdReview = await _ReviewRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdReview.ReviewId }, Review);
                if (createdReview == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Review"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdReview
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Review."
                });
            }
        }


        //UpdateReview
        //[Reviewize(Roles = "Review")]
        [HttpPut]
        [Route("UpdateReview")]
        public async Task<ActionResult<Review>> UpdateReviewAsync([FromBody] Review model)
        {
            try
            {
                if (model == null || model.ReviewID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Review = await _ReviewRepository.GetAsync(item => item.ReviewID == model.ReviewID, true);

                if (Review == null)
                {
                    _logger.LogError("Review not found with given Id");
                    return NotFound("Review not found");
                }



                var updateReview = await _ReviewRepository.UpdateAsync(model);


                if (updateReview != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Review updated successfully",
                        Review = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Review not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Review."
                });
            }
        }


        //DeleteReview
        //[Reviewize(Roles = "Admin, Review")]
        [HttpDelete]
        [Route("DeleteReview/{id}")]
        public async Task<ActionResult<bool>> DeleteReviewAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Review Id"
                    });
                }

                var Review = await _ReviewRepository.GetAsync(Review => Review.ReviewID == id, false);

                if (Review == null)
                {
                    _logger.LogError("Review not found with given Id");
                    return false;
                }

                var deleteStatus = await _ReviewRepository.DeleteAsync(Review);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Review deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Review Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Review."
                });
            }
        }
    }

}
