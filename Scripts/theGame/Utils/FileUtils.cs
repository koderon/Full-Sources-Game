using System.IO;
using System.Text;
using UnityEngine;

namespace theGame
{
    
    public static class FileUtils
    {

        public static void SaveTextToFile(string fullFileName, string text)
        {
            var path = PathUtils.Get() + fullFileName;
            var file = new FileInfo(path);

            if (!file.Exists)
            {
                file.Create().Close();
            }

            using (StreamWriter sw = new StreamWriter(file.ToString()))
            {
                sw.Write(text);
                sw.Close();
            }
        }

        public static string LoadTextFromFile(string fullFileName)
        {
            var path = PathUtils.Get() + fullFileName;

            var text = "";

            if (File.Exists(path))
            {
                var fileReader = new StreamReader(path, Encoding.ASCII);
                text = fileReader.ReadLine();
                fileReader.Close();
            }

            return text;
        }

        public static string LoadTextFromResources(string fullFileName)
        {
            var res = Resources.Load<TextAsset>(fullFileName);

            if(res != null)
                return res.text;

            Debug.LogError("File := " + fullFileName + " not found!");

            return null;
        }
    }

}