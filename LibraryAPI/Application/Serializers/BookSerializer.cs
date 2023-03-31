using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;

namespace LibraryApp.API.Application.Serializers {

    public class BookUserSerializer : JsonConverter<Book>
    {
        public override Book? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Throw NotSupportedException instead of NotImplementedException
            throw new NotSupportedException("Book deserialization is not supported");
        }

        public override void Write(Utf8JsonWriter writer, Book book, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("bookId", book.bookid);
            writer.WriteString("imagePath", book.imagePath);
            writer.WriteString("title", book.title);
            writer.WriteString("description", book.description);
            writer.WriteStartArray("author");
                foreach(var author in book.Authors){
                    writer.WriteStartObject();
                    writer.WriteNumber("authorId", author.authorId);
                    writer.WriteString("name", author.name);
                    writer.WriteEndObject();
                }
            writer.WriteEndArray();
            writer.WriteNumber("rating", book.rating);
            writer.WriteNumber("available", book.Available);
            writer.WriteStartArray("reviews");
                foreach(var review in book.Reviews){
                    writer.WriteStartObject();
                    writer.WriteString("user", review.user.UserName);
                    writer.WriteNumber("rating", review.Rating);
                    if(review.review!=null){
                        writer.WriteString("review", review.review);
                    }
                    writer.WriteEndObject();
                }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }

    public class BookAdminSerializer : JsonConverter<Book>
    {
        public override Book? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, Book book, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("bookId", book.bookid);
            writer.WriteString("imagePath", book.imagePath);
            writer.WriteString("title", book.title);
            writer.WriteString("description", book.description);
            writer.WriteStartArray("author");
                foreach(var author in book.Authors){
                    writer.WriteStartObject();
                    writer.WriteNumber("authorId", author.authorId);
                    writer.WriteString("name", author.name);
                    writer.WriteEndObject();
                }
            writer.WriteEndArray();
            writer.WriteNumber("rating", book.rating);
            writer.WriteNumber("available", book.Available);
            writer.WriteStartArray("checkouts");
                foreach(var checkout in book.Checkouts){
                    writer.WriteStartObject();
                    writer.WriteString("user", checkout.user.UserName);
                    writer.WriteString("email", checkout.user.Email);
                    writer.WriteString("checkoutDate", checkout.checkoutDate);
                    writer.WriteString("returnDate", checkout.returnDate);
                    writer.WriteBoolean("returned", checkout.returned);
                    writer.WriteEndObject();
                }
            writer.WriteEndArray();
            writer.WriteEndObject();
            
        }
    }


}