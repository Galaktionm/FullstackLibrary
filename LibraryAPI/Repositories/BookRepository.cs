using LibraryApp.API.DTO;
using LibraryApp.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryApp.API.Application;

namespace LibraryApp.API.Data.Repositories {

    public class BookRepository : IGenericRepository<Book>
    {
        private readonly DatabaseContext databaseContext;

        public BookRepository(DatabaseContext databaseContext){
            this.databaseContext=databaseContext;
        }

        public IQueryable<Book> getAll(){
            return databaseContext.Books.AsNoTracking().Include<Book>("Authors").OrderBy(book=>book.imagePath==null);
        }

        public async Task<Book?> findByIdAsync(long id)
        {
            return await databaseContext.Books
                      .Include<Book>("Authors")
                      .FirstOrDefaultAsync(book=>book.bookid==id);
        }

        public async Task<Book?> findByIdWithReviewsAsync(long id)
        {
            return await databaseContext.Books
                      .AsNoTracking()
                      .Include<Book>("Authors")
                      .Include<Book>("Reviews.user")
                      .FirstOrDefaultAsync(book=>book.bookid==id);
        }

        public async Task deleteEntityAsync(long id)
        {
            Book book=await findByIdAsync(id);
            databaseContext.Books.Remove(book);
            await databaseContext.SaveChangesAsync();
        }
        /// <summary>
        ///   Search data using a specific keyword
        /// </summary>
        public IQueryable<Book> searchByTitle(string title)
        {
            return databaseContext.Books.Include<Book>("Authors")
                   .Where(book=>book.title.ToLower().Contains(title.ToLower()));
        }
        public async Task updateBook(UpdateBookRequest request){
            Book book=await databaseContext.Books.FirstOrDefaultAsync<Book>(book=>book.bookid==request.bookId);
            if(book==null){
                throw new Exception();
            }

            if(request.title!=null){ book.title=request.title; }

            if(request.description!=null){ book.description=request.description; }

            if(request.available!=null){ book.Available= (int)request.available; }

            await databaseContext.SaveChangesAsync();
        }

        public async Task addBookAsync(AddBookRequest request){

            Book book=new Book(request.title, request.description, request.available);

           //Calculate total upfront, so it won't be recalculated on every iteration
           int total=request.authorIds.Count();
           //For loop is chaper that foreach loop
           for(int i=0; i<total; i++){
               Author author=databaseContext.Authors.Find(request.authorIds[i]);
               if(author==null){
                  throw new ArgumentNullException("authorId", $"Author with the id of {request.authorIds[i]} not found");
               }
               book.addAuthor(author);
           }
           databaseContext.Add(book);
           await databaseContext.SaveChangesAsync();
        }

        public async Task decrementAvailableAsync(long[] bookIds, Checkout checkout) {
           for(int i=0; i<bookIds.Count(); i++) {
               Book book=await findByIdAsync(bookIds[i]);
            if(book!=null && book.Available>0){
                book.Available-=1;
                book.addCheckout(checkout);
                await databaseContext.SaveChangesAsync();
            } else {
                throw new InvalidOperationException("Book does not exist or is not available");
            }
           }
        }

        public void addCheckouts(HashSet<Book> books, Checkout checkout){
            foreach(var book in books){
                book.addCheckout(checkout);
            }
        }

    }

}