using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dictionary
{
    public interface IDictionaryService
    {
        Task<IEnumerable<Dictionary>> GetAll();
        Task<Dictionary?> Get(int id);
        Task<Dictionary?> GetByKey(string name);
        Task<Dictionary?> Add(string name, string value);
        Task<Dictionary?> Update(string name, string value);
        Task<Dictionary?> Remove(string name);
    }
}
