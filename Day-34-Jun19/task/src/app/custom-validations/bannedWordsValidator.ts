import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function bannedWordsValidator(bannedWords: string[]) : ValidatorFn{

    return (control: AbstractControl) : ValidationErrors | null => {
        const value : string = control.value ;

        return bannedWords.some(
            word => value?.includes(word)
        ) ? {bannedWordsError : true} : null;
    }
}