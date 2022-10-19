import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TodoService } from 'app/services/todo.service';
import { Todo } from 'app/services/todo.types';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-edit-todo',
  templateUrl: './edit-todo.component.html',
  styleUrls: ['./edit-todo.component.scss']
})
export class EditTodoComponent implements OnInit {


  todo: Todo;
  formGroup: FormGroup;
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _todoService: TodoService, private _formBuilder: FormBuilder, private _router: Router) { }

  ngOnInit(): void {
    this._todoService.todo$
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((todo) => {
        this.todo = todo;

        this.formGroup = this._formBuilder.group(
          {
            title: [todo.title,[Validators.required, Validators.maxLength(200)]],
            description: [todo.description],
            dueDate: [todo.dueDate.toString()],
            isDone: [todo.isDone]
          }
        );
      });
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  submitTodo() {
    this._todoService.editTodo(this.todo.todoId, this.formGroup.getRawValue()).subscribe((todo)=>{
      this._router.navigate(['']);
    });
   }
}
