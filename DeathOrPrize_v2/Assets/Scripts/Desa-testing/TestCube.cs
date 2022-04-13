using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCube : MonoBehaviour
{
    public Text norte;
    public Text sur;
    public Text este;
    public Text oeste;
    public Text idt;

    void Start()
    {
        
    }

    public void SetText(string n, string s, string e, string o, string id)
    {
        norte.text = n;
        sur.text = s;
        este.text = e;
        oeste.text = o;
        idt.text = id;
    }
}
