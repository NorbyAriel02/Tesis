using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{
    public Button btnNext;
    public Button btnDesactivar;
    public float typingTime = 0.05f;
    public Animator animator;
    public bool disableDialogue = false;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
        
    private int lineIndex;
    void Start()
    {
        btnDesactivar.onClick.AddListener(Desactivar);
        btnNext.onClick.AddListener(Next);
        DesactivePanel();
    }
    void Desactivar()
    {
        disableDialogue = true;
        animator.SetBool("Close", true);
    }
    void DesactivePanel()
    {
        dialoguePanel.gameObject.SetActive(false);        
    }
    void Next()
    {
        if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[lineIndex];
        }        
    }

    public void StartDialogue()
    {
        if (disableDialogue)
            return;

        dialoguePanel.SetActive(true);
        animator.SetBool("Close", false);
        lineIndex = 0;        
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            animator.SetBool("Close", true);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char letter in dialogueLines[lineIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    
}
