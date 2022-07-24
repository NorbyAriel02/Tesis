using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject panel;
    private void OnEnable()
    {
        CityController.OnExitCity += Active;
        PlayerMove.OnPlayerArrive += Desactive;
        LimitCell.OnCellAction += Active;
    }
    private void OnDisable()
    {
        CityController.OnExitCity -= Active;
        PlayerMove.OnPlayerArrive -= Desactive;
        LimitCell.OnCellAction -= Active;
    }
    private void Start()
    {
        Desactive();
    }
    void Active(float x, float y)
    {
        panel.SetActive(true);
    }
    void Active(int idCell)
    {
        panel.SetActive(true);
    }
    void Desactive()
    {
        panel.SetActive(false);
    }
}
