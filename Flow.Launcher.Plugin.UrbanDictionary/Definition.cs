namespace Flow.Launcher.Plugin.UrbanDictionary
{
    public class Definition
    {
        public string Word { get; set; }
        public string DefinitionText { get; set; }
        public string Permalink { get; set; }
        public int ThumbsUp { get; set; }
        public int ThumbsDown { get; set; }
        public string Author { get; set; }
        public int DefId { get; set; }

        public Definition(string word, string definitionText, string permalink, int thumbsUp, int thumbsDown, string author, int defId)
        {
            Word = word;
            DefinitionText = definitionText;
            Permalink = permalink;
            ThumbsUp = thumbsUp;
            ThumbsDown = thumbsDown;
            Author = author;
            DefId = defId;
        }
    }
}
