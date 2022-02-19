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
        PlayerPositionData pos = fileController.GetData<PlayerPositionData>(PathHelper.PlayerPositionDataFile);
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
        PlayerPositionData pos = new PlayerPositionData(transform.position, kingdomID);
        fileController.Save<PlayerPositionData>(pos, PathHelper.PlayerPositionDataFile);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            UpdatePosition();
    }
    void OnDestroy()
    {
        Debug.Log("Se llama al finalizar el juego?");
        UpdatePosition();
    }
}
[Serializable]
public class PlayerPositionData
{
    public int KingdomID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public PlayerPositionData()
    {
    
    }
    public PlayerPositionData(Vector3 value, int _kingdomID)
    {
        KingdomID = _kingdomID;
        X = value.x;
        Y = value.y;
        Z = value.z;
    }
}
