using System;
using System.Globalization;
using HeyRed.MarkdownSharp;
using System.Text.RegularExpressions;
using StringReader = System.IO.StringReader;

namespace Haiku
{
    public class Post
    {
        public string Name;
        public string Path;
        public DateTime Date;
        public string Title;
        public string SubTitle;
        public string Content;
        public string Markdown;
        public HTML HTML;

        public void Process()
        {
            var datePattern = @"(\d+-\d+-\d+)";
            var dateMatch = new Regex(datePattern, RegexOptions.IgnoreCase);
            var Date = DateTime.ParseExact(dateMatch.Match(Name).Groups[1].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Content = System.IO.File.ReadAllText(Path);
            var markdown = new Markdown();
            var contents = new StringReader(Content);
            Title = contents.ReadLine();
            SubTitle = contents.ReadLine();
            Markdown = markdown.Transform(contents.ReadToEnd());
        }
    }
}
