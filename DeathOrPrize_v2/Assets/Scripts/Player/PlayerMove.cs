using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int diceValue = 0;
    public float moveSpeed = 5f;
    public Transform playerMovePoint;
    
    private CamaraMove cam;
    private float _moveSpeed = 5f;
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamaraMove>();
        _moveSpeed = moveSpeed;
        playerMovePoint.parent = null;
    }
     
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, _moveSpeed * Time.fixedDeltaTime);
    }
    public void SetPosition(float x, float y)
    {
        AkSoundEngine.PostEvent("Player_Move", this.gameObject);
        _moveSpeed = moveSpeed;
        playerMovePoint.position = new Vector3(x, y, playerMovePoint.position.z);
        PlayerDataHelper.UpdatePosition(playerMovePoint.position);
    }
    public void SetPositionNewKingdom(float x, float y, int sizeKingdom)
    {
        cam.ActiveLoadKingdom();
        float[] xy = CalculeNewPos(x, y, sizeKingdom);
        playerMovePoint.position = new Vector3(xy[0], xy[1], playerMovePoint.position.z);
        _moveSpeed = moveSpeed * 100;
    }
    float[] CalculeNewPos(float x, float y, int sizeKingdom)
    {
        float xn = -1;
        float yn = -1;

        if (x == 0)
        {
            xn = sizeKingdom - 2;
            yn = y;
        }
        if (x == (sizeKingdom - 1))
        {
            xn = 1;
            yn = y;
        }
        if (y == (sizeKingdom - 1))
        {
            xn = x;
            yn = 1;
        }
        if (y == 0)
        {
            xn = x;
            yn = sizeKingdom - 2;
        }

        return new float[] { xn, yn };
    }
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
