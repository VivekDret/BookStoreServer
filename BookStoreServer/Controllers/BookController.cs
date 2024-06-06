using BookStoreServer.Interface;
using BookStoreServer.Mappers;
using BookStoreServer.Models;
using BookStoreServer.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IRepository<Book> _BookRepository;
        private readonly IConfiguration _configuration;
        private static readonly string UploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");



        public BookController(IRepository<Book> BookRepository, ILogger<BookController> logger, IConfiguration configuration)
        {
            _BookRepository = BookRepository;
            _configuration = configuration;
            _logger = logger;
        }


        //GetAllBooks
        //[Bookize(Roles = "Admin, Book, Owner")]
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult<List<Book>>> GetAllBooksAsync()
        {
            try
            {
                var Books = await _BookRepository.GetAllAsync();

                if (Books == null || Books.Count <= 0)
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
                    data = Books
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Books."
                });
            }
        }


        //GetBookById
        //[Bookize(Roles = "Admin, Book, Owner")]
        [HttpGet]
        [Route("GetBookById/{id}", Name = "GetBookById")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Book Id"
                    });
                }
                var Book = await _BookRepository.GetAsync(Book => Book.BookID == id, false);


                if (Book == null)
                {
                    _logger.LogError("Book not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Book' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Book
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Book."
                });
            }
        }


        //GetBookByName
        //[Bookize(Roles = "Admin, Book, Owner")]
        [HttpGet]
        [Route("GetBookByTitle/{title}")]
        public async Task<ActionResult<Book>> GetBookByNameAsync(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Book Email"
                    });
                }

                var Books = await _BookRepository.GetAllAsync();
                var Book = Books.Where(Book => Book.BookTitle.Contains(title)).FirstOrDefault();


                if (Book == null)
                {
                    _logger.LogError("Book not found with given name");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Book' with Email: {title} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = Book
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Book."
                });
            }
        }

        //CreateBook
        //[Bookize(Roles = "Book")]
        [HttpPost]
        [Route("CreateBook")]
        public async Task<ActionResult<Book>> CreateBookAsync([FromForm] BookDTO model)
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

                if (model.File == null || model.File.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                string imageUrl = null;
                try
                {
                    if (!Directory.Exists(UploadDirectory))
                    {
                        Directory.CreateDirectory(UploadDirectory);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
                    string filePath = Path.Combine(UploadDirectory, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }

                    imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{uniqueFileName}";
                }catch(Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message.ToString());
                }

                model.BookImageLink = imageUrl;

                RegisterToBook bookToMap = new RegisterToBook(model);
                Book bookToAdd = bookToMap.GetBook();


                var createdBook = await _BookRepository.CreateAsync(bookToAdd);

                //return CreatedAtRoute("GetStudentById", new { id = createdBook.BookId }, Book);
                if (createdBook == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create Book"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdBook
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Book."
                });
            }
        }


        //UpdateBook
        //[Bookize(Roles = "Book")]
        [HttpPut]
        [Route("UpdateBook")]
        public async Task<ActionResult<Book>> UpdateBookAsync([FromBody] Book model)
        {
            try
            {
                if (model == null || model.BookID <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Book = await _BookRepository.GetAsync(item => item.BookID == model.BookID, true);

                if (Book == null)
                {
                    _logger.LogError("Book not found with given Id");
                    return NotFound("Book not found");
                }



                var updateBook = await _BookRepository.UpdateAsync(model);


                if (updateBook != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Book updated successfully",
                        Book = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Book not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Book."
                });
            }
        }


        //DeleteBook
        //[Bookize(Roles = "Admin, Book")]
        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<ActionResult<bool>> DeleteBookAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Book Id"
                    });
                }

                var Book = await _BookRepository.GetAsync(Book => Book.BookID == id, false);

                if (Book == null)
                {
                    _logger.LogError("Book not found with given Id");
                    return false;
                }

                var deleteStatus = await _BookRepository.DeleteAsync(Book);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Book deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Book Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Book."
                });
            }
        }
    }
}

