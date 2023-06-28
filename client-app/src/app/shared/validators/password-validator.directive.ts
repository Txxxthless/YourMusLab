import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(passwordRegEx: RegExp): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    return passwordRegEx.test(control.value)
      ? {
          forbiddenPassword: {
            value: control.value,
          },
        }
      : null;
  };
}
