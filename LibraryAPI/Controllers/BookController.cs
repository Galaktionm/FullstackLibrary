using LibraryApp.API.DTO;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.API.Application;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Application.Serializers;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.API.Controllers {
    
    [ApiController]
    [Route("api/book")]

    public class BookController : ControllerBase {

        private readonly BookRepository repo;
        private readonly IHttpContextAccessor accessor;

        public BookController(BookRepository repo, IHttpContextAccessor accessor){
            this.repo=repo;
            this.accessor=accessor;
        }

        [HttpGet("all")]
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult<ApiResult<Book>>> getBooksAll(int pageIndex, int pageSize){
            return Ok(await ApiResult<Book>.CreateAsync(repo.getAll(), pageIndex, pageSize));
        }

        [HttpGet("search")]
        [Authorize(Roles ="User,Admin")]
        public async Task<ActionResult<ApiResult<Book>>> searchBooks(String query, int pageIndex, int pageSize) {         
             return await ApiResult<Book>.CreateAsync(repo.searchByTitle(query), pageIndex, pageSize);
        }

        [HttpGet("search/summary")]
        [Authorize(Roles ="User,Admin")]
        public IQueryable<Book> searchBookSummary(String query) {         
             return repo.searchByTitle(query);
        }



        [HttpGet("{id}")]
        [Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> searchBookById(long id){
            return Ok(await repo.findByIdWithReviewsAsync(id));
        }

        [HttpPost("update")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> updateBook(UpdateBookRequest request){
            try {
                await repo.updateBook(request);
                return Ok();
            } catch (Exception e) {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> deleteBook(long bookId){
           try {
              await repo.deleteEntityAsync(bookId);
              return Ok(new { message="Deletion successful"});
           } catch (ArgumentNullException exception) {
              return BadRequest( new { mesage="Book with requested id does not exist"});
           }
        }

        [HttpPost("add")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> addBookAsync(AddBookRequest request){
            try {
                await repo.addBookAsync(request);
                return Ok( new { message="Book added"});
            } catch (ArgumentNullException exception) {
                return BadRequest(new { message=exception.Message});
            }
        }
    }
}