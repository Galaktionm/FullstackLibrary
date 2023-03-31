using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Data.Entities;

namespace LibraryApp.API.Application.Serializers {

    public class UserSerializer : JsonConverter<User>
    {
        public override User? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Throw NotSupportedException instead of NotImplementedException
            throw new NotSupportedException("User deserialization is not supported");
        }

        public override void Write(Utf8JsonWriter writer, User user, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("userId", user.Id);
            writer.WriteString("username", user.UserName);
            writer.WriteString("email", user.Email);
            writer.WriteStartArray("checkouts");
            foreach(var checkout in user.checkouts){
                writer.WriteStartObject();
                writer.WriteStartArray("books");
                foreach(var book in checkout.books){
                    writer.WriteStartObject();
                    writer.WriteString("title", book.title);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteString("checkoutDate", checkout.checkoutDate);
                writer.WriteString("returnDate", checkout.returnDate);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }



}