import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTodoComponent } from './pages/crud-todo/crud-todo.component';
import { TodolistComponent } from './pages/todolist/todolist.component';

const routes: Routes = [
  {
    path: '',
    component: TodolistComponent
  },
  {
    path: 'create',
    component: CreateTodoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
