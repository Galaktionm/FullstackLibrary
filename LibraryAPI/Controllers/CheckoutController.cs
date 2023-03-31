
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;
using LibraryApp.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers {

    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase {

        private readonly CheckoutRepository repo;

        public CheckoutController(CheckoutRepository repo){
            this.repo=repo;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult getCheckouts(){
            return Ok(repo.getAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Checkout>> getCheckoutById(long id){
            return Ok(repo.findByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> addCheckout(AddCheckoutRequest request){
                var result=await repo.addCheckoutAsync(request);
                if(result.succeeded) {
                    return Ok(new { message="Checkout added"});
                } else {
                    return BadRequest(new { message="There was an error processing your request"});
                }
        
        }

    }

}