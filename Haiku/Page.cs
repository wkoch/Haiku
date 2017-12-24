using HeyRed.MarkdownSharp;
using StringReader = System.IO.StringReader;

namespace Haiku
{
    public class Page
    {
        public string Name;
        public string Path;
        public string Title;
        public string SubTitle;
        public string Content;
        public string Markdown;
        public HTML HTML;

        public void Process()
        {
            Content = System.IO.File.ReadAllText(Path);
            var markdown = new Markdown();
            var contents = new StringReader(Content);
            Title = contents.ReadLine();
            SubTitle = contents.ReadLine();
            if (SubTitle == string.Empty)
                SubTitle = null;
            Markdown = markdown.Transform(contents.ReadToEnd());
        }
    }
}
