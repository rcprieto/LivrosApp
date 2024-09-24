import { NgIf } from '@angular/common';
import { Component, input, Input, Self } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  BsDatepickerConfig,
  BsDatepickerModule,
  BsLocaleService,
} from 'ngx-bootstrap/datepicker';

import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-date-picker',
  standalone: true,
  imports: [ReactiveFormsModule, BsDatepickerModule, NgIf],
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.css',
})
export class DatePickerComponent implements ControlValueAccessor {
  label = input<string>('');
  maxDate = input<Date | undefined>();
  bsConfig?: Partial<BsDatepickerConfig>;

  constructor(
    @Self() public ngControl: NgControl,
    private localeService: BsLocaleService
  ) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD/MM/YYYY',
    };
    localeService.use('pt-br');
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }
}
