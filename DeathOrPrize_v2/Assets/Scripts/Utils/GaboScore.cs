using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaboScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Scorcito", 1000);
        SetNewScore();
        List<string> T = GetTabla();
        foreach (string x in T)
            Debug.Log(x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNewScore()
    {
        float score = PlayerPrefs.GetInt("Scorcito");
        string tabla = "";
        if (PlayerPrefs.HasKey("Tabla"))
        {
            tabla = PlayerPrefs.GetString("Tabla");
            string[] vTabla = tabla.Split('|');
            
            for(int x = 0; x < vTabla.Length; x++)
            {
                float value = System.Convert.ToInt64(vTabla[x]);
                if(value < score)
                {
                    vTabla[x] = score.ToString();
                    score = value;
                }
            }
            tabla = "";
            for (int x = 0; x < vTabla.Length; x++)
            {
                tabla += vTabla[x] + '|';
            }

            tabla = tabla.Remove(tabla.Length - 1);
        }
        else
        {
            tabla = score + "|0|0|0|0|0|0|0|0|0";            
        }
        PlayerPrefs.SetString("Tabla", tabla);
    }    

    List<string> GetTabla()
    {
        List<string> lTabla = new List<string>();
        string tabla = PlayerPrefs.GetString("Tabla");
        string[] vTabla = tabla.Split('|');
        
        foreach (string value in vTabla)
            lTabla.Add(value);

        return lTabla;
    }
}
