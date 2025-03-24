import { Component, input, output } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-form',
  imports: [ReactiveFormsModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent {
  id = input<string>();
  title = input<string>();
  subTitle = input<string>();
  formGroup = input<FormGroup>();
  submit = output<SubmitEvent>();
}
