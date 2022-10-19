import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, map, Observable, of, switchMap, tap, throwError } from 'rxjs';
import { Todo } from './todo.types';
import { environment } from 'environments/environment';

@Injectable({ providedIn: 'root' })
export class TodoService {
    private _todos: BehaviorSubject<Todo[] | null> = new BehaviorSubject(null);
    private _todo: BehaviorSubject<Todo | null> = new BehaviorSubject(null);

    constructor(private _httpClient: HttpClient) { }

    get todos$(): Observable<Todo[]> {
        return this._todos.asObservable();
    }

    get todo$(): Observable<Todo> {
        return this._todo.asObservable();
    }

    getTodos(): Observable<Todo[]> {
        return this._httpClient.get<Todo[]>(`${environment.apiUrl}/api/todos`).pipe(
            tap((response: any) => {
                this._todos.next(response);
            })
        );
    }


    getTodo(id: string): Observable<Todo> {
        return this._httpClient.get<Todo>(`${environment.apiUrl}/api/todos/${id}`).pipe(
            map((response: any) => {
                this._todo.next(response);
                return response;
            }),
            switchMap((response) => {
                if (!response) {
                    return throwError(() => new Error('Could not found todo with id of ' + id + '!'))
                }
                return of(response);
            })
        );
    }

    getDuesOnly(): Observable<Todo> {
        return this._httpClient.get<Todo[]>(`${environment.apiUrl}/api/todos/overdue`).pipe(
            tap((response: any) => {
                this._todos.next(response);
            })
        );
    }

    editTodo(id:number, editedTodo: Todo): Observable<Todo> 
    {
        return this._httpClient.put<Todo>(`${environment.apiUrl}/api/todos/${id}`, editedTodo).pipe(
            tap((response: any) => {
                this._todo.next(response);
            })            
        );
    }

    addTodo(newTodo: Todo): Observable<Todo> 
    {
        return this._httpClient.post<Todo>(`${environment.apiUrl}/api/todos`, newTodo).pipe(
            tap((response: any) => {
                this._todo.next(response);
            })
        );
    }

    deleteTodo(todoId: number): Observable<Todo> 
    {
        return this._httpClient.delete<Todo>(`${environment.apiUrl}/api/todos/${todoId}`).pipe(
            tap((response: any) => {
                this._todo.next(response);
            })
        );
    }


}