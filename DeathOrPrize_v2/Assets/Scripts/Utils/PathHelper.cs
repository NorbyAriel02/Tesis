using System.IO;
using UnityEngine;

public class PathHelper {
    private static string GetPlatformPath(string file)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            file = Path.Combine(Application.persistentDataPath, file);
        }
        else
        {
            file = Path.Combine(Application.streamingAssetsPath, file);
        }

        return file;
    }
    public static string WolrdDataFile(int IdKingdom)
    {
        string FilePath = string.Format(MyAppConfig.WorldDataFile, IdKingdom);

        if (Application.platform == RuntimePlatform.Android)
        {
            FilePath = Path.Combine(Application.persistentDataPath, FilePath);
        }
        else
        {
            FilePath = Path.Combine(Application.streamingAssetsPath, FilePath);
        }

        return FilePath;        
    }
    public static string BiomesDataFile
    {
        get { 
            string FilePath = MyAppConfig.BiomesDataFile;

            return GetPlatformPath(FilePath);
        }
    }

    public static string InventoryDataFile
    {
        get
        {
            string FilePath = MyAppConfig.InventoryDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string EquipmentDataFile
    {
        get
        {
            string FilePath = MyAppConfig.EquipmentDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string NeighborKingdomDataFile
    {
        get
        {
            string FilePath = MyAppConfig.NeighborDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string PlayerPositionDataFile
    {
        get
        {
            string FilePath = MyAppConfig.PlayerPositionDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string LogFile
    {
        get
        {
            string FilePath = "Log.txt";

            return GetPlatformPath(FilePath);
        }
    }
    public static string CellTypesDataFile
    {
        get
        {
            string FilePath = MyAppConfig.CellTypesDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string CellSubTypesDataFile
    {
        get
        {
            string FilePath = MyAppConfig.CellSubTypesDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string PathDataOnAndroid
    {
        get
        {            
            return Application.persistentDataPath; 
        }
    }
    public static string ScremShot
    {
        get
        {
            string FilePath = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "ScremShot.png";

            return GetPlatformPath(FilePath);
        }
    }
   
    
}
