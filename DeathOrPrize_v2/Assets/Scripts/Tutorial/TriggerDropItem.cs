using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDropItem : MonoBehaviour
{
    public Tutorial tutorial;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("FacePlayer"))
        {
            tutorial.ActivateDropItem();
            gameObject.SetActive(false);
        }
    }
}
