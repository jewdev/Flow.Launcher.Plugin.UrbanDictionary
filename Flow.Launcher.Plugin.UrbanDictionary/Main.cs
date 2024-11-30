using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    /// <summary>
    /// Plugin for searching Urban Dictionary definitions.
    /// </summary>
    public class UrbanDictionary : IPlugin
    {
        private readonly UrbanDictionaryService _service;
        private PluginInitContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrbanDictionary"/> class.
        /// </summary>
        public UrbanDictionary()
        {
            _service = new UrbanDictionaryService();
        }

        /// <summary>
        /// Initializes the plugin with the provided context.
        /// </summary>
        /// <param name="context">The plugin initialization context.</param>
        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executes the search query synchronously.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of search results.</returns>
        public List<Result> Query(Query query)
        {
            return QueryAsync(query).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Executes the search query asynchronously.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A task that represents the asynchronous operation and contains a list of search results.</returns>
        public async Task<List<Result>> QueryAsync(Query query)
        {
            string term = query.Search;

            if (string.IsNullOrWhiteSpace(term))
            {
                return ResultFactory.CreateEmptySearchResult();
            }

            try
            {
                var definitions = await _service.GetDefinitionsAsync(term);
                var results = new List<Result>();

                foreach (var definition in definitions)
                {
                    results.Add(ResultFactory.CreateDefinitionResult(
                        definition,
                        url => _context.API.OpenUrl(url)
                    ));
                }

                return results;
            }
            catch (Exception ex)
            {
                return ResultFactory.CreateErrorResult(ex);
            }
        }
    }
}