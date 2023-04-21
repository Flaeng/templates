import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-crud-todo',
  templateUrl: './crud-todo.component.html',
  styleUrls: ['./crud-todo.component.scss'],
})
export class CreateTodoComponent {
  todo: FormGroup = new FormGroup({
    title: new FormControl<string>(''),
    description: new FormControl<string>(''),
  });

  id: string | undefined;
  formCanSubmit: boolean = true;

  async submitForm(ev: Event): Promise<void> {
    if (this.formCanSubmit === false) {
      return;
    }

    this.formCanSubmit = false;
    try {
      if (this.id === undefined) {
        await this.createTodo(ev);
      } else {
        await this.updateTodo(ev);
      }
    } catch (error) {
      console.error(error);
    }
    this.formCanSubmit = true;
  }

  async createTodo(ev: Event): Promise<void> {
    fetch('todo/create', {
      method: 'put',
      cache: 'no-cache',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.todo.value),
    });
  }

  async updateTodo(ev: Event): Promise<void> {
    fetch(`todo/update?id=${this.id}`, {
      method: 'patch',
      cache: 'no-cache',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.todo.value),
    });
  }
}
