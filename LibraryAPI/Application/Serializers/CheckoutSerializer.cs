using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;

namespace LibraryApp.API.Application.Serializers {

  public class CheckoutUserSerializer : JsonConverter<Checkout>
    {
        public override Checkout? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, Checkout checkout, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("checkoutDate", checkout.checkoutDate);
            writer.WriteString("returnDate", checkout.returnDate);
              writer.WriteStartArray("books");
                foreach(var book in checkout.books){
                  writer.WriteStartObject();
                   writer.WriteString("title", book.title);
                   writer.WriteEndObject();
                }
              writer.WriteEndArray();
            writer.WriteString("user", checkout.user.Email);
            writer.WriteEndObject();
            
        }
    }

    public class CheckoutAdminSerializer : JsonConverter<Checkout>
    {
        public override Checkout? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, Checkout checkout, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", checkout.checkoutId);
            writer.WriteString("checkoutDate", checkout.checkoutDate);
            writer.WriteString("returnDate", checkout.returnDate);
              writer.WriteStartObject("user");
                writer.WriteString("id", checkout.user.Id);
                writer.WriteString("name", checkout.user.UserName);
                writer.WriteString("email", checkout.user.Email);
              writer.WriteEndObject();
              writer.WriteStartArray("books");
                foreach(var book in checkout.books){
                  writer.WriteStartObject();
                   writer.WriteString("title", book.title);
                   writer.WriteEndObject();
                }
              writer.WriteEndArray();
            writer.WriteEndObject();
            
        }
    }
}