using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityUI : MonoBehaviour
{
    public GameObject panelCity;
    public Animator animator;
    private void OnEnable()
    {
        CityController.OnEnterCity += EnterCity;
        CityController.OnExitCity += ExitCity;
    }
    private void OnDisable()
    {
        CityController.OnEnterCity -= EnterCity;
        CityController.OnExitCity -= ExitCity;
    }
    void Start()
    {
        panelCity.SetActive(false);
    }

    public void EnterCity()
    {
        panelCity.SetActive(true);
        animator.SetBool("OpenCity", true);        
    }
    public void ExitCity(float x, float y)
    {        
        animator.SetBool("OpenCity", false);        
    }
}
