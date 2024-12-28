using System.IO;

namespace _Project.Scripts.Template.GameDataEditor.Editor.DefaultPathEditor
{
    public static class FileHelper
    {
        public static void SaveFile(string path, string content)
        {
            string fullPath = path + content;
            Directory.Delete(fullPath, true);
            Directory.CreateDirectory(fullPath);
        }
    }
}