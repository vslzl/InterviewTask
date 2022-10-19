import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTodoComponent } from './add-todo/add-todo.component';
import { EditTodoComponent } from './edit-todo/edit-todo.component';
import { EditTodoResolver } from './edit-todo/edit-todo.resolver';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoListResolver } from './todo-list/todo-list.resolver';

const routes: Routes = [
  {
    path     : '',
    component: TodoListComponent,
    resolve: {
        initialData: TodoListResolver
    }
  },
  {
    path     : 'edit/:todoId',
    component: EditTodoComponent,
    resolve: {
        initialData: EditTodoResolver
    }
  },
  {
    path     : 'add',
    component: AddTodoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
