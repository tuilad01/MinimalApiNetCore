using MinimalApiNetCore.Database;

namespace Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly MyDbContext _db;
        public TodoService(MyDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Todo> GetAll()
        {
            var todos = _db.Todos.ToList();
            return todos;
        }

        public async Task<Todo?> Get(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            return todo;
        }

        public async Task<Todo?> Update(int id, Todo newTodo)
        {
            var todo = await _db.Todos.FindAsync(id);
            if (todo != null)
            {
                todo.Title = newTodo.Title;
                todo.Description = newTodo.Description;
                todo.IsCompleted = newTodo.IsCompleted;
                todo.UpdatedAt = DateTime.Now;

                _db.Todos.Update(todo);
                await _db.SaveChangesAsync();
            }

            return todo;
        }

        public async Task<Todo?> Remove(int id)
        {
            var todo = await _db.Todos.FindAsync(id);
            if (todo != null )
            {
                _db.Todos.Remove(todo);
                await _db.SaveChangesAsync();
            }

            return todo;
        }

        public async Task<Todo?> Add(Todo newTodo)
        {
            var todo = new Todo
            {
                Title = newTodo.Title,
                Description = newTodo.Description,
                IsCompleted = newTodo.IsCompleted,
            };
            _db.Todos.Add(todo);
            await _db.SaveChangesAsync();

            return todo;
        }
    }
}
