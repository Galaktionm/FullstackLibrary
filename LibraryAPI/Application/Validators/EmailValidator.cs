using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Application.Validators {

    sealed public class EmailValidatorAttribute : ValidationAttribute {

        private static String[] validEmails={ "gmail.com", "yahoo.com", "libraryapp.com"};

        public override bool IsValid(object? value)
        {

            if(value==null){
                return false;
             }
          
           string email=value as string;
           
           int index=email.IndexOf('@');
           
           if(index==-1) {
            return false;
           }

           if(Array.Find(validEmails, e=>e.Equals(email.Substring(index+1)))==null){
            return false;
           }

           return true;

        }

    }

    

}