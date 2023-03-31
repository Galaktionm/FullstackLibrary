
using LibraryApp.API.Application;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;
using LibraryApp.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers {   
    
    [ApiController]
    [Route("api/author")]

    public class AuthorController : ControllerBase {

        private readonly AuthorRepository repo;

        public AuthorController(AuthorRepository repo){
            this.repo=repo;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<Author>>> getAll(int pageIndex=0, int pageSize=10){
            return await ApiResult<Author>.CreateAsync(repo.getAll(), pageIndex, pageSize);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResult<Author>>> getByNameAsync(String query, int pageIndex=0, int pageSize=10) {
            return await ApiResult<Author>.CreateAsync(repo.searchByNameAsync(query), pageIndex, pageSize);
        }

        [HttpPost("add")]
        public async Task<IActionResult> addAuthor(AddAuthorRequest request) {
            try {
                await repo.addAuthor(request);
                return Ok();
            } catch {
                return BadRequest();
            }

        }

        [HttpDelete("delete")]
        public IActionResult deleteAuthor(long id) {
            try {
                repo.deleteAuthor(id);
                return Ok();
            } catch (ArgumentNullException ex) {
                return BadRequest();
            }
        }

        

    }

}
