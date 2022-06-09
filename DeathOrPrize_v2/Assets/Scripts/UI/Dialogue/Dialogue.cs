using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Data;


public class Dialogue : MonoBehaviour
{
    public Button btnNext;
    public Button btnDesactivar;
    public float typingTime = 0.05f;
    public Animator animator;
    public bool disableDialogue = false;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    private List<string> dialogueLines;
    private Dictionary<string, List<string>> dialogues;
    private DataFileController df;
    private int lineIndex;
    void Start()
    {
        
        GetDialogue();
        btnDesactivar.onClick.AddListener(Desactivar);
        btnNext.onClick.AddListener(Next);
        DesactivePanel();
    }
    void Desactivar()
    {
        disableDialogue = true;
        animator.SetBool("Close", true);
    }
    void GetDialogue()
    {
        df = new DataFileController();
        dialogues = new Dictionary<string, List<string>>();
        DataTable dt = df.GetData(PathHelper.DialogueDataFile);
        foreach(DataRow row in dt.Rows)
        {
            if(dialogues.ContainsKey(row[0].ToString()))
            {
                dialogues[row[0].ToString()].Add(row[2].ToString());
            }
            else
            {
                List<string> dialogueLines = new List<string>();
                dialogueLines.Add(row[2].ToString());
                dialogues.Add(row[0].ToString(), dialogueLines);
            }                
        }
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

    public void StartDialogue(int rollDice)
    {
        if (disableDialogue)
            return;

        dialoguePanel.SetActive(true);
        animator.SetBool("Close", false);
        lineIndex = 0;
        AlgoQueSeteaElDialogoActual(rollDice);
        StartCoroutine(ShowLine());
    }
    void AlgoQueSeteaElDialogoActual(int rollDice)
    {
        dialogueLines = new List<string>();
        foreach (string line in dialogues[rollDice.ToString()])
        {
            dialogueLines.Add(line);
        }
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Count)
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
