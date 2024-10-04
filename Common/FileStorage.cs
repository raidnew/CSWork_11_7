using System.Diagnostics;
using System.IO;

namespace Task.Common;

public class FileStorage
{
    public Action<List<string>> OnFileLoaded;
    private string _fileName;

    public FileStorage(string fileName)
    {
        _fileName = fileName;
    }

    public void SaveFile(List<string> dataStrings)
    {
        FileStream _fileHandle = File.Open(_fileName, FileMode.Create, FileAccess.Write);
        StreamWriter streamWriter = new StreamWriter(_fileHandle);
        foreach (string data in dataStrings)
        {
            streamWriter.WriteLine(data);
        }
        streamWriter.Close();
    }

    public void LoadFile()
    {
        List<string> data = new List<string>();
        try
        {
            FileStream _fileHandle = File.Open(_fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(_fileHandle);
            string? fileString = streamReader.ReadLine();
            while (fileString != null)
            {
                data.Add(fileString);
                fileString = streamReader.ReadLine();
            }
            _fileHandle.Close();
        }
        catch (FileNotFoundException e)
        {
            Trace.WriteLine(e);
        }
        OnFileLoaded?.Invoke(data);
    }

}