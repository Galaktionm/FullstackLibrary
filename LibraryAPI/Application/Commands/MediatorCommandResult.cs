

namespace LibraryApp.API.Application.Commands {

    public record MediatorCommandResult {
        public bool succeeded { get; set; }
        public string message { get; set; }
    }

}