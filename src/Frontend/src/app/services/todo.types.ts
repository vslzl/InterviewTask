export interface Todo{
    todoId: number;
    title: string;
    description?:string;
    dueDate?:Date;
    isDone: boolean;
}