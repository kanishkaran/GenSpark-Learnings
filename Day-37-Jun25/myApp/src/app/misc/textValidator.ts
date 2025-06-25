import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function textValidator() : ValidatorFn{

    return(control: AbstractControl) : ValidationErrors | null => {

        let value = control.value as string;

        if(value?.length < 6 )
            return {lenError : 'Password Length Cannot Be Less Than 6 Characters'}
        return null;
    }
}