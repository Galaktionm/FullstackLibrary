using System.ComponentModel.DataAnnotations;
using LibraryApp.API.Application.Validators;

namespace LibraryApp.API.DTO  {

    public record RegistrationRequest {

        [Required]
        public string username { get; set; }

        [EmailValidator]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

    }

    public record LoginRequest {

        [EmailValidator]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

    }

    public record LoginResult {

        public bool success { get; set; }

        public String message { get; set; }

        public String token { get; set; }

        public String userId { get; set; }

        public bool isAdmin { get; set; }
    }


}