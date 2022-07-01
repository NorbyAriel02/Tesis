using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDayCicleSign : MonoBehaviour
{
    public Tutorial tutorial;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("FacePlayer"))
        {
            tutorial.ActivateSignDay();
            gameObject.SetActive(false);
        }
            
    }
}
