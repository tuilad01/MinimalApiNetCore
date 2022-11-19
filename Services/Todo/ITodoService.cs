using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Todo
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetAll();
        Task<Todo?> Get(int id);
        Task<Todo?> Add(Todo newTodo);
        Task<Todo?> Update(int id, Todo newTodo);
        Task<Todo?> Remove(int id);
    }
}
