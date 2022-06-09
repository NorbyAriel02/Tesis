using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SebaLista : MonoBehaviour
{
    public Text juego;
    public Text Ingresado;
    public List<string> jg;
    public List<string> ingre;
    void Start()
    {
        juego.text = "";
        int count = 0;
        foreach(string c in ingre)
        {
            if (count >= jg.Count)
                break;

            if(c == jg[count])
            {
                juego.text += c.ToUpper();
            }
            else
                juego.text += "_";

            count++;
        }
        string jue = "";
        foreach (string c in jg)
        {
            jue += c;
        }
        
        foreach (string c in jg)
        {
            for(int x=0; x < ingre.Count; x++)
            {
                if(ingre[x] == c)
                    juego.text += c;                
            }

            count++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
