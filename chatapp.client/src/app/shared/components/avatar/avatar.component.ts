import { Component, input } from '@angular/core';

@Component({
  selector: 'app-avatar',
  imports: [],
  template: `<img [src]="src()" [alt]="alt()" [class]="'avatar ' + size()" />`,
  styles: 
    `
    .avatar {
      clip-path: circle();
    }

    .small {
      max-width: 30px;
    }

    .medium {
      max-width: 50px;
    }

    .large {
        max-width: 70px;
    }
    `
  ,
})
export class AvatarComponent {
  src = input<string>();
  alt = input<string>();
  size = input<'small' | 'medium' | 'large'>('medium');
}
