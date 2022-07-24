using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject PlayerInventory;
    public GameObject PlayerInventoryCity;
    private void OnEnable()
    {
        CityController.OnEnterCity += ChangeInventory;
        CityController.OnExitCity += ChangeInventory;
    }
    private void OnDisable()
    {
        CityController.OnEnterCity -= ChangeInventory;
        CityController.OnExitCity -= ChangeInventory;
    }
    private void Start()
    {
        if (PlayerInventory.activeSelf)
            PlayerInventoryCity.SetActive(false);
        else
            PlayerInventoryCity.SetActive(true);
    }
    void ChangeInventory()
    {
        PlayerInventory.SetActive(!PlayerInventory.activeSelf);
        PlayerInventoryCity.SetActive(!PlayerInventoryCity.activeSelf);
    }
    void ChangeInventory(float x, float y)
    {
        PlayerInventory.SetActive(!PlayerInventory.activeSelf);
        PlayerInventoryCity.SetActive(!PlayerInventoryCity.activeSelf);
    }
}
