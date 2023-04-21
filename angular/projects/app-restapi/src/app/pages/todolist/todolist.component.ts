import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';

type TodoItem = { title: string, description: string }

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.scss']
})
export class TodolistComponent implements OnInit {

  todoItems$: Observable<TodoItem[]> = of([]);

  constructor(
  ) {
  }

  ngOnInit(): void {
  }

}
