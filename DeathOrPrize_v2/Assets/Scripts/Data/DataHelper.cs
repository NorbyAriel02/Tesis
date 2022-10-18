using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public class DataHelper 
{    
    public delegate void ItemPickup(ItemModel item);
    public static ItemPickup OnItemPickup;
    public delegate void RemoveItem(ItemModel item);
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
    public static List<ItemModel> GetListMarket()
    {
        return GetListItems(pathMarket);
    }
    public static void AddItemMarket(ItemModel item)
    {
        Add(item, pathMarket);        
    }
    public static void CreateMarket(List<ItemModel> items)
    {
        InventoryModel market = GetInventory(pathMarket);
        market.items = items;        
        DataFileController.SaveEncryptedV2<InventoryModel>(market, pathMarket);
    }
    public static void RemoveItemMarket(ItemModel item)
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
    public static void RestHealth(float value)
    {
        PlayerDataModel data = GetDataPlayer();
        if (data != null)
        {
            data.stats.currentHealth -= value;
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
    public static void AddItemInventory(ItemModel item)
    {        
        Add(item, pathInventory);
        OnItemPickup?.Invoke(item);
    }
    public static void RemoveItemInventory(ItemModel item)
    {        
        Remove(item, pathInventory);         
    }
    public static void UpdateItemInventory(ItemModel item)
    {        
        UpdateItem(item, pathInventory);
    }
    public static bool ItemExistsInInventory(ItemModel item)
    {        
        return ItemExists(item, pathInventory);
    }
    public static List<ItemModel> GetListInventory()
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
    public static void ToEquip(ItemModel item)
    {
        Add(item, pathEquip);        
    }
    public static void Unequip(ItemModel item)
    {
        Remove(item, pathEquip);        
    }
    public static List<ItemModel> GetListEquip()
    {
        return GetListItems(pathEquip);
    }
    #endregion
    #region genericos
    static bool ItemExists(ItemModel item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemModel auxItem = inventory.items[i];
            if (item.name == auxItem.name)
            {
                return true;
            }
        }

        return false;
    }
    //Tengo que arreglar este metodo
    static void UpdateItem(ItemModel item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemModel auxItem = inventory.items[i];
            if (item.name == auxItem.name)
            {
                inventory.items.Remove(auxItem);
                inventory.items.Add(item);
                break;
            }
        }

        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, path);
    }
    static void Add(ItemModel item, string path)
    {
        InventoryModel inventory = GetInventory(path);
        if (item.IsStackable)
        {
            ItemModel aux = GetStack(item, inventory);
            if(aux != null)
            {
                aux.Stack++;
                UpdateItem(aux, path);
            }
            else
                inventory.items.Add(item);
        }
        else
        {
            inventory.items.Add(item);
        }
        DataFileController.SaveEncryptedV2<InventoryModel>(inventory, path);
    }  
    static ItemModel GetStack(ItemModel item, InventoryModel inventory)
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemModel auxItem = inventory.items[i];
            if (!auxItem.IsStackable)
                continue;

            if (item.GetType() == auxItem.GetType())
            {
                return auxItem;
            }
        }
        return null;
    }
    static void Remove(ItemModel item, string path)
    {
        InventoryModel inventory = GetInventory(path);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemModel auxItem = inventory.items[i];
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

    static List<ItemModel> GetListItems(string path)
    {
        InventoryModel inventory = GetInventory(path);

        return inventory.items;
    }
    #endregion
    
    public static EnemiesXcellModel GetStatsEnemies(int idCell, int idKingdom)
    {        
        List<EnemiesXcellModel> enemies = DataFileController.GetEncryptedDataV2<List<EnemiesXcellModel>>(PathHelper.EnemiesDataFile(idKingdom));
        return enemies[idCell];
    }
    public static void RemoveItemList(ItemModel item)
    {
        string path = PathHelper.PlayerDataFile; 
        if (files == null)
            LoadPathFiles();

        if (files.ContainsKey(item.DataFile))
            path = files[item.DataFile];

        Remove(item, path);
    }
    public static List<ItemModel> GetItems(string dataFile)
    {
        string path = "";
        if (files == null)
            LoadPathFiles();

        if (files.ContainsKey(dataFile))
            path = files[dataFile];

        return GetListItems(path);
    }
    public static void AddItemList(ItemModel item)
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
        }
    }


}
