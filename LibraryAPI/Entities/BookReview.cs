
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Data.Entities {

    public class BookReview {

        public User user { get; private set; }

        public string userId { get; private set; }

        public long bookId { get; private set; }

        public int _rating { get; set; }

        public int Rating
        {
            get => _rating;
            set
            {
                if(value>0 && value<=5){
                    _rating=value;
                }
            }

        }



        public String? review { get; set; }

        public BookReview(string userId, long bookId, int rating, String? review=null){
            this.userId=userId;
            this.bookId=bookId;
            this.Rating=rating;
            this.review=review;
        }

    }

}