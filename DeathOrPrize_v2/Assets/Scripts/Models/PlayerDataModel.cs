using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerDataModel 
{
    public PlayerPositionModel position;
    public PlayerStatsModel stats;
    public int coins;

    public PlayerDataModel()
    {
        position = new PlayerPositionModel();
        stats = new PlayerStatsModel();
    }
}
