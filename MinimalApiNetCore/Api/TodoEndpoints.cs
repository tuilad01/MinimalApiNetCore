using MinimalApiNetCore.Database;
using Services.Todo;

namespace MinimalApiNetCore.Api
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this WebApplication app)
        {
            app.MapGet("/todo", (ITodoService todoService) =>
            {
                var todos = todoService.GetAll();
                return Results.Ok(todos);
            });
            app.MapGet("/todo/{id}", async (int id, ITodoService todoService) =>
            {

                var todo = await todoService.Get(id);
                if (todo != null)
                {
                    return Results.Ok(todo);
                }

                return Results.NotFound();
            });

            app.MapPost("/todo", async (Todo newTodo, ITodoService todoService) =>
            {
                var todo = await todoService.Add(newTodo);

                // Cannot add
                if (todo == null || todo.Id == 0 )
                {
                    return Results.NoContent();
                }

                return Results.Created($"/todo/{todo.Id}", todo);
            });


            app.MapPut("/todo/{id}", async (int id, Todo newTodo, ITodoService todoService) =>
            {
                var todo = await todoService.Update(id, newTodo);

                if (todo == null)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });

            app.MapDelete("/todo/{id}", async (int id, ITodoService todoService) =>
            {
                var todo = await todoService.Remove(id);
                if (todo == null)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });
        }
    }
}
