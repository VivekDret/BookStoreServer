using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IRepository<Cart> _CartRepository;
        private readonly IConfiguration _configuration;


        public CartController(IRepository<Cart> CartRepository, ILogger<CartController> logger, IConfiguration configuration)
        {
            _CartRepository = CartRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllCarts
        //[Cartize(Roles = "Admin, Cart, Owner")]
        [HttpGet]
        [Route("GetAllCarts")]
        public async Task<ActionResult<List<Cart>>> GetAllCartsAsync()
        {
            try
            {
                var Carts = await _CartRepository.GetAllAsync();

                if (Carts == null || Carts.Count <= 0)
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
                    data = Carts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Carts."
                });
            }
        }


        //GetCartById
        //[Cartize(Roles = "Admin, Cart, Owner")]
        [HttpGet]
        [Route("GetCartById/{id}", Name = "GetCartById")]
        public async Task<ActionResult<Cart>> GetCartByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Cart Id"
                    });
                }
                var Cart = await _CartRepository.GetAsync(Cart => Cart.CartID == id, false);


                if (Cart == null)
                {
                    _logger.LogError("Cart not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Cart' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Cart
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Cart."
                });
            }
        }


      

        //CreateCart
        //[Cartize(Roles = "Cart")]
        [HttpPost]
        [Route("CreateCart")]
        public async Task<ActionResult<Cart>> CreateCartAsync([FromBody] Cart model)
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

                var createdCart = await _CartRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdCart.CartId }, Cart);
                if (createdCart == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Cart"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdCart
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Cart."
                });
            }
        }


        //UpdateCart
        //[Cartize(Roles = "Cart")]
        [HttpPut]
        [Route("UpdateCart")]
        public async Task<ActionResult<Cart>> UpdateCartAsync([FromBody] Cart model)
        {
            try
            {
                if (model == null || model.CartID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Cart = await _CartRepository.GetAsync(item => item.CartID == model.CartID, true);

                if (Cart == null)
                {
                    _logger.LogError("Cart not found with given Id");
                    return NotFound("Cart not found");
                }



                var updateCart = await _CartRepository.UpdateAsync(model);


                if (updateCart != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Cart updated successfully",
                        Cart = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Cart not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Cart."
                });
            }
        }


        //DeleteCart
        //[Cartize(Roles = "Admin, Cart")]
        [HttpDelete]
        [Route("DeleteCart/{id}")]
        public async Task<ActionResult<bool>> DeleteCartAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Cart Id"
                    });
                }

                var Cart = await _CartRepository.GetAsync(Cart => Cart.CartID == id, false);

                if (Cart == null)
                {
                    _logger.LogError("Cart not found with given Id");
                    return false;
                }

                var deleteStatus = await _CartRepository.DeleteAsync(Cart);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Cart deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Cart Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Cart."
                });
            }
        }
    }
}

