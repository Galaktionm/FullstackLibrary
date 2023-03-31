using LibraryApp.API.Application.Commands;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.API.Controllers {

    [ApiController]
    [Route("api/operations")]

    public class OperationController : ControllerBase {

        private readonly IMediator mediator;
        private readonly DatabaseContext databaseContext;

        public OperationController(IMediator mediator, DatabaseContext databaseContext){
            this.mediator=mediator;
            this.databaseContext=databaseContext;
        }

        [HttpGet("review")]
        public IEnumerable<BookReview> getAllReviews() {
            IEnumerable<BookReview> reviews=databaseContext.Reviews.Include("user").AsEnumerable();
            return reviews;
        }


        [HttpPost("review/add")]
        public async Task<IActionResult> addReview(AddReviewRequest request){
            MediatorCommandResult result=await mediator.Send(new AddReviewCommand(request));
            if(result.succeeded) {
                return Ok( new { message="Review added"});
            } else {
                return BadRequest( new { message=result.message });
            }
        }

        [HttpPost("image/upload")]
        [Authorize(Roles ="Admin")]
         public async Task<IActionResult> uploadPicture(IFormFile image, long bookId){
             
            var book=await databaseContext.Books.FindAsync(bookId);
            if(book!=null){
                try {
                    using(FileStream stream=new FileStream("Images/"+image.FileName, FileMode.Create, FileAccess.Write)) {
                       await image.CopyToAsync(stream);
                    }
                    book.imagePath="Images/"+image.FileName;
                    await databaseContext.SaveChangesAsync();
                    return Ok(new { message="Image added" });
                } catch (Exception e) {
                  return BadRequest(new { message=e.Message });
                } 
            } else {
                return BadRequest(new { message="Book with such id not found"});
            }
         }

         [HttpGet("image")]
         public FileContentResult getPicture(String imagePath) {
            try {
            byte[] imageBytes=System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg");
            } catch ( FileNotFoundException exception ) {
                throw new HttpRequestException("Could not find Image");
            }
         }
        
    }

}