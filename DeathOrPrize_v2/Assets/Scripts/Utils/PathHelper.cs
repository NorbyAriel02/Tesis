﻿using System.IO;
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

        return GetPlatformPath(FilePath);
    }
    public static string EnemiesDataFile(int IdKingdom)
    {
        string FilePath = string.Format(MyAppConfig.EnemiesDataFile, IdKingdom);
        return GetPlatformPath(FilePath);
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
    public static string MarketDataFile
    {
        get
        {
            string FilePath = MyAppConfig.MarketDataFile;

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
    public static string PlayerDataFile
    {
        get
        {
            string FilePath = MyAppConfig.PlayerDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string QuestDataFile(string idQuest)
    {
        string FilePath = string.Format(MyAppConfig.QuestDataFile, idQuest);

        return GetPlatformPath(FilePath);
    }
    public static string PlayerStatsDataFile
    {
        get
        {
            string FilePath = MyAppConfig.PlayerStatsDataFile;

            return GetPlatformPath(FilePath);
        }
    }
    public static string LogFile
    {
        get
        {
            string FilePath = System.DateTime.Now.ToString("yyyyMMdd") + MyAppConfig.LogFile;

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
