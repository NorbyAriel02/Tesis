using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public delegate void EventNext();
    public static EventNext OnNextAction;

    public GameObject content;
    public Button btnNext;
    public Button btnDisableDialogue;
    Dialogue dialogue;
    private bool disable = false;
    private void Start()
    {
        content.SetActive(false);
        dialogue = GetComponent<Dialogue>();
        btnNext.onClick.AddListener(Next);
        btnDisableDialogue.onClick.AddListener(DisableDialogue);
    }
    void DisableDialogue()
    {
        disable = true;
        DisablePanel();
    }
    private void OnEnable()
    {
        Dialogue.OnEndDialogue += DisablePanel;
        Dialogue.OnStartDialogue += EnablePanel;
    }
    private void OnDisable()
    {
        Dialogue.OnEndDialogue -= DisablePanel;
        Dialogue.OnStartDialogue -= EnablePanel;
    }
    void Next()
    {
        OnNextAction?.Invoke();
    }
    void DisablePanel()
    {
        content.SetActive(false);
    }
    void EnablePanel()
    {
        if (disable)
            return;
        content.SetActive(true);
    }
}
