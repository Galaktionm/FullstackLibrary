using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.DTO {

    public record AddReviewRequest {


        [Required]
        public string userId { get; set; }

        [Required]

        public long bookId { get; set; }

        [Required]
        public int rating { get; set; }

        public String? review { get; set; }
    }

}