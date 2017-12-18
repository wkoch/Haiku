using System;
using System.IO;
using System.Collections.Generic;

namespace Haiku
{
    public class WebSite
    {
        readonly private string[] _folders = { "pages", "posts", "public", "template" };
        readonly private Config _config = new Config { Name = "Haiku", Extension = "conf" };
        public static Status status = Status.Nothing;
        // private bool IsHaikuProject(string path) => (Directory.Exists(path) && File.Exists(Path.Combine(path, ConfigFile)));
        public void New()
        {
            // create folders
            // create files
        }

        public void Build()
        {
            // check existing project
            // read config
            // load template
            // load content
            // copy static files
            // build html files
        }
    }
}