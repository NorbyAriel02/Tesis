using UnityEngine.EventSystems;
using UnityEngine;

public class ClickInCell : MonoBehaviour
{
    public delegate void Click(CellModel cell);
    public static Click OnClickMe;
    public delegate void CursorOver(CellModel cell);
    public static CursorOver OnCursorOver;

    private Cell cell;
    private GameObject border;        

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        cell = GetComponent<Cell>();
        border = ChildrenController.GetChildWithTag(gameObject, "Border");        
    }
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsMouseOverUI())
                return;

            cell.ClickMe = true;
            OnClickMe?.Invoke(cell.cellData);
        }

        if (border != null)
            border.SetActive(true);

        OnCursorOver?.Invoke(cell.cellData);
    }
    void OnMouseExit()
    {
        if (border != null)
            border.SetActive(false);
    }
    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
