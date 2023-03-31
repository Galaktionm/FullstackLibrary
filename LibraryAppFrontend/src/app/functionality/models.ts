export interface BookSummary {
    bookId: number,
    imagePath: string,
    title: string;
    description: String;
    author: BookAuthor[];
    rating: number;
    available: number;

}

export interface BookDetails {
    
    bookId: number,
    imagePath: string,
    title: String;
    description: string;
    author: BookAuthor[];
    rating: number;
    available: number;
    reviews: BookReview[]

}

export interface BookAuthor {
    authorId: string;
    name: string;
}

export interface BookReview {
    user: string;
    rating: number,
    review?: string
}

export interface Author {
    authorId: number;
    name: string;
}

export interface AddBookRequest {
    title: string,
    description: string,
    authorIds: number[],
    available: number
}

export interface User {
    userId: string,
    username: string,
    email: string,
    checkouts: Checkout[]

}

export interface Checkout {
    checkoutDate: string,
    returnDate: string,
    books: CheckoutBookTitles[]
}

export interface CheckoutBookTitles {
    title: string
}

export interface AddCheckoutRequest {
    until: string,
    bookIds: number[]
    userId: string
}

export interface AddReviewRequest {
    userId: string,
    bookId: number,
    rating: number,
    review: string
}