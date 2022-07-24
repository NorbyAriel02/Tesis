using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data;
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
	public static void SaveEncryptedV2<T>(object obj, string path)
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
	public static T GetEncryptedDataV2<T>(string path) where T : class, new()
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
	public T LoadFile<T>(string path) where T : class, new()
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
			using (var reader = new StreamReader(path))
			{				
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split('|');
					if (dt.Columns.Count == 0)
						dt = CreateCol(dt, values);

					DataRow row = dt.NewRow();
					for (int col = 0; col < dt.Columns.Count; col++)
						row[col] = values[col];

					dt.Rows.Add(row);
				}
			}
		}
		catch (System.Exception e)
		{			
			Logger.WriteLog(e.Message + " " + e.StackTrace);
		}
		return dt;
	}
	DataTable CreateCol(DataTable dt, string[] cols)
    {
		for (int x = 0; x < cols.Length; x++)
			dt.Columns.Add("col" + x);

		return dt;
    }
	public Sprite LoadSprite(string imageName, string spriteName)
	{
		Sprite[] all = Resources.LoadAll<Sprite>(imageName);

		foreach (var s in all)
		{
			if (s.name == spriteName)
			{
				return s;
			}
		}
		return null;
	}

	public Sprite[] LoadSpriteSheet(string imageName)
	{
		Sprite[] Sheet = Resources.LoadAll<Sprite>(imageName);
				
		return Sheet;
	}
}