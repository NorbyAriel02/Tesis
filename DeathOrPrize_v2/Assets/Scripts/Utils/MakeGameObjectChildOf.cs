using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGameObjectChildOf : MonoBehaviour
{
    public GameObject Child;
    public GameObject Father;
    public Vector3 position;
    void Start()
    {
        Child.transform.SetParent(Father.transform);
        Child.transform.position = position;
    }
}
