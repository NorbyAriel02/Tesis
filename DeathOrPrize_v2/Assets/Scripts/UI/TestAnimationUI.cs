using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationUI : MonoBehaviour
{
    public GameObject PanelIzq;
    private Animator animator;
    void Start()
    {
        animator = PanelIzq.GetComponent<Animator>();
        PanelIzq.SetActive(false);        
    }
    public void Open()
    {
        PanelIzq.SetActive(true);
        animator.SetBool("Close", false);
    }
    public void Close()
    {        
        animator.SetBool("Close", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            if (!PanelIzq.activeSelf)
                Open();
            else
                Close();

    }
}
