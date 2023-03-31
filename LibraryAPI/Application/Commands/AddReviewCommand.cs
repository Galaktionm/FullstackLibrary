
using LibraryApp.API.Data.Entities;
using LibraryApp.API.DTO;
using MediatR;

namespace LibraryApp.API.Application.Commands {

    public record AddReviewCommand : IRequest<MediatorCommandResult> {

        public AddReviewRequest request { get; set;}

        public AddReviewCommand(AddReviewRequest request){
            this.request=request;
        }
        
    }



}