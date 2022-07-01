using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Dialogue : MonoBehaviour
{
    public delegate void EndDialogue();
    public static EndDialogue OnEndDialogue;
    public delegate void EventStartDialogue();
    public static EventStartDialogue OnStartDialogue;
    public FilePathEnum filePath;
    public float typingTime = 0.05f;    
    public bool disableDialogue = false;    
    [SerializeField] private Text dialogueText;
    private List<string> dialogueLines;
    private Dictionary<string, List<string>> dialogues;
    private DataFileController df;
    private int lineIndex;
    private void OnEnable()
    {
        Tutorial.OnNextAction += Next;
        Tutorial.OnStartDialogue += StartDialogue;
        DialogueManager.OnNextAction += Next;
    }
    private void OnDisable()
    {
        Tutorial.OnNextAction -= Next;
        Tutorial.OnStartDialogue -= StartDialogue;
        DialogueManager.OnNextAction -= Next;
    }
    private void Awake()
    {
        LoadDialogue();
    }
    void Start()
    {        
        
    }
    void Desactivar()
    {
        disableDialogue = true;        
    }
    void LoadDialogue()
    {
        try
        {
            
            df = new DataFileController();
            dialogues = new Dictionary<string, List<string>>();
            DataTable dt = df.GetData(FilePath.Get(filePath));
            foreach (DataRow row in dt.Rows)
            {
                if (dialogues.ContainsKey(row[0].ToString()))
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
        catch(Exception ex)
        {
            Logger.WriteLog(ex.Message);
        }
    }
    
    void Next()
    {
        if (lineIndex >= dialogueLines.Count)
            return;

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

    public void StartDialogue(int indexDialogue)
    {
        if (disableDialogue)
            return;

        lineIndex = 0;
        SetCurrentDialogue(indexDialogue);
        StartCoroutine(ShowLine());
        OnStartDialogue?.Invoke();
    }
    void SetCurrentDialogue(int indexDialogue)
    {
        dialogueLines = new List<string>();
        foreach (string line in dialogues[indexDialogue.ToString()])
        {
            dialogueLines.Add(line);
        }
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Count)
        {
            StartCoroutine(ShowLine());
        }
        else
            OnEndDialogue?.Invoke();
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
