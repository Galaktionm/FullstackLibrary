using System.Text.Json;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.API.Data.Repositories {

    public class AuthorRepository {

        private readonly DatabaseContext databaseContext;

        public AuthorRepository(DatabaseContext databaseContext){
            this.databaseContext=databaseContext;
        }

        public IQueryable<Author> getAll(){
            IQueryable<Author> result=databaseContext.Authors.Include<Author>("books");
            return result;
        }

        public async Task<Author?> searchByIdAsync(long id){
            return await databaseContext.Authors.Include<Author>("books")
                     .FirstOrDefaultAsync<Author>(author=>author.authorId==id);
        }

        /// <summary>
        ///    Search for author by name. Search is case-insensitive
        /// </summary>
        public IQueryable<Author> searchByNameAsync(string name){
            return  databaseContext.Authors.Include<Author>("books").Where(author=>author.name.ToLower().Contains(name.ToLower()));
        }

        public async Task addAuthor(AddAuthorRequest request) {
            Author author=new Author(request.name, request.bio);
            databaseContext.Add(author);
            await databaseContext.SaveChangesAsync();
        }

        public void deleteAuthor(long id) {
            Author? author=databaseContext.Authors.Find(id);
            databaseContext.Remove(author);
            // Might throw argumentnull exception
        }



    }

}