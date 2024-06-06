using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartDetailStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly ILogger<CartDetailController> _logger;
        private readonly IRepository<CartDetail> _CartDetailRepository;
        private readonly IConfiguration _configuration;


        public CartDetailController(IRepository<CartDetail> CartDetailRepository, ILogger<CartDetailController> logger, IConfiguration configuration)
        {
            _CartDetailRepository = CartDetailRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllCartDetails
        //[CartDetailize(Roles = "Admin, CartDetail, Owner")]
        [HttpGet]
        [Route("GetAllCartDetails")]
        public async Task<ActionResult<List<CartDetail>>> GetAllCartDetailsAsync()
        {
            try
            {
                var CartDetails = await _CartDetailRepository.GetAllAsync();

                if (CartDetails == null || CartDetails.Count <= 0)
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
                    data = CartDetails
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching CartDetails."
                });
            }
        }


        //GetCartDetailById
        //[CartDetailize(Roles = "Admin, CartDetail, Owner")]
        [HttpGet]
        [Route("GetCartDetailById/{id}", Name = "GetCartDetailById")]
        public async Task<ActionResult<CartDetail>> GetCartDetailByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid CartDetail Id"
                    });
                }
                var CartDetail = await _CartDetailRepository.GetAsync(CartDetail => CartDetail.CartDetailID == id, false);


                if (CartDetail == null)
                {
                    _logger.LogError("CartDetail not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'CartDetail' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = CartDetail
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching CartDetail."
                });
            }
        }


        //CreateCartDetail
        //[CartDetailize(Roles = "CartDetail")]
        [HttpPost]
        [Route("CreateCartDetail")]
        public async Task<ActionResult<CartDetail>> CreateCartDetailAsync([FromBody] CartDetail model)
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

                var createdCartDetail = await _CartDetailRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdCartDetail.CartDetailId }, CartDetail);
                if (createdCartDetail == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create CartDetail"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdCartDetail
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating CartDetail."
                });
            }
        }


        //UpdateCartDetail
        //[CartDetailize(Roles = "CartDetail")]
        [HttpPut]
        [Route("UpdateCartDetail")]
        public async Task<ActionResult<CartDetail>> UpdateCartDetailAsync([FromBody] CartDetail model)
        {
            try
            {
                if (model == null || model.CartDetailID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var CartDetail = await _CartDetailRepository.GetAsync(item => item.CartDetailID == model.CartDetailID, true);

                if (CartDetail == null)
                {
                    _logger.LogError("CartDetail not found with given Id");
                    return NotFound("CartDetail not found");
                }



                var updateCartDetail = await _CartDetailRepository.UpdateAsync(model);


                if (updateCartDetail != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "CartDetail updated successfully",
                        CartDetail = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "CartDetail not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating CartDetail."
                });
            }
        }


        //DeleteCartDetail
        //[CartDetailize(Roles = "Admin, CartDetail")]
        [HttpDelete]
        [Route("DeleteCartDetail/{id}")]
        public async Task<ActionResult<bool>> DeleteCartDetailAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid CartDetail Id"
                    });
                }

                var CartDetail = await _CartDetailRepository.GetAsync(CartDetail => CartDetail.CartDetailID == id, false);

                if (CartDetail == null)
                {
                    _logger.LogError("CartDetail not found with given Id");
                    return false;
                }

                var deleteStatus = await _CartDetailRepository.DeleteAsync(CartDetail);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "CartDetail deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "CartDetail Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting CartDetail."
                });
            }
        }
    }
}

