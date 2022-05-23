using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMe : MonoBehaviour
{
    public float distance = 1.5f;
    private Cell cell;
    private GameObject border;
    private PlayerMove playerMove;
    void Start()
    {
        cell = GetComponent<Cell>();
        border = ChildrenController.GetChildWithTag(gameObject, "Border");
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsIntoDistance && playerMove.diceValue > 0)
                cell.ActionCell();
            else
                ErrorMessage();
        }

        if (border != null && IsIntoDistance)
            border.SetActive(true);
    }
    void OnMouseExit()
    {
        if (border != null)
            border.SetActive(false);
    }

    bool IsIntoDistance
    {
        get
        {
            if (Vector3.Distance(playerMove.GetCurrentPosition(), transform.position) < distance)
                return true;

            return false;
        }
    }

    void ErrorMessage()
    {
        Debug.Log("No es posible");
    }
}
