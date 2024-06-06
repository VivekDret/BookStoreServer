using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderDetailStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly IRepository<OrderDetail> _OrderDetailRepository;
        private readonly IConfiguration _configuration;


        public OrderDetailController(IRepository<OrderDetail> OrderDetailRepository, ILogger<OrderDetailController> logger, IConfiguration configuration)
        {
            _OrderDetailRepository = OrderDetailRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllOrderDetails
        //[OrderDetailize(Roles = "Admin, OrderDetail, Owner")]
        [HttpGet]
        [Route("GetAllOrderDetails")]
        public async Task<ActionResult<List<OrderDetail>>> GetAllOrderDetailsAsync()
        {
            try
            {
                var OrderDetails = await _OrderDetailRepository.GetAllAsync();

                if (OrderDetails == null || OrderDetails.Count <= 0)
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
                    data = OrderDetails
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching OrderDetails."
                });
            }
        }


        //GetOrderDetailById
        //[OrderDetailize(Roles = "Admin, OrderDetail, Owner")]
        [HttpGet]
        [Route("GetOrderDetailById/{id}", Name = "GetOrderDetailById")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid OrderDetail Id"
                    });
                }
                var OrderDetail = await _OrderDetailRepository.GetAsync(OrderDetail => OrderDetail.OrderDetailID == id, false);


                if (OrderDetail == null)
                {
                    _logger.LogError("OrderDetail not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'OrderDetail' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = OrderDetail
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching OrderDetail."
                });
            }
        }



        //CreateOrderDetail
        //[OrderDetailize(Roles = "OrderDetail")]
        [HttpPost]
        [Route("CreateOrderDetail")]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetailAsync([FromBody] OrderDetail model)
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

                var createdOrderDetail = await _OrderDetailRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdOrderDetail.OrderDetailId }, OrderDetail);
                if (createdOrderDetail == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create OrderDetail"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdOrderDetail
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating OrderDetail."
                });
            }
        }


        //UpdateOrderDetail
        //[OrderDetailize(Roles = "OrderDetail")]
        [HttpPut]
        [Route("UpdateOrderDetail")]
        public async Task<ActionResult<OrderDetail>> UpdateOrderDetailAsync([FromBody] OrderDetail model)
        {
            try
            {
                if (model == null || model.OrderDetailID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var OrderDetail = await _OrderDetailRepository.GetAsync(item => item.OrderDetailID == model.OrderDetailID, true);

                if (OrderDetail == null)
                {
                    _logger.LogError("OrderDetail not found with given Id");
                    return NotFound("OrderDetail not found");
                }



                var updateOrderDetail = await _OrderDetailRepository.UpdateAsync(model);


                if (updateOrderDetail != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "OrderDetail updated successfully",
                        OrderDetail = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "OrderDetail not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating OrderDetail."
                });
            }
        }


        //DeleteOrderDetail
        //[OrderDetailize(Roles = "Admin, OrderDetail")]
        [HttpDelete]
        [Route("DeleteOrderDetail/{id}")]
        public async Task<ActionResult<bool>> DeleteOrderDetailAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid OrderDetail Id"
                    });
                }

                var OrderDetail = await _OrderDetailRepository.GetAsync(OrderDetail => OrderDetail.OrderDetailID == id, false);

                if (OrderDetail == null)
                {
                    _logger.LogError("OrderDetail not found with given Id");
                    return false;
                }

                var deleteStatus = await _OrderDetailRepository.DeleteAsync(OrderDetail);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "OrderDetail deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "OrderDetail Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting OrderDetail."
                });
            }
        }
    }
}

