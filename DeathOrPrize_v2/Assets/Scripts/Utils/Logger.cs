using System.IO;
using System;
  
public abstract class LogBase
{
	public abstract void Log(string message);
    //public abstract void LogError(string message);
}
public class Logger : LogBase
{
    //private string path = PathHelper.Log;
    //private string pathError = PathHelper.LogError;

    public Logger()
	{
		//if (!Directory.Exists(path))
		//	Directory.CreateDirectory(path);

  //      if (!Directory.Exists(pathError))
  //          Directory.CreateDirectory(pathError);

  //      DeleteFileOlds();
	}

	public override void Log(string message)
	{
        string file = PathHelper.LogFile;
        using (StreamWriter streamWriter = new StreamWriter(file, true))
		{
			streamWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
			streamWriter.WriteLine(message);
			streamWriter.WriteLine("------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Close();
		}
	}
    public static void WriteLog(string message)
    {
        string file = PathHelper.LogFile;        
        using (StreamWriter streamWriter = new StreamWriter(file, true))
        {
            streamWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            streamWriter.WriteLine(message);
            streamWriter.WriteLine("------------------------------------------");
            streamWriter.WriteLine();
            streamWriter.Close();
        }
    }
    //public override void LogError(string message)
    //{
    //    string file = DateTime.Now.ToString("yyyyMMdd") + MyAppConfig.FileErrorLog;
    //    file = Path.Combine(pathError, file);
    //    using (StreamWriter streamWriter = new StreamWriter(file, true))
    //    {
    //        streamWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
    //        streamWriter.WriteLine(message);
    //        streamWriter.WriteLine("------------------------------------------");
    //        streamWriter.WriteLine();
    //        streamWriter.Close();
    //    }
    //}

 //   public void DeleteFileOlds()
	//{   
	//	DirectoryInfo infoDir = new DirectoryInfo(path);
	//	FileInfo[] infoFiles = infoDir.GetFiles();
	//	for(int x = infoFiles.Length -1; x >= 0; x--)
	//	{
	//		TimeSpan timer = DateTime.Now - infoFiles[x].LastWriteTime;
	//		if (timer.Days > 60)
	//			File.Delete(infoFiles[x].FullName);
	//	}
	//}
}