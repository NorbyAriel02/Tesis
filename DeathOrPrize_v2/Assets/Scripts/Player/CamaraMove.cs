using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform playerPosition;
    public GameObject LoadKingdom;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (CameraIsInPlayerPosition())
        {
            LoadKingdom.SetActive(false);
            moveSpeed = 2;
        }
        
    }
    public void ActiveLoadKingdom()
    {
        LoadKingdom.SetActive(true);
        moveSpeed = 99;        
    }
    
    void Move()
    {
        newPos = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
    }

    bool CameraIsInPlayerPosition()
    {
        if (transform.position.x == playerPosition.position.x && transform.position.y == playerPosition.position.y)
            return true;

        return false;
    }
}
