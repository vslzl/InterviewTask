import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { TodoService } from 'app/services/todo.service';
import { forkJoin, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class TodoListResolver implements Resolve<any> {

    /**
     *
     */
    constructor(private _todoService: TodoService) {

    }
    
    resolve(route: ActivatedRouteSnapshot): Observable<any> {
        return forkJoin([this._todoService.getTodos()]);
    }
}