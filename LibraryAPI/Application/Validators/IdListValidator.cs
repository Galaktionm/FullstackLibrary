using System.ComponentModel.DataAnnotations;

namespace LibraryApp.API.Application.Validators{

    sealed public class IdListValidatorAttribute : ValidationAttribute {

        public override bool IsValid(object? value)
        {
            if(value==null){
                return false;
            }

            long[] list=value as long[];

            return list.Count()>=1;
        }


    }

}