using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateOnClickOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsDesactiveOnClick();
    }

    public void IsDesactiveOnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == gameObject.name)
            {
                gameObject.SetActive(false);                
            }
        }
    }
}
