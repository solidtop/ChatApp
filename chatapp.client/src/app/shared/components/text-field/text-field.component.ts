import { ChangeDetectionStrategy, Component, input, signal } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-text-field',
  imports: [FormsModule],
  templateUrl: './text-field.component.html',
  styleUrl: './text-field.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: TextFieldComponent, 
    multi: true,
  }]
})
export class TextFieldComponent implements ControlValueAccessor {
  id = input<string>();
  type = input<'text' | 'email' | 'password'>('text');
  label = input<string>();
  placeholder = input<string>();
  helperText = input<string>();

  value = signal<string>('');
  disabled = signal<boolean>(false);
  onChange: OnChangeFn<string> = () => {}
  onTouched = () => {}

  writeValue(value: string): void {
    this.value.set(value);
  }

  registerOnChange(fn: OnChangeFn<string>): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: OnTouchFn): void {
    this.onTouched = fn;  
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled.set(isDisabled);
  }
}

type OnChangeFn<T> = (value: T) => void;
type OnTouchFn = () => void;