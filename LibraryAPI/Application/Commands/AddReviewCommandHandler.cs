using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;
using LibraryApp.API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.API.Application.Commands {

    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, MediatorCommandResult>
    {
        private DatabaseContext databaseContext;
        private UserManager<User> userManager;

        public AddReviewCommandHandler(DatabaseContext databaseContext, UserManager<User> userManager){
            this.databaseContext=databaseContext;
            this.userManager=userManager;
        }

        public async Task<MediatorCommandResult> Handle(AddReviewCommand command, CancellationToken cancellationToken)
        {
            User? user=await userManager.FindByIdAsync(command.request.userId);
            if(user!=null){
                Book? book=await databaseContext.Books.FindAsync(command.request.bookId);
                if(book!=null){
                    BookReview review=new BookReview(user.Id, command.request.bookId, command.request.rating, command.request.review);
                    user.addReview(review);
                    await databaseContext.SaveChangesAsync();
                    return new MediatorCommandResult {
                        succeeded=true,
                        message="Update successful"
                    };
                } else {
                    return new MediatorCommandResult {
                          succeeded=false,
                          message=$"Book with id {command.request.bookId} not found"
                     };
                }
            }
            
            return new MediatorCommandResult {
                          succeeded=false,
                          message=$"User with id {command.request.userId} not found"
            };
        }
    }



}