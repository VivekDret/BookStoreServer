using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IRepository<Payment> _PaymentRepository;
        private readonly IConfiguration _configuration;


        public PaymentController(IRepository<Payment> PaymentRepository, ILogger<PaymentController> logger, IConfiguration configuration)
        {
            _PaymentRepository = PaymentRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllPayments
        //[Paymentize(Roles = "Admin, Payment, Owner")]
        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<ActionResult<List<Payment>>> GetAllPaymentsAsync()
        {
            try
            {
                var Payments = await _PaymentRepository.GetAllAsync();

                if (Payments == null || Payments.Count <= 0)
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
                    data = Payments
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Payments."
                });
            }
        }


        //GetPaymentById
        //[Paymentize(Roles = "Admin, Payment, Owner")]
        [HttpGet]
        [Route("GetPaymentById/{id}", Name = "GetPaymentById")]
        public async Task<ActionResult<Payment>> GetPaymentByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Payment Id"
                    });
                }
                var Payment = await _PaymentRepository.GetAsync(Payment => Payment.PaymentId == id, false);


                if (Payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Payment' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Payment
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Payment."
                });
            }
        }



        //CreatePayment
        //[Paymentize(Roles = "Payment")]
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<ActionResult<Payment>> CreatePaymentAsync([FromBody] Payment model)
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

                var createdPayment = await _PaymentRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdPayment.PaymentId }, Payment);
                if (createdPayment == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Payment"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdPayment
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Payment."
                });
            }
        }


        //UpdatePayment
        //[Paymentize(Roles = "Payment")]
        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<ActionResult<Payment>> UpdatePaymentAsync([FromBody] Payment model)
        {
            try
            {
                if (model == null || model.PaymentId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Payment = await _PaymentRepository.GetAsync(item => item.PaymentId == model.PaymentId, true);

                if (Payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                    return NotFound("Payment not found");
                }



                var updatePayment = await _PaymentRepository.UpdateAsync(model);


                if (updatePayment != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Payment updated successfully",
                        Payment = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Payment not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Payment."
                });
            }
        }


        //DeletePayment
        //[Paymentize(Roles = "Admin, Payment")]
        [HttpDelete]
        [Route("DeletePayment/{id}")]
        public async Task<ActionResult<bool>> DeletePaymentAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Payment Id"
                    });
                }

                var Payment = await _PaymentRepository.GetAsync(Payment => Payment.PaymentId == id, false);

                if (Payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                    return false;
                }

                var deleteStatus = await _PaymentRepository.DeleteAsync(Payment);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Payment deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Payment Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Payment."
                });
            }
        }
    }
}

