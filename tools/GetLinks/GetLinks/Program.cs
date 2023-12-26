using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GetLinks
{
    class Program
    {
        private static string RepoURL = string.Empty;

        private static void Main(string[] args)
        {
            string rootPath = Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            GetGitgubRepo(rootPath);
            if (RepoURL.Length > 0)
            {
                GetFilesByDir(rootPath);
            }
            Console.ReadKey();
        }

        private static void GetGitgubRepo(string dir)
        {
            DirectoryInfo[] dirs = new DirectoryInfo(dir).GetDirectories();
            IEnumerable<DirectoryInfo> matchDir = from dirinfo in dirs where dirinfo.Name == ".git" select dirinfo;
            if (matchDir.Count() > 0)
            {
                FileInfo[] files = matchDir.First().GetFiles();
                IEnumerable<FileInfo> matchFile = from dirinfo in files where dirinfo.Name == "config" select dirinfo;
                if (matchFile.Count() > 0)
                {
                    string[] lines = File.ReadAllLines(matchFile.First().FullName);
                    IEnumerable<string> matchLine = from lne in lines where lne.Contains("https://github.com") select lne;
                    if (matchLine.Count() > 0)
                    {
                        RepoURL = matchLine.First().Replace(" ", "").Replace("url=", "").Replace("\t", "").Replace("/blob", "").Insert(8, "raw.");
                        RepoURL = RepoURL.Substring(0, RepoURL.LastIndexOf(".")) + "/master";
                    }
                }
            }
        }

        private static void GetFilesByDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            string dddir = Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            string addDir = dirPath.Replace(dddir, "").Replace("\\", "/");
            if (addDir.Length > 0)
            {
                Console.WriteLine("\r\n" + addDir);
            }
            DirectoryInfo dir = new DirectoryInfo((dirPath));
            foreach (FileInfo item in dir.GetFiles())
            {
                Console.WriteLine(RepoURL + ("/" + addDir + "/").Replace("//", "/") + item.Name);
            }

            foreach (DirectoryInfo item in dir.GetDirectories())
            {
                if (item.Name == ".git" || item.Name == "tools")
                {
                    continue;
                }
                GetFilesByDir(item.FullName);
            }
        }
    }
}
