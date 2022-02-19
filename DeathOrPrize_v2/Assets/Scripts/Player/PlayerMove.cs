using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int diceValue = 0;
    public float moveSpeed = 5f;
    public Transform playerMovePoint;
    private float _moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = moveSpeed;
        playerMovePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, _moveSpeed * Time.deltaTime);
    }
    public void SetPosition(float x, float y)
    {
        _moveSpeed = moveSpeed;
        playerMovePoint.position = new Vector3(x, y, playerMovePoint.position.z);        
    }
    public void SetPositionNewKingdom(float x, float y)
    {
        playerMovePoint.position = new Vector3(x, y, playerMovePoint.position.z);
        _moveSpeed = moveSpeed * 100;
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
