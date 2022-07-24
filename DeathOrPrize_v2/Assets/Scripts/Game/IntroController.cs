using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public float timer;
    void Start()
    {
       
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SceneHelper.Load("Menu");
        }
    }
}
