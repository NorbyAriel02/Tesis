using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data;
using UnityEngine;
using ExcelDataReader;

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
	public void SaveEncrypted<T>(object obj, string path)
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
	public T GetEncryptedData<T>(string path) where T : class, new()
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

	public DataTable GetData(string path) 
	{
		if (!File.Exists(path))
			return null;

		DataTable dt = new DataTable();		
		try
		{
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
					var result = reader.AsDataSet();
                    // Ejemplos de acceso a datos
                    dt = result.Tables[0];
                }
			}

            //using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            //{
            //	using (var reader = ExcelReaderFactory.CreateReader(stream))
            //	{
            //		var result = reader.AsDataSet();
            //		// Ejemplos de acceso a datos
            //		dt = result.Tables[0];					
            //	}
            //}
        }
		catch (System.Exception e)
		{
			Debug.Log(e.Message + " " + e.StackTrace);
		}
		return dt;
	}

}