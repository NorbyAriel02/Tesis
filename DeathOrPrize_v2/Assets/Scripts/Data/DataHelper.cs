using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public class DataHelper 
{    
    public delegate void ItemPickup(ItemProperties item);
    public static ItemPickup OnItemPickup;
    public delegate void RemoveItem(ItemProperties item);
    public static RemoveItem OnRemoveItem;
 
    static string pathInventory = PathHelper.InventoryDataFile;
    static string pathEquip = PathHelper.EquipmentDataFile;
    static string pathMarket = PathHelper.MarketDataFile;
    static string pathPlayer = PathHelper.PlayerDataFile;
    static string pathFiles = PathHelper.DataFiles;
    static Dictionary<string, string> files;
    #region Kingdom
    public static int GetIdCurrentKingdom()
    {
        PlayerDataModel data = GetDataPlayer();
        return data.position.KingdomID;
    }
    public static void UpdateIdCurrentKingdom(int idKingdom)
    {
        PlayerDataModel data = GetDataPlayer();
        data.position.KingdomID = idKingdom;
        SaveDataPlayer(data);
    }
    #endregion
    #region City
    public static List<ItemProperties> GetListMarket()
    {
        return GetListItems(pathMarket);
    }
    public static void AddItemMarket(ItemProperties item)
    {
        Add(item, pathMarket);        
    }
    public static void CreateMarket(List<ItemProperties> items)
    {
        InventoryModel market = GetInventory(pathMarket);
        market.items = items;
        DataFileController.SaveEncryptedV2<InventoryModel>(market, pathMarket);
    }
    public static void RemoveItemMarket(ItemProperties item)
    {
        Remove(item, pathMarket);        
    }
    #endregion
    #region Player Stats
    public static void SetTutorialPositionPlayer()
    {
        Vector3 pos = new Vector3(-18, 0, 0);
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);

        if (data != null)
        {
            data.position.X = pos.x;
            data.position.Y = pos.y;
            data.position.Z = pos.z;
            data.position.KingdomID = 1;
            data.stats.maxHealth = 25;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void Heal()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.stats.currentHealth = data.stats.maxHealth;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void SaveExp(PlayerStatsModel stats)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.stats.experience = stats.experience;
            data.stats.level = stats.level;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    
    
    public static void ResetCoints()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.coins = 0;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void NewGame()
    {        
        DataFileController.SaveEncryptedV2<PlayerDataModel>(new PlayerDataModel(), pathPlayer);
        DataFileController.SaveEncryptedV2<InventoryModel>(new InventoryModel(), pathEquip);
        DataFileController.SaveEncryptedV2<InventoryModel>(new InventoryModel(), pathInventory);
        SetTutorialPositionPlayer();
        Heal();
        ResetCoints();
        SaveStats(new PlayerStatsModel());
    }
    public static PlayerStatsModel GetStats()
    {
        PlayerDataModel data = GetDataPlayer();
        if (data == null)
        {
            data.stats = new PlayerStatsModel();
        }
        return data.stats;
    }
    public static void SaveStats(PlayerStatsModel stats)
    {        
        PlayerDataModel data = GetDataPlayer();
        if (data != null)
        {
            data.stats = stats;
        }

        DataFileController.SaveEncryptedV2<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void UpdateEquipment(Equipment equipment)
    {
        PlayerDataModel data = GetDataPlayer();
        data.stats.equipment = equipment;
        SaveDataPlayer(data);
    }
    public static int GetCoins()
    {
        return GetDataPlayer().coins;
    }
    public static void AddCoins(int coins)
    {
        PlayerDataModel data = GetDataPlayer();
        data.coins += coins;
        SaveDataPlayer(data);
    }
    static PlayerDataModel GetDataPlayer()
    {
        PlayerDataModel data = DataFileController.GetEncryptedDataV2<PlayerDataModel>(pathPlayer);
        if (data == null)
        {
            data = new PlayerDataModel();
        }
        return data;
    }
    static void SaveDataPlayer(PlayerDataModel player)
    {
        DataFileController.SaveEncryptedV2<PlayerDataModel>(player, pathPlayer);        
    }
    #endregion
    #region inventario
    public static void UpdateSlotsNumberInventory(int slotsNumber)
    {
        InventoryModel inventory = GetInventory(pathInventory);
        inventory.SlotsNumber = slotsNumber;
        SaveInvemtory(inventory);
    }
    public static void AddItemInventory(ItemProperties item)
    {        
        Add(item, pathInventory);
        OnItemPickup?.Invoke(item);
    }
    public static void RemoveItemInventory(ItemProperties item)
    {        
        Remove(item, pathInventory);         
    }
    public static void UpdateItemInventory(ItemProperties item)
    {        
        UpdateItem(item, pathInventory);
    }
    public static bool ItemExistsInInventory(ItemProperties item)
    {        
        return ItemExists(item, pathInventory);
    }
    public static List<ItemProperties> GetListInventory()
    {
        return GetListItems(pathInventory);
    }
    public static int GetSlotsNumberInventory()
    {
        return GetInventory(pathInventory).SlotsNumber;
    }
    public static void SaveInvemtory(InventoryModel inventory)
    {
        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, pathInventory);
    }
    #endregion
    #region Equipo
    public static void ToEquip(ItemProperties item)
    {
        Add(item, pathEquip);        
    }
    public static void Unequip(ItemProperties item)
    {
        Remove(item, pathEquip);        
    }
    public static List<ItemProperties> GetListEquip()
    {
        return GetListItems(pathEquip);
    }
    #endregion
    #region genericos
    static bool ItemExists(ItemProperties item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemProperties auxItem = inventory.items[i];
            if (item.name == auxItem.name)
            {
                return true;
            }
        }

        return false;
    }
    static void UpdateItem(ItemProperties item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemProperties auxItem = inventory.items[i];
            if (item.name == auxItem.name)
            {
                inventory.items.Remove(auxItem);
                inventory.items.Add(item);
                break;
            }
        }

        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, path);
    }
    static void Add(ItemProperties item, string path)
    {
        InventoryModel inventory = GetInventory(path);
        
        inventory.items.Add(item);
        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, path);
    }
    static void Remove(ItemProperties item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemProperties auxItem = inventory.items[i];
            if (item.name == auxItem.name)
            {
                inventory.items.Remove(auxItem);
                break;
            }
        }

        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, path);
    }

    static InventoryModel GetInventory(string path)
    {
        InventoryModel inventory = DataFileController.GetEncryptedDataV2<InventoryModel>(path);

        if (inventory == null)
            inventory = new InventoryModel();

        return inventory;
    }

    static List<ItemProperties> GetListItems(string path)
    {
        InventoryModel inventory = GetInventory(path);

        return inventory.items;
    }
    #endregion
    public static void RemoveItemList(ItemProperties item)
    {
        string path = PathHelper.PlayerDataFile; 
        if (files == null)
            LoadPathFiles();

        if (files.ContainsKey(item.DataFile))
            path = files[item.DataFile];

        Remove(item, path);
    }
    public static List<ItemProperties> GetItems(string dataFile)
    {
        string path = "";
        if (files == null)
            LoadPathFiles();

        if (files.ContainsKey(dataFile))
            path = files[dataFile];

        return GetListItems(path);
    }
    public static void AddItemList(ItemProperties item)
    {
        string path = PathHelper.PlayerDataFile;
        if (files == null)
            LoadPathFiles();

        if (files.ContainsKey(item.DataFile))
            path = files[item.DataFile];

        Add(item, path);
    }
    static void LoadPathFiles()
    {
        DataFileController df = new DataFileController();
        DataTable dt = df.GetData(pathFiles);
        files = new Dictionary<string, string>();
        foreach(DataRow row in dt.Rows)
        {
            string path = PathHelper.GetPlatformPath(row[1].ToString());
            files.Add(row[0].ToString(), path);
            Logger.WriteLog(row[0].ToString() + " es la llave del path " + path);
        }
    }
}
