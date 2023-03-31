using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryApp.API.Application.Serializers;

namespace LibraryApp.API.Data.Entities {

    ///<summary>
    /// Author entity. 
    ///
    /// Used only for searching/filtering purposes. Authors do not have
    /// their account, like authors in Goodreads. Therefore, this entity does not
    /// extend IdentityUser.
    /// </summary>
    
    [JsonConverter(typeof(AuthorSerializer))]
    public class Author {

        [Key]
        public long authorId { get; set; }
        public String name  { get; set; }

        public String? bio { get; set; }

        public HashSet<Book> books { get; set; }

        public Author(String name, String? bio=null){
            this.name=name;
            this.bio=bio;
            this.books=new HashSet<Book>();
        }

        public void addBook(Book book){
            this.books.Add(book);
        }
    }


}