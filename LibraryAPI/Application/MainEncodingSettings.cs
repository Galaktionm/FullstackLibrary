
using System.Text.Encodings.Web;

namespace LibraryApp.API.Application {

    public class MainEncodingSettings : TextEncoderSettings {
        
        private MainEncodingSettings() : base() {}

        public static MainEncodingSettings getInstance(){
            MainEncodingSettings instance=new MainEncodingSettings();
            instance.AllowCharacter('\'');
            return instance;
        }

    }





}