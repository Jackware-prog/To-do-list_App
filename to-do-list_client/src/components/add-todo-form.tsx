"use client";

import type React from "react";

import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import type { TodoItem } from "@/types/IList";
import { CalendarIcon, Plus } from "lucide-react";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { format } from "date-fns";
import { cn } from "@/lib/utils";

interface AddTodoFormProps {
  onAddTodo: (
    todo: Omit<TodoItem, "listItemId" | "listId" | "createdDate">
  ) => void;
}

export function AddTodoForm({ onAddTodo }: AddTodoFormProps) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [date, setDate] = useState<Date | undefined>(
    new Date(Date.now() + 24 * 60 * 60 * 1000)
  ); // Tomorrow
  const [isExpanded, setIsExpanded] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!title.trim()) return;

    onAddTodo({
      title,
      description,
      isActive: true,
      dueDate: date ? date.toISOString() : new Date().toISOString(),
    });

    // Reset form
    setTitle("");
    setDescription("");
    setDate(new Date(Date.now() + 24 * 60 * 60 * 1000));
    setIsExpanded(false);
  };

  return (
    <Card>
      <form onSubmit={handleSubmit}>
        <CardHeader>
          <CardTitle>Add New Task</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4 mb-6">
          <div className="space-y-2">
            <Input
              placeholder="Task title"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              onFocus={() => setIsExpanded(true)}
              required
            />
          </div>

          {isExpanded && (
            <>
              <div className="space-y-2">
                <Textarea
                  placeholder="Description"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  rows={3}
                />
              </div>

              <div className="space-y-2">
                <Popover>
                  <PopoverTrigger asChild>
                    <Button
                      variant="outline"
                      className={cn(
                        "w-full justify-start text-left font-normal",
                        !date && "text-muted-foreground"
                      )}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {date ? (
                        format(date, "PPP")
                      ) : (
                        <span>Pick a due date</span>
                      )}
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0">
                    <Calendar
                      mode="single"
                      selected={date}
                      onSelect={setDate}
                      initialFocus
                    />
                  </PopoverContent>
                </Popover>
              </div>
            </>
          )}
        </CardContent>
        <CardFooter className="flex justify-between">
          {isExpanded && (
            <Button
              type="button"
              variant="ghost"
              onClick={() => setIsExpanded(false)}
            >
              Cancel
            </Button>
          )}
          <Button
            type="submit"
            className={cn(isExpanded ? "" : "w-full")}
            disabled={!title.trim() || !description.trim()}
          >
            <Plus className="mr-2 h-4 w-4" /> Add Task
          </Button>
        </CardFooter>
      </form>
    </Card>
  );
}
