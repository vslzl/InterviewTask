import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatTable } from '@angular/material/table';
import { TodoService } from 'app/services/todo.service';
import { Todo } from 'app/services/todo.types';
import { Subject, takeUntil } from 'rxjs';

import { MatSlideToggleChange } from '@angular/material/slide-toggle'

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {

  todos: Todo[];
  displayedColumns: string[] = ['todoId', 'title', 'dueDate', 'isDone', 'actions'];
  @ViewChild(MatTable) table: MatTable<Todo>;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _todoService: TodoService) { }

  ngOnInit(): void {
    this._todoService.todos$
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((todos) => {
        this.todos = todos;
        if (!!this.table) {
          this.table.renderRows();
        }
      });
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }


  dueChanged(event: MatSlideToggleChange) {
    if (event.checked) {
      this._todoService.getDuesOnly().subscribe();
    }
    else {
      this._todoService.getTodos().subscribe();
    }
  }

  changeDone(event: MatCheckboxChange, todo: Todo) {
    todo.isDone = event.checked;
    this._todoService.editTodo(todo.todoId, todo).subscribe(() => {

    }, (err) => {
      //on error:
      todo.isDone = !event.checked;
      this.table.renderRows();
    });
  }


  deleteTodo(todo: Todo) {
    const removing = this.todos.findIndex(el => el.todoId == todo.todoId);
    if (removing !== -1) {
      this._todoService.deleteTodo(todo.todoId).subscribe(() => {
        this.todos.splice(removing, 1);
        this.table.renderRows();
      });
    }
  }

}
