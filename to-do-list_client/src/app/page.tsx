"use client";

import { useState, useEffect } from "react";
import { TodoList } from "@/components/todo-list";
import { AddTodoForm } from "@/components/add-todo-form";
import type { TodoItem } from "@/types/IList";
import { Pencil } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Button } from "@/components/ui/button";

export default function Home() {
  const [todos, setTodos] = useState<TodoItem[]>([]);
  const [listTitle, setListTitle] = useState("My Todo List");
  const [listDescription, setListDescription] = useState(
    "A simple todo list application"
  );
  const [isLoading, setIsLoading] = useState(true);
  const [isEditingHeader, setIsEditingHeader] = useState(false);

  useEffect(() => {
    const fetchListData = async () => {
      try {
        setIsLoading(true);

        const response = await fetch(`http://localhost:8080/api/List/1`);

        if (!response.ok) {
          throw new Error("Failed to fetch list data");
        }

        const data = await response.json();

        console.log(data);

        setListTitle(data.title);
        setListDescription(data.description);
        setTodos(data.items);
      } catch (error) {
        console.error("Error fetching list:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchListData();
  }, []);

  // Add item
  const addTodo = async (
    todo: Omit<TodoItem, "listItemId" | "listId" | "createdDate" | "isActive">
  ) => {
    try {
      // Prepare the todo item for the backend
      const newTodoItem = {
        title: todo.title,
        description: todo.description,
        dueDate: todo.dueDate,
        listId: 1,
      };

      const response = await fetch("http://localhost:8080/api/ListItem", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newTodoItem),
      });

      if (!response.ok) {
        throw new Error("Failed to add todo");
      }

      // Get the created item from the response
      const createdItem = await response.json();

      // Update the local state with the new todo
      setTodos([...todos, createdItem]);
    } catch (error) {
      console.error("Error adding todo:", error);
    }
  };

  // Update item
  const updateTodo = async (updatedTodo: TodoItem) => {
    try {
      const response = await fetch(
        `http://localhost:8080/api/ListItem/${updatedTodo.listItemId}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(updatedTodo),
        }
      );

      if (!response.ok) {
        throw new Error("Failed to update todo");
      }

      // Only update local state if API call succeeds
      setTodos(
        todos.map((todo) =>
          todo.listItemId === updatedTodo.listItemId ? updatedTodo : todo
        )
      );
    } catch (error) {
      console.error("Error updating todo:", error);
    }
  };

  // Delete item
  const deleteTodo = async (id: number) => {
    try {
      const response = await fetch(`http://localhost:8080/api/ListItem/${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Failed to delete todo");
      }

      // Only update local state if API call succeeds
      setTodos(todos.filter((todo) => todo.listItemId !== id));
    } catch (error) {
      console.error("Error deleting todo:", error);
    }
  };

  // Toggle item active status
  const toggleTodoStatus = async (id: number) => {
    try {
      // Find the todo to update
      const todoToUpdate = todos.find((todo) => todo.listItemId === id);
      if (!todoToUpdate) return;

      // Create updated todo with only isActive changed
      const updatedTodo = {
        ...todoToUpdate,
        isActive: !todoToUpdate.isActive,
      };

      // Call API to update
      const response = await fetch(
        `http://localhost:8080/api/ListItem/Check/${id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(updatedTodo),
        }
      );

      if (!response.ok) {
        throw new Error("Failed to update todo status");
      }

      // Update local state only after successful API call
      setTodos(
        todos.map((todo) => (todo.listItemId === id ? updatedTodo : todo))
      );
    } catch (error) {
      console.error("Error toggling todo status:", error);
    }
  };

  // Update List Details
  const updateListDetails = async (
    newTitle: string,
    newDescription: string
  ) => {
    try {
      const response = await fetch(`http://localhost:8080/api/List/1`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          listId: 1,
          title: newTitle,
          description: newDescription,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to update list details");
      }

      // Update local state only after successful API call
      setListTitle(newTitle);
      setListDescription(newDescription);
      setIsEditingHeader(false);

      // Optional success feedback
      console.log("List details updated successfully");
    } catch (error) {
      console.error("Error updating list details:", error);
    }
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-screen">
        Loading...
      </div>
    );
  }

  return (
    <main className="container mx-auto p-4 max-w-3xl">
      <div className="space-y-6">
        {isEditingHeader ? (
          <div className="space-y-4 p-4 border rounded-lg bg-card">
            <div className="space-y-2">
              <label htmlFor="list-title" className="text-sm font-medium">
                List Title
              </label>
              <Input
                id="list-title"
                value={listTitle}
                onChange={(e) => setListTitle(e.target.value)}
                placeholder="Enter list title"
              />
            </div>
            <div className="space-y-2">
              <label htmlFor="list-description" className="text-sm font-medium">
                Description
              </label>
              <Textarea
                id="list-description"
                value={listDescription}
                onChange={(e) => setListDescription(e.target.value)}
                placeholder="Enter list description"
                rows={2}
              />
            </div>
            <div className="flex justify-end gap-2">
              <Button
                variant="outline"
                onClick={() => setIsEditingHeader(false)}
              >
                Cancel
              </Button>
              <Button
                onClick={() => updateListDetails(listTitle, listDescription)}
              >
                Save
              </Button>
            </div>
          </div>
        ) : (
          <div className="flex justify-between items-start">
            <div>
              <h1 className="text-3xl font-bold tracking-tight">{listTitle}</h1>
              <p className="text-muted-foreground">{listDescription}</p>
            </div>
            <Button
              variant="outline"
              size="sm"
              onClick={() => setIsEditingHeader(true)}
            >
              <Pencil className="h-4 w-4 mr-2" />
              Edit
            </Button>
          </div>
        )}

        <AddTodoForm onAddTodo={addTodo} />

        <TodoList
          todos={todos ?? []}
          onUpdateTodo={updateTodo}
          onDeleteTodo={deleteTodo}
          onToggleStatus={toggleTodoStatus}
        />
      </div>
    </main>
  );
}
