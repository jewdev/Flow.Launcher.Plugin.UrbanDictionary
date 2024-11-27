using System.Collections.Generic;
using System;
using System.Windows;
using System.Text.Json;
using System.Net.Http;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    public class UrbanDictionary : IPlugin, IAsyncPlugin
    {
        private readonly string URL = "https://api.urbandictionary.com/v0";
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        public List<Result> Query(Query query)
        {
            string term = query.Search;

            if (string.IsNullOrWhiteSpace(term))
            {
                return new List<Result>{
                    new()
                    {
                        Title = "Search for a term",
                        SubTitle = "Enter a word to search for in Urban Dictionary.",
                        IcoPath = "icon.png",
                    }
                };
            }

            try
            {
                List<Result> definitions = GetDefinitions(term);
                return definitions;
            }
            catch (Exception ex)
            {
                return new List<Result>
                {
                    new()
                    {
                        Title = "Error",
                        SubTitle = ex.Message,
                        IcoPath = "icon.png",
                        Action = _ =>
                        {
                            _context.API.CopyToClipboard(ex.ToString());
                            return true;
                        }
                    }
                };
            }
        }

        private List<Result> GetDefinitions(string term)
        {
            string url = $"{URL}/define?term={term}";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Flow.Launcher.Plugin.UrbanDictionary");

            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;
            List<Definition> definitions = new List<Definition>();

            JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            foreach (JsonElement element in root.GetProperty("list").EnumerateArray())
            {
                Definition definition = new Definition(
                    element.GetProperty("word").GetString(),
                    element.GetProperty("definition").GetString(),
                    element.GetProperty("permalink").GetString(),
                    element.GetProperty("thumbs_up").GetInt32(),
                    element.GetProperty("thumbs_down").GetInt32(),
                    element.GetProperty("author").GetString(),
                    element.GetProperty("defid").GetInt32()
                );

                definitions.Add(definition);
            }

            // Sort definitions by thumbs up in descending order
            definitions.Sort((a, b) => b.ThumbsUp.CompareTo(a.ThumbsUp));

            List<Result> results = new List<Result>();
            foreach (Definition definition in definitions)
            {
                Result result = new Result
                {
                    Title = definition.DefinitionText,
                    SubTitle = $"👍 {definition.ThumbsUp} / 👎 {definition.ThumbsDown}",
                    IcoPath = "icon.png",
                    Action = _ =>
                    {
                        _context.API.OpenUrl(definition.Permalink);
                        return true;
                    }
                };

                results.Add(result);
            }

            return results;
        }
    }
}