using LibraryApp.API.Data.Entities;

namespace LibraryApp.API.DTO {

    public class AddAuthorRequest
    {
        public String name  { get; set; }

        public String? bio { get; set; }

        public AddAuthorRequest(String name, String? bio=null) {
            this.name=name;
            this.bio=bio;
        }
    }


}