using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerDataModel 
{
    public PlayerPositionModel startPosition;
    public PlayerPositionModel position;
    public PlayerStatsModel stats;
    public int coins;

    public PlayerDataModel()
    {
        startPosition = new PlayerPositionModel();
        position = new PlayerPositionModel();
        stats = new PlayerStatsModel();
    }
}
