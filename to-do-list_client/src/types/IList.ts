export interface TodoItem {
  listItemId: number;
  listId: number;
  title: string;
  description: string;
  isActive: boolean;
  createdDate: string;
  dueDate: string;
}

export interface TodoList {
  listId: number;
  title: string;
  description: string;
  createdDate: string;
  updatedDate: string;
  items: TodoItem[];
}
