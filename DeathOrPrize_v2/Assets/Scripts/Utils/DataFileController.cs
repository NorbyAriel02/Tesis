using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class DataFileController 
{
	public DataFileController()
	{

	}
	public bool Exists(string path)
    {
		if (File.Exists(path))
			return true;

		return false;
    }
	public void Save<T>(object obj, string path)
	{
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        try
		{
            T _obj = (T)obj;
            
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, _obj);
        }
		catch (SerializationException e)
		{            
			Debug.Log(e.Message + " " + e.StackTrace);		
		}
		finally
		{
			fs.Close();
		}
	}
	public void Log(string msj, string path)
	{	
		try
		{	
			using (StreamWriter streamWriter = new StreamWriter(path, true))
			{
				streamWriter.Write(msj);
				streamWriter.Close();
			}
		}
		catch (SerializationException e)
		{
			Debug.Log(e.Message + " " + e.StackTrace);
		}	
	}
	public T GetData<T>(string path) where T : class, new()
	{
        if (!File.Exists(path))
            return null;

		T deserializedObject = null;
		FileStream fs = new FileStream(path, FileMode.Open);
		try
		{
			BinaryFormatter formatter = new BinaryFormatter();
			deserializedObject = (T)formatter.Deserialize(fs);
		}
		catch (SerializationException e)
		{
			Debug.Log(e.Message + " " + e.StackTrace);			
		}
		finally
		{
			fs.Close();
		}
		return deserializedObject;
	}

    
}