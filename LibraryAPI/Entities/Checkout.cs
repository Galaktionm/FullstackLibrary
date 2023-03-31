using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryApp.API.Application.Serializers;

namespace LibraryApp.API.Data.Entities {

    /// <summary>
    /// Checkout instance describes the information regarding
    /// user's checked out book(s), times of their checkout and return.
    /// Checkout can have multiple books, in which case "returnDate" is 
    /// the return date for all of them. 
    /// </summary>

    [JsonConverter(typeof(CheckoutUserSerializer))]
    public class Checkout {

        [Key]
        public long checkoutId { get; set; }
        public User user { get; set; }

        public HashSet<Book> books { get; set; }

        public DateTime checkoutDate { get; }

        public DateTime returnDate { get; set; }

        public bool returned { get; set; }

        public Checkout(DateTime returnDate){
            this.books=new HashSet<Book>();
            this.checkoutDate=DateTime.UtcNow;
            this.returnDate=returnDate;
            this.returned=false;
        }

    }



}