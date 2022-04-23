using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToolTip : MonoBehaviour
{
    GameObject child;
    private void Start()
    {
        child = ChildrenController.GetChild(gameObject);
        Deactive();
    }
    public void Active()
    {
        child.SetActive(true);
    }
    public void Deactive()
    {
        child.SetActive(false);
    }
}
