using System.ComponentModel.DataAnnotations;
using LibraryApp.API.Application.Validators;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Services;

namespace LibraryApp.API.DTO  { 

    public record AddCheckoutRequest {


        [Required]
        public string userId { get; set; }

        [IdListValidator]
        public long[] bookIds { get; set; }

        [Required]
        public String until { get; set; }

    }



}