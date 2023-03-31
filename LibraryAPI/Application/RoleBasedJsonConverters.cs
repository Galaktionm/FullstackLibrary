

using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApp.API.Application.Serializers;

namespace LibraryApp.API.Application {

    public class RoleBasedJsonConverters {

        public static JsonSerializerOptions getUserOptions(){
            JsonSerializerOptions userOptions=new JsonSerializerOptions {
                Converters= { new BookUserSerializer(), new CheckoutUserSerializer() },
                WriteIndented=true
            };
            return userOptions;
        }

        public static JsonSerializerOptions getAdminOptions(){
            JsonSerializerOptions adminOptions=new JsonSerializerOptions {
                Converters= { new BookAdminSerializer(), new CheckoutAdminSerializer() },
                WriteIndented=true
            };
            return adminOptions;
        }

    }

}