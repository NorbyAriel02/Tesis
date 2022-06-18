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
    }
    private void OnDisable()
    {
        CityController.OnExitCity -= Active;
        PlayerMove.OnPlayerArrive -= Desactive;
    }
    private void Start()
    {
        Desactive();
    }
    void Active(float x, float y)
    {
        panel.SetActive(true);
    }
    void Desactive()
    {
        panel.SetActive(false);
    }
}
