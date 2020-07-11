using UnityEngine;

public class FileCreator
{
    private string path;
    private bool initialized;

    private void Initialize()
    {
        path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }

        initialized = true;
    }

    public void CreateTxtFile(string name, string content)
    {
        if (!initialized)
            Initialize();

        System.IO.File.WriteAllText($@"{path}\{name}.txt", content);
    }
}