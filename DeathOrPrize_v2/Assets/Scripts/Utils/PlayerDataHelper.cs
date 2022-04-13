using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHelper 
{
    #region Posicion
    public static PlayerPositionModel GetPosition()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data == null || data.position == null)
        {
            SetStartDataPlayer(new Vector3(0, 0, 0));
            data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        }

        return data.position;
    }
    public static void UpdatePosition()
    {

    }    
    public static int GetIdKingdom()
    {
        int id = -1;
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            id = data.position.KingdomID;

        return id;
    }
    public static void UpdateIdKingdom(int id)
    {        
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            data.position.KingdomID = id;

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void UpdatePosition(Vector3 pos)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.position.X = pos.x;
            data.position.Y = pos.y;
            data.position.Z = pos.z;
        }           

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    #endregion

    #region monedas
    public static void UpdateCoins(int coins)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.coins = coins;
        }

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }

    public static string GetCoins()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            return data.coins.ToString();

        return "0";
    }
    #endregion

    public static void SetStartDataPlayer(Vector3 pos)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = new PlayerDataModel();
        if (data != null)
        {
            data.position.X = pos.x;
            data.position.Y = pos.y;
            data.position.Z = pos.z;
            data.position.KingdomID = 0;
            data.stats.maxHealth = 50;
        }

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static PlayerStatsModel GetStats()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            return data.stats;
        }

        return null;
    }

    #region Salud
    public static void RestHealth(float value)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.stats.currentHealth -= value;
        }

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static float GetCurrentHealth()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            return data.stats.currentHealth;
        }

        return 0;
    }
    public static float GetMaxHealth()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            return data.stats.maxHealth;
        }

        return 0;
    }
    public static void Heal()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.stats.currentHealth = data.stats.maxHealth;
        }

        fileController.Save<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    #endregion
}
