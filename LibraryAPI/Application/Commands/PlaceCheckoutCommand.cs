
using LibraryApp.API.Data.Entities;
using MediatR;

namespace LibraryApp.API.Application.Commands {

    public record PlaceCheckoutCommand : IRequest<MediatorCommandResult> {
        public long[] bookIds { get; set; }
        public Checkout checkout { get; set; }
        public PlaceCheckoutCommand(long[] bookIds, Checkout checkout){
            this.bookIds=bookIds;
            this.checkout=checkout;
        }
    }
}