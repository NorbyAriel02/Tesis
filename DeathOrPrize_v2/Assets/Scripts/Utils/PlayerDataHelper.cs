using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHelper 
{
    #region Posicion
    public static PlayerPositionModel GetPosition()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        
        return data.position;
    }
    public static Vector3 GetVectorPosition()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);

        return new Vector3(data.position.X, data.position.Y, data.position.Z);
    }
    public static void SaveStartPositionPlayerKingdom1(Vector3 pos)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data == null)
            data = new PlayerDataModel();
                
        data.startPosition.X = pos.x;
        data.startPosition.Y = pos.y + 2;
        data.startPosition.Z = pos.z;
        
        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    } 
    public static Vector3 GetStartPosition()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        
        return new Vector3(data.startPosition.X, data.startPosition.Y, 0);
    }
    public static int GetIdKingdom()
    {
        int id = -1;
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            id = data.position.KingdomID;

        return id;
    }
    public static void UpdateIdKingdom(int id)
    {        
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            data.position.KingdomID = id;

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static void UpdatePosition(Vector3 pos)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.position.X = pos.x;
            data.position.Y = pos.y;
            data.position.Z = pos.z;
        }           

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    #endregion

    #region monedas
    public static void UpdateCoins(int coins)
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.coins = coins;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }

    public static string GetCoins()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
            return data.coins.ToString();

        return "0";
    }
    #endregion

    public static void SetTutorialPositionPlayer()
    {
        Vector3 pos = new Vector3(0,0,0);
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
    
    public static PlayerStatsModel GetStats()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
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
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            data.stats.currentHealth -= value;
        }

        fileController.SaveEncrypted<PlayerDataModel>(data, PathHelper.PlayerDataFile);
    }
    public static float GetCurrentHealth()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            return data.stats.currentHealth;
        }

        return 0;
    }
    public static float GetMaxHealth()
    {
        DataFileController fileController = new DataFileController();
        PlayerDataModel data = fileController.GetEncryptedData<PlayerDataModel>(PathHelper.PlayerDataFile);
        if (data != null)
        {
            return data.stats.maxHealth;
        }

        return 0;
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
    #endregion
}