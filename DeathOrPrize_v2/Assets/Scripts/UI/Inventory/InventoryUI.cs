using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public delegate void Open();
    public static Open OnOpen;
    public delegate void Close();
    public static Close OnClose;
    
    public KeyCode keyInventory;
    public GameObject inventory;
    public Animator animator;
    private void OnEnable()
    {
        HUDController.OnInventoryOpenOrClose += OpenOrClose;
    }
    private void OnDisable()
    {
        HUDController.OnInventoryOpenOrClose -= OpenOrClose;
    }

    void Start()
    {
        inventory.SetActive(false);
    }
        
    void Update()
    {
        if (Input.GetKeyDown(keyInventory) && !inventory.activeSelf)
            OpenInventory();
        else if (Input.GetKeyDown(keyInventory) && inventory.activeSelf)
            CloseInventory();
    }
    public void OpenOrClose()
    {
        if (inventory.activeSelf)
            CloseInventory();
        else
            OpenInventory();
    }
    public void CloseInventory()
    {        
        animator.SetBool("Close", true);
        OnClose?.Invoke();
    }
    public void OpenInventory()
    {        
        inventory.SetActive(true);
        animator.SetBool("Close", false);
        OnOpen?.Invoke();
    }
}
