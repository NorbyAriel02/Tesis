using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerMove.OnPlayerMove += Move;
    }
    private void OnDisable()
    {
        PlayerMove.OnPlayerMove -= Move;
    }
    void Start()
    {
        
    }
    void Move(int dezplazamiento)
    {
        StartCoroutine(Walking(dezplazamiento));          
    }

    IEnumerator Walking(int dezplazamiento)
    {
        for (int x = 0; x < dezplazamiento; x++)
        {            
            AkSoundEngine.PostEvent("Player_Move", this.gameObject);            
            yield return new WaitForSeconds(0.7f);

        }
    }
    void Update()
    {
        
    }
}
