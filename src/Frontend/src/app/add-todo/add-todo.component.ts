import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TodoService } from 'app/services/todo.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.scss']
})
export class AddTodoComponent implements OnInit {

  formGroup: FormGroup;
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _todoService: TodoService, private _formBuilder: FormBuilder, private _router: Router) { }

  ngOnInit(): void {
    this.formGroup = this._formBuilder.group(
      {
        title: ['', [Validators.required, Validators.maxLength(200)]],
        description: [''],
        dueDate: [new Date()],
        isDone: [false]
      }
    );
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  submitTodo() {
    this._todoService.addTodo(this.formGroup.getRawValue()).subscribe((todo) => {
      this._router.navigate(['']);
    });
  }

}
