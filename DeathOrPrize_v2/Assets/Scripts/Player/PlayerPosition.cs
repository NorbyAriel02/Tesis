using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Transform targetPosPlayer;
    public int kingdomID;
    DataFileController fileController = new DataFileController();
    private void Awake()
    {
        PlayerPositionModel pos = fileController.GetData<PlayerPositionModel>(PathHelper.PlayerPositionDataFile);
        if (pos != null)
        {
            transform.position = new Vector3(pos.X, pos.Y, pos.Z);
            targetPosPlayer.position = new Vector3(pos.X, pos.Y, pos.Z);
            kingdomID = pos.KingdomID;
        }
    }

    void Start()
    {
                    
    }

    public void UpdatePosition()
    {
        PlayerPositionModel pos = new PlayerPositionModel(transform.position, kingdomID);
        fileController.Save<PlayerPositionModel>(pos, PathHelper.PlayerPositionDataFile);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        
        UpdatePosition();
    }
}
