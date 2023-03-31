using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Data.Repositories;

namespace LibraryApp.API.Application.Serializers {


    public class AuthorSerializer : JsonConverter<Author>
    {
        public override Author? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override void Write(Utf8JsonWriter writer, Author author, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("authorId", author.authorId);
            writer.WriteString("name", author.name);
            writer.WriteStartArray("books");
                foreach(var book in author.books){
                    writer.WriteStartObject();
                    writer.WriteString("name", book.title);
                    writer.WriteString("description", book.description);
                    writer.WriteNumber("rating", book.rating);
                    writer.WriteEndObject();
                }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }

}