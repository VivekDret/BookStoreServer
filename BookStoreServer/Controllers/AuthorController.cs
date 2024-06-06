using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IRepository<Author> _authorRepository;
        private readonly IConfiguration _configuration;


        public AuthorController(IRepository<Author> authorRepository, ILogger<AuthorController> logger, IConfiguration configuration)
        {
            _authorRepository = authorRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllAuthors
        //[Authorize(Roles = "Admin, Author, Owner")]
        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<ActionResult<List<Author>>> GetAllAuthorsAsync()
        {
            try
            {
                var Authors = await _authorRepository.GetAllAsync();

                if (Authors == null || Authors.Count <= 0)
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
                    data = Authors
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Authors."
                });
            }
        }


        //GetAuthorById
        //[Authorize(Roles = "Admin, Author, Owner")]
        [HttpGet]
        [Route("GetAuthorById/{id}", Name = "GetAuthorById")]
        public async Task<ActionResult<Author>> GetAuthorByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Author Id"
                    });
                }
                var Author = await _authorRepository.GetAsync(Author => Author.AuthorID == id, false);


                if (Author == null)
                {
                    _logger.LogError("Author not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Author' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Author
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Author."
                });
            }
        }


        //GetAuthorByName
        //[Authorize(Roles = "Admin, Author, Owner")]
        [HttpGet]
        [Route("GetAuthorByEmail/{email}")]
        public async Task<ActionResult<Author>> GetAuthorByNameAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Author Email"
                    });
                }

                var Authors = await _authorRepository.GetAllAsync();
                var Author = Authors.Where(Author => Author.AuthorEmail.ToLower() == email.ToLower()).FirstOrDefault();


                if (Author == null)
                {
                    _logger.LogError("Author not found with given name");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Author' with Email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Author
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Author."
                });
            }
        }

        //CreateAuthor
        //[Authorize(Roles = "Author")]
        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<ActionResult<Author>> CreateAuthorAsync([FromBody] Author model)
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

                var createdAuthor = await _authorRepository.CreateAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdAuthor.AuthorId }, Author);
                if (createdAuthor == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Author"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdAuthor
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Author."
                });
            }
        }


        //UpdateAuthor
        //[Authorize(Roles = "Author")]
        [HttpPut]
        [Route("UpdateAuthor")]
        public async Task<ActionResult<Author>> UpdateAuthorAsync([FromBody] Author model)
        {
            try
            {
                if (model == null || model.AuthorID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Author = await _authorRepository.GetAsync(item => item.AuthorID == model.AuthorID, true);

                if (Author == null)
                {
                    _logger.LogError("Author not found with given Id");
                    return NotFound("Author not found");
                }



                var updateAuthor = await _authorRepository.UpdateAsync(model);


                if (updateAuthor != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Author updated successfully",
                        Author = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Author not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Author."
                });
            }
        }


        //DeleteAuthor
        //[Authorize(Roles = "Admin, Author")]
        [HttpDelete]
        [Route("DeleteAuthor/{id}")]
        public async Task<ActionResult<bool>> DeleteAuthorAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Author Id"
                    });
                }

                var Author = await _authorRepository.GetAsync(Author => Author.AuthorID == id, false);

                if (Author == null)
                {
                    _logger.LogError("Author not found with given Id");
                    return false;
                }

                var deleteStatus = await _authorRepository.DeleteAsync(Author);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Author deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Author Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Author."
                });
            }
        }
    }
    }
