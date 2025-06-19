import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function passwordStrengthValidator() : ValidatorFn{
    return (control : AbstractControl) : ValidationErrors | null => {
        const value: string = control.value || null;

        if (value?.length < 6) {
            return {weakPassword : 'Password must be atleast 6 characters long'}
        }

        let hasNumber: boolean = false;
        let hasSymbol: boolean = false;

        const symbols: string = '!@#$%^&*()_+-=~`{}[];:,./?|';

        for (let i = 0; i < value?.length; i++) {
            let char = value[i];
            
            if(!isNaN(+char)){
                hasNumber = true;
            }
            else if(symbols.includes(char)){
                hasSymbol = true;
            }

            if(hasNumber && hasSymbol)
                break;
        }

        if(!hasNumber || !hasSymbol){
            return {weakPassword: "Password must include atleast a number and a character"}
        }

        return null;
    }
}