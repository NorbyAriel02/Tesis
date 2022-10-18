using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    public Vector2 destination;

    void Update()
    {
        //Moves the GameObject from it's current position to destination over time
        transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);
    }
}
