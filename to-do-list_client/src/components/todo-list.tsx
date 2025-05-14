"use client";

import type { TodoItem } from "@/types/IList";
import { Card, CardContent } from "@/components/ui/card";
import { Checkbox } from "@/components/ui/checkbox";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2 } from "lucide-react";
import { useState } from "react";
import { EditTodoForm } from "./edit-todo-form";
import { Badge } from "@/components/ui/badge";
import { format, parseISO } from "date-fns";

interface TodoListProps {
  todos: TodoItem[];
  onUpdateTodo: (todo: TodoItem) => void;
  onDeleteTodo: (id: number) => void;
  onToggleStatus: (id: number) => void;
}

export function TodoList({
  todos,
  onUpdateTodo,
  onDeleteTodo,
  onToggleStatus,
}: TodoListProps) {
  const [editingId, setEditingId] = useState<number | null>(null);

  const handleEdit = (todo: TodoItem) => {
    setEditingId(todo.listItemId);
  };

  const handleUpdate = (todo: TodoItem) => {
    onUpdateTodo(todo);
    setEditingId(null);
  };

  const handleCancel = () => {
    setEditingId(null);
  };

  if (todos.length === 0) {
    return (
      <div className="text-center p-8 border rounded-lg bg-muted/20">
        <p className="text-muted-foreground">
          No todos yet. Add one to get started!
        </p>
      </div>
    );
  }

  return (
    <div className="space-y-4">
      <h2 className="text-xl font-semibold">Your Tasks</h2>
      {todos.map((todo) => (
        <Card
          key={todo.listItemId}
          className={`${!todo.isActive ? "bg-muted/30" : ""}`}
        >
          <CardContent className="p-4">
            {editingId === todo.listItemId ? (
              <EditTodoForm
                todo={todo}
                onUpdate={handleUpdate}
                onCancel={handleCancel}
              />
            ) : (
              <div className="flex items-start gap-3">
                <Checkbox
                  id={`todo-${todo.listItemId}`}
                  checked={todo.isActive}
                  onCheckedChange={() => onToggleStatus(todo.listItemId)}
                  className="mt-1"
                />
                <div className="flex-1 space-y-1">
                  <div className="flex items-start justify-between">
                    <div>
                      <label
                        htmlFor={`todo-${todo.listItemId}`}
                        className={`font-medium ${
                          todo.isActive
                            ? "line-through text-muted-foreground"
                            : ""
                        }`}
                      >
                        {todo.title}
                      </label>
                      <p
                        className={`text-sm ${
                          todo.isActive
                            ? "line-through text-muted-foreground"
                            : "text-muted-foreground"
                        }`}
                      >
                        {todo.description}
                      </p>
                    </div>
                    <div className="flex items-center gap-2">
                      <Button
                        variant="ghost"
                        size="icon"
                        onClick={() => handleEdit(todo)}
                        aria-label="Edit todo"
                      >
                        <Pencil className="h-4 w-4" />
                      </Button>
                      <Button
                        variant="ghost"
                        size="icon"
                        onClick={() => onDeleteTodo(todo.listItemId)}
                        aria-label="Delete todo"
                      >
                        <Trash2 className="h-4 w-4" />
                      </Button>
                    </div>
                  </div>
                  <div className="flex items-center justify-between mt-2">
                    <Badge
                      variant={
                        isOverdue(todo.dueDate) ? "destructive" : "outline"
                      }
                    >
                      Due: {formatDate(todo.dueDate)}
                    </Badge>
                    <Badge variant="secondary" className="text-xs">
                      Created: {formatDate(todo.createdDate)}
                    </Badge>
                  </div>
                </div>
              </div>
            )}
          </CardContent>
        </Card>
      ))}
    </div>
  );
}

function formatDate(dateString: string): string {
  try {
    // Parse as UTC ISO string
    const date = parseISO(dateString);
    return format(date, "MMM d, yyyy");
  } catch (error) {
    console.error("Date formatting error:", error);
    return "Invalid date";
  }
}

function isOverdue(dateString: string): boolean {
  try {
    const dueDate = new Date(dateString);
    return dueDate < new Date() && dueDate.getTime() > 0;
  } catch (error) {
    return false;
  }
}
