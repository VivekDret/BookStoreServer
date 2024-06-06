using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRepository<OrderTbl> _OrderRepository;
        private readonly IConfiguration _configuration;


        public OrderController(IRepository<OrderTbl> OrderRepository, ILogger<OrderController> logger, IConfiguration configuration)
        {
            _OrderRepository = OrderRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllOrders
        //[Orderize(Roles = "Admin, Order, Owner")]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<List<OrderTbl>>> GetAllOrdersAsync()
        {
            try
            {
                var Orders = await _OrderRepository.GetAllAsync();

                if (Orders == null || Orders.Count <= 0)
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
                    data = Orders
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Orders."
                });
            }
        }


        //GetOrderById
        //[Orderize(Roles = "Admin, Order, Owner")]
        [HttpGet]
        [Route("GetOrderById/{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderTbl>> GetOrderByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Order Id"
                    });
                }
                var Order = await _OrderRepository.GetAsync(Order => Order.OrderID == id, false);


                if (Order == null)
                {
                    _logger.LogError("Order not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Order' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Order
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Order."
                });
            }
        }


     

        //CreateOrder
        //[Orderize(Roles = "Order")]
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult<OrderTbl>> CreateOrderAsync([FromBody] OrderTbl model)
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

                var createdOrder = await _OrderRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdOrder.OrderId }, Order);
                if (createdOrder == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Order"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdOrder
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Order."
                });
            }
        }


        //UpdateOrder
        //[Orderize(Roles = "Order")]
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<ActionResult<OrderTbl>> UpdateOrderAsync([FromBody] OrderTbl model)
        {
            try
            {
                if (model == null || model.OrderID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Order = await _OrderRepository.GetAsync(item => item.OrderID == model.OrderID, true);

                if (Order == null)
                {
                    _logger.LogError("Order not found with given Id");
                    return NotFound("Order not found");
                }



                var updateOrder = await _OrderRepository.UpdateAsync(model);


                if (updateOrder != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Order updated successfully",
                        Order = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Order not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Order."
                });
            }
        }


        //DeleteOrder
        //[Orderize(Roles = "Admin, Order")]
        [HttpDelete]
        [Route("DeleteOrder/{id}")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Order Id"
                    });
                }

                var Order = await _OrderRepository.GetAsync(Order => Order.OrderID == id, false);

                if (Order == null)
                {
                    _logger.LogError("Order not found with given Id");
                    return false;
                }

                var deleteStatus = await _OrderRepository.DeleteAsync(Order);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Order deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Order Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Order."
                });
            }
        }
    }
}

