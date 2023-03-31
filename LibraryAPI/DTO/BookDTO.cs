using System.ComponentModel.DataAnnotations;
using LibraryApp.API.Application.Validators;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Services;

namespace LibraryApp.API.DTO  {

    public record AddBookRequest {

        [Required]
        public String title { get; set; }

        [Required]
        public String description { get; set; }
       
        [IdListValidatorAttribute]
        public long[] authorIds { get; set;}

        [Range(0, int.MaxValue)]
        public int available { get; set; }
    }


    public record UpdateBookRequest {

         [Required]
         public long bookId { get; set; }
         public String? title { get; set; }
         public String? description { get; set; }

         public int? available { get; set; }
    }


}