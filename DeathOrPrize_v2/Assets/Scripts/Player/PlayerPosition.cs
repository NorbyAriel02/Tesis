using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Transform targetPosPlayer;
    public int kingdomID;
    
    private void Awake()
    {        
        PlayerPositionModel pos = PlayerDataHelper.GetPosition();
        if (pos != null)
        {
            transform.position = new Vector3(pos.X, pos.Y, pos.Z);
            targetPosPlayer.position = new Vector3(pos.X, pos.Y, pos.Z);
            kingdomID = pos.KingdomID;
        }
    }
    public void TrasladePlayerStartPosition()
    {
        PlayerPositionModel pos = PlayerDataHelper.GetStartPositionRespawn();
        if (pos != null)
        {
            transform.position = new Vector3(pos.X, pos.Y, pos.Z);
            targetPosPlayer.position = new Vector3(pos.X, pos.Y, pos.Z);
            kingdomID = pos.KingdomID;
        }
    }
    public void UpdatePosition()
    {        
        PlayerDataHelper.UpdatePosition(transform.position);
    }
    void OnDestroy()
    {
        UpdatePosition();
    }
}
