using System;
using UnityEngine;
[Serializable]
public class PlayerPositionModel 
{
    public int KingdomID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public PlayerPositionModel()
    {

    }
    public PlayerPositionModel(Vector3 value, int _kingdomID)
    {
        KingdomID = _kingdomID;
        X = value.x;
        Y = value.y;
        Z = value.z;
    }
}
