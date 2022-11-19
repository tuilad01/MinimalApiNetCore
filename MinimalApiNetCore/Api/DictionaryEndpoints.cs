using Services.Dictionary;
using Services.Todo;

namespace MinimalApiNetCore.Api
{
    public static class DictionaryEndpoints
    {
        public static void MapDictionaryEndpoints(this WebApplication app)
        {
            app.MapGet("/dictionary", async (IDictionaryService dictionaryService) =>
            {
                var dictionaries = await dictionaryService.GetAll();
                return Results.Ok(dictionaries);
            });

            app.MapGet("/dictionary/{name}", async (string name, IDictionaryService dictionaryService) =>
            {

                var dictionary = await dictionaryService.GetByKey(name);
                if (dictionary != null)
                {
                    return Results.Ok(dictionary);
                }

                return Results.NotFound();
            });

            app.MapPost("/dictionary", async (Dictionary newDictionary, IDictionaryService dictionaryService) =>
            {
                var dictionary = await dictionaryService.Add(newDictionary.Name, newDictionary.Value);

                // Cannot add
                if (dictionary == null || dictionary.Id == 0)
                {
                    return Results.NoContent();
                }

                return Results.Created($"/dictionary/{dictionary.Name}", dictionary);
            });


            app.MapPut("/dictionary/{name}", async (string name, Dictionary newDictionary, IDictionaryService dictionaryService) =>
            {
                var dictionary = await dictionaryService.Update(name, newDictionary.Value);

                if (dictionary == null)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });

            app.MapDelete("/dictionary/{name}", async (string name, IDictionaryService dictionaryService) =>
            {
                var dictionary = await dictionaryService.Remove(name);
                if (dictionary == null)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            });
        }
    }
}
