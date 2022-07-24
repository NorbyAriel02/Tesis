using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public delegate void Move(int value);
    public static Move OnPlayerMove;
    public delegate void Arrive();
    public static Arrive OnPlayerArrive;


    public int diceValue = 0;
    public float moveSpeed = 5f;
    public Transform playerMovePoint;    
    private float _moveSpeed = 5f;
    private LineRenderer line;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    
    private void OnEnable()
    {
        DiceController.OnRollDice += SetDiceValue;
        CityController.OnEnterCity += ResetDiceValue;
        CityController.OnExitCity += ExitCity;
        ClickInCell.OnClickMe += SetPosition;
        ClickInCell.OnCursorOver += UpdateCellPosition;
        Cell.IsCellAction += HasMovements;
        LimitCell.OnSetPositionKingdom += SetPositionNewKingdom;
    }
    private void OnDisable()
    {
        DiceController.OnRollDice -= SetDiceValue;
        CityController.OnEnterCity -= ResetDiceValue;
        CityController.OnExitCity -= ExitCity;
        ClickInCell.OnClickMe -= SetPosition;
        ClickInCell.OnCursorOver -= UpdateCellPosition;
        Cell.IsCellAction -= HasMovements;
        LimitCell.OnSetPositionKingdom -= SetPositionNewKingdom;
    }

    void SetDiceValue(int value)
    {
        diceValue = value;
    }
    void ResetDiceValue()
    {
        diceValue = 0;
    }
    void Start()
    {        
        line = GetComponent<LineRenderer>();
        _moveSpeed = moveSpeed;
        playerMovePoint.parent = null;
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    bool HasMovements()
    {
        return diceValue > 0 ? true : false;
    }
    void FixedUpdate()
    {
        Going();
    }
    void Going()
    {
        
        //transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, _moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(Vector3.MoveTowards(rb.position, playerMovePoint.position, _moveSpeed * Time.fixedDeltaTime));
    }
    IEnumerator OnArrive()
    {
        while(transform.position != playerMovePoint.position)
        {            
            yield return null;
        }
        OnPlayerArrive?.Invoke();
        boxCollider.enabled = true;
    }
    void UpdateCellPosition(CellModel cell)
    {
        Vector3 CellPosition = new Vector3(cell.x, cell.y, transform.position.z);
        
        if (Vector3.Distance(transform.position, CellPosition) < (diceValue + 0.5f))
        {
            line.SetPosition(1, CellPosition);
        }
        else
        {
            line.SetPosition(1, transform.position);
        }

        line.SetPosition(0, transform.position);
    }
    
    void SetPosition(CellModel cell)
    {
        Vector3 cellPos = new Vector3(cell.x, cell.y, playerMovePoint.position.z);
        if (Vector3.Distance(transform.position, cellPos) < (diceValue + 0.5f))
        {
            int desplazamiento = Mathf.RoundToInt(Vector3.Distance(transform.position, cellPos));
            diceValue -= desplazamiento;
            
            _moveSpeed = moveSpeed;
            playerMovePoint.position = cellPos;
            PlayerDataHelper.UpdatePosition(playerMovePoint.position);
            OnPlayerMove?.Invoke(desplazamiento);
            StartCoroutine(OnArrive());
        }            
    }
    private void ExitCity(float x, float y)
    {
        _moveSpeed = moveSpeed;
        playerMovePoint.position = new Vector3(x, y, playerMovePoint.position.z);
        PlayerDataHelper.UpdatePosition(playerMovePoint.position);
        StartCoroutine(OnArrive());
    }
    public void SetPositionNewKingdom(float x, float y, int sizeKingdom)
    {
        boxCollider.enabled = false;
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
}
