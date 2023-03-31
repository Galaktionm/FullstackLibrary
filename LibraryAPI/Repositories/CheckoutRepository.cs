using LibraryApp.API.DTO;
using LibraryApp.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryApp.API.Application;
using Microsoft.AspNetCore.Identity;
using MediatR;
using LibraryApp.API.Application.Commands;

namespace LibraryApp.API.Data.Repositories {

    public class CheckoutRepository : IGenericRepository<Checkout>
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<User> userManager;

        private readonly IMediator mediator;

        public CheckoutRepository(DatabaseContext databaseContext, UserManager<User> userManager, IMediator mediator){
            this.databaseContext=databaseContext;
            this.userManager=userManager;
            this.mediator=mediator;
        }
        public IQueryable<Checkout> getAll()
        {
            return databaseContext.Checkouts.AsNoTracking()
                .Include<Checkout>("user").Include<Checkout>("books");
        }

        public Task<Checkout?> findByIdAsync(long id)
        {
            return databaseContext.Checkouts.AsNoTracking()
                .Include<Checkout>("user").Include<Checkout>("books").FirstOrDefaultAsync(chechout=>chechout.checkoutId==id);
        }

        public async Task deleteEntityAsync(long id)
        {
            Checkout checkout=databaseContext.Checkouts.Find(id);
            databaseContext.Remove(checkout);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<MediatorCommandResult> addCheckoutAsync(AddCheckoutRequest request){
            User? user=await userManager.FindByIdAsync(request.userId);
            Checkout checkout=new Checkout(DateTime.Parse(request.until).ToUniversalTime());
            user.addCheckout(checkout);
            MediatorCommandResult result=await mediator.Send(new PlaceCheckoutCommand(request.bookIds, checkout));
            return result;
        }
    }

}