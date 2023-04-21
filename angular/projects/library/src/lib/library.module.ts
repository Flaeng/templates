import { NgModule } from '@angular/core';
import { CardComponent } from './components/card/card.component';
import { ButtonComponent } from './components/button/button.component';

@NgModule({
  declarations: [CardComponent, ButtonComponent],
  imports: [],
  exports: [CardComponent, ButtonComponent],
})
export class LibraryModule {}
