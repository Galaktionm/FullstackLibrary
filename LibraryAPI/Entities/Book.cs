using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LibraryApp.API.Application.Serializers;

namespace LibraryApp.API.Data.Entities {

    [JsonConverter(typeof(BookUserSerializer))]
    public class Book {

        [Key]
        public long bookid { get; private set; }
        private String _title;

        public String title
        {
            get => _title;
            set {
                if(value!=null) {
                    _title=value;
                }
            }
        }
        public String description { get; set; }

        private HashSet<Author> _authors;

        public IReadOnlySet<Author> Authors
        {
            get=> _authors;
        }
        private HashSet<BookReview> _reviews;

        public IReadOnlySet<BookReview> Reviews {
            get => _reviews;
        }

        private HashSet<Checkout> _checkouts;

        public IReadOnlySet<Checkout> Checkouts {
            get=> _checkouts;
        }
        private int _available;

        public int Available 
        {
            get {
                return _available;
            }

            set {
                if(value>=0){
                    _available=value;
                }
            }

        }

        public double rating { get; private set; }

        public String? imagePath { get; set; }
        
        public Book(String title, String description, int available){
            this.title=title;
            this.description=description;
            _authors=new HashSet<Author>();
            this._reviews=new HashSet<BookReview>();
            this._checkouts=new HashSet<Checkout>();
            this.Available=available;
        }

        public void addReview(BookReview review){
            this._reviews.Add(review);
        }

        public void addAuthor(Author author){
            this._authors.Add(author);
        }

        public void addCheckout(Checkout checkout) {
            this._checkouts.Add(checkout);
        }
        
    }

}