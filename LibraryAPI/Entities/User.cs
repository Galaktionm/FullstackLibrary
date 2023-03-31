using Microsoft.AspNetCore.Identity;

namespace LibraryApp.API.Data.Entities {


public class User : IdentityUser {
    
    public HashSet<Checkout> checkouts { get; set; }
    private HashSet<BookReview> _reviews;

    public IReadOnlySet<BookReview> Reviews
    {
        get => _reviews;
    }

    public User(String userName, String email) : base(userName) {
        this.Email=email;
        this.checkouts=new HashSet<Checkout>();
        this._reviews=new HashSet<BookReview>();
    }

    public void addCheckout(Checkout checkout){
        checkout.user=this;
        this.checkouts.Add(checkout);
    }

    public void addReview(BookReview review){
        this._reviews.Add(review);
    }
    

}



}

