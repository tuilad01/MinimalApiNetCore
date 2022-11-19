using MinimalApiNetCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dictionary
{
    public class DictionaryService : IDictionaryService
    {
        private readonly MyDbContext _db;
        public DictionaryService(MyDbContext db)
        {
            _db = db;
        }
        public Task<IEnumerable<Dictionary>> GetAll()
        {
            var dictionaries = _db.Dicitionaries.ToArray();
            return Task.FromResult<IEnumerable<Dictionary>>(dictionaries);
        }

        public async Task<Dictionary?> Get(int id)
        {
            var dictionary = await _db.Dicitionaries.FindAsync(id);
            return dictionary;
        }

        public Task<Dictionary?> GetByKey(string name)
        {
            var dictionary = _db.Dicitionaries.FirstOrDefault(dic => dic.Name == name);
            return Task.FromResult(dictionary);
        }


        public async Task<Dictionary?> Add(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) return null;

            var dictionary = _db.Dicitionaries.FirstOrDefault(dic => dic.Name == name);

            if (dictionary == null)
            {
                // add new
                var newDictionary = new Dictionary()
                {
                    Name = name,
                    Value = value
                };
                _db.Dicitionaries.Add(newDictionary);
                await _db.SaveChangesAsync();

                return newDictionary;
            }

            return null;
        }

        public async Task<Dictionary?> Update(string name, string value)
        {
            var dictionary = _db.Dicitionaries.FirstOrDefault(dic => dic.Name == name);

            if (dictionary != null)
            {
                // update
                dictionary.Value = value;
                dictionary.UpdatedAt = DateTime.Now;

                _db.Dicitionaries.Update(dictionary);
                await _db.SaveChangesAsync();

                return dictionary;
            }
            // return null
            return dictionary;
        }

        public async Task<Dictionary?> Remove(string name)
        {
            var dictionary = _db.Dicitionaries.FirstOrDefault(dic => dic.Name == name);

            if (dictionary != null)
            {
                _db.Dicitionaries.Remove(dictionary);
                await _db.SaveChangesAsync();

                return dictionary;
            }
            // return null
            return dictionary;
        }
    }
}
