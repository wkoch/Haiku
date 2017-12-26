namespace Haiku
{
    public class Template
    {
        public string Name;
        public string Path;
        public string Content;

        public void Process()
        {
            Content = System.IO.File.ReadAllText(Path);
        }
    }
}
