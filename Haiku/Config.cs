namespace Haiku
{
    public class Config
    {
        public File File;

        public Config(Folder folder, string name, string extension)
        {
            File = new File(folder, $"{name}.{extension}");
        }
    }
}