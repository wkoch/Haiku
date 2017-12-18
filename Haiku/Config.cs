namespace Haiku
{
    public class Config
    {
        public File File;

        public Config(string path, string name, string extension)
        {
            File = new File(path, $"{name}.{extension}");
        }
    }
}