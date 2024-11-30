using System;
using System.Collections.Generic;
using System.Windows;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    internal static class ResultFactory
    {
        public static List<Result> CreateEmptySearchResult() =>
            new() {
                new Result
                {
                    Title = "Search for a term",
                    SubTitle = "Enter a word to search for in Urban Dictionary.",
                    IcoPath = "icon.png"
                }
            };

        public static List<Result> CreateErrorResult(Exception ex) =>
            new() {
                new Result
                {
                    Title = "Error",
                    SubTitle = ex.Message,
                    IcoPath = "icon.png",
                    Action = _ => {
                        Clipboard.SetText(ex.ToString());
                        return true;
                    }
                }
            };

        public static Result CreateDefinitionResult(Definition definition, Action<string> urlOpener) =>
            new()
            {
                Title = definition.DefinitionText,
                SubTitle = $"👍 {definition.ThumbsUp} / 👎 {definition.ThumbsDown}",
                IcoPath = "icon.png",
                Action = _ => {
                    urlOpener(definition.Permalink);
                    return true;
                }
            };
    }
} 