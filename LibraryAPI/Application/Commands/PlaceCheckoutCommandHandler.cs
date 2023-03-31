
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;
using MediatR;

namespace LibraryApp.API.Application.Commands {
    public class PlaceCheckoutCommandHandler : IRequestHandler<PlaceCheckoutCommand, MediatorCommandResult>
    {
        private readonly BookRepository repo;

        public PlaceCheckoutCommandHandler(BookRepository repo) {
            this.repo=repo;
        }
        public async Task<MediatorCommandResult> Handle(PlaceCheckoutCommand request, CancellationToken cancellationToken)
        {
                try {
                    await repo.decrementAvailableAsync(request.bookIds, request.checkout);
                    return new MediatorCommandResult {
                        succeeded=true,
                        message="Checkouts added"
                    };
                } catch (InvalidOperationException exception) {
                    throw exception;
                }           
        }
    }
}