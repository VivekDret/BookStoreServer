using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RefundStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly ILogger<RefundController> _logger;
        private readonly IRepository<Refund> _RefundRepository;
        private readonly IConfiguration _configuration;


        public RefundController(IRepository<Refund> RefundRepository, ILogger<RefundController> logger, IConfiguration configuration)
        {
            _RefundRepository = RefundRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllRefunds
        //[Refundize(Roles = "Admin, Refund, Owner")]
        [HttpGet]
        [Route("GetAllRefunds")]
        public async Task<ActionResult<List<Refund>>> GetAllRefundsAsync()
        {
            try
            {
                var Refunds = await _RefundRepository.GetAllAsync();

                if (Refunds == null || Refunds.Count <= 0)
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
                    data = Refunds
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Refunds."
                });
            }
        }


        //GetRefundById
        //[Refundize(Roles = "Admin, Refund, Owner")]
        [HttpGet]
        [Route("GetRefundById/{id}", Name = "GetRefundById")]
        public async Task<ActionResult<Refund>> GetRefundByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Refund Id"
                    });
                }
                var Refund = await _RefundRepository.GetAsync(Refund => Refund.RefundId == id, false);


                if (Refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Refund' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Refund
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Refund."
                });
            }
        }


       

        //CreateRefund
        //[Refundize(Roles = "Refund")]
        [HttpPost]
        [Route("CreateRefund")]
        public async Task<ActionResult<Refund>> CreateRefundAsync([FromBody] Refund model)
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

                var createdRefund = await _RefundRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdRefund.RefundId }, Refund);
                if (createdRefund == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Refund"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdRefund
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Refund."
                });
            }
        }


        //UpdateRefund
        //[Refundize(Roles = "Refund")]
        [HttpPut]
        [Route("UpdateRefund")]
        public async Task<ActionResult<Refund>> UpdateRefundAsync([FromBody] Refund model)
        {
            try
            {
                if (model == null || model.RefundId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Refund = await _RefundRepository.GetAsync(item => item.RefundId == model.RefundId, true);

                if (Refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                    return NotFound("Refund not found");
                }



                var updateRefund = await _RefundRepository.UpdateAsync(model);


                if (updateRefund != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Refund updated successfully",
                        Refund = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Refund not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Refund."
                });
            }
        }


        //DeleteRefund
        //[Refundize(Roles = "Admin, Refund")]
        [HttpDelete]
        [Route("DeleteRefund/{id}")]
        public async Task<ActionResult<bool>> DeleteRefundAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Refund Id"
                    });
                }

                var Refund = await _RefundRepository.GetAsync(Refund => Refund.RefundId == id, false);

                if (Refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                    return false;
                }

                var deleteStatus = await _RefundRepository.DeleteAsync(Refund);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Refund deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Refund Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Refund."
                });
            }
        }
    }
}

