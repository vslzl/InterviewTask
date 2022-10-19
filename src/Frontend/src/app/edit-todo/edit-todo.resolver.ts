import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { TodoService } from 'app/services/todo.service';
import { catchError, forkJoin, Observable, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class EditTodoResolver implements Resolve<any> {
    /**
     *
     */
    constructor(private _router: Router, private _todoService: TodoService) {

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
        route
        return this._todoService.getTodo(route.paramMap.get('todoId'))
            .pipe(
                // Error here means the requested task is not available
                catchError((error) => {

                    // Log the error
                    console.error(error);

                    // Navigate to home
                    this._router.navigate([''])

                    // Throw an error
                    return throwError(() => error);
                })
            );
    }
}