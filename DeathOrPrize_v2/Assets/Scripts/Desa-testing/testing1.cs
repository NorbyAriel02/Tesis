using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing1 : MonoBehaviour
{
    public Button btnTest;
    public Image imgAttack;
    public Text mensaje;
    public Text mensaje2;
    public float timer;
    public float attackSpeed = 5;
    public float speed = 5;
    public float oneAttack = 0;
    private int ataques = 0;
    
    void Start()
    {
        btnTest.onClick.AddListener(test);
    }
    void test()
    {
        speed = 1 / attackSpeed;
        mensaje.text = "Ataque ";
        timer = 0;
        ataques = 0;
    }

    private void Update()
    {
        if(CanAttack(ref oneAttack, speed))
        {
            ataques++;
            mensaje.text = "Ataque " + ataques;
        }
    }

    bool CanAttack(ref float oneAttack, float speed)
    {
        if (PassASeg())
        {
            oneAttack += speed;
        }            

        if(oneAttack >= 1)
        {
            oneAttack = 0;            
            return true;
        }
        return false;         
    }
    bool PassASeg()
    {
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            timer = 0;
            return true;
        }
        return false;
    }
}
