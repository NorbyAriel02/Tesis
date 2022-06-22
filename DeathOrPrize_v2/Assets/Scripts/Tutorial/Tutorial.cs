using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public delegate void StartDialogue(int indexDialogue);
    public static StartDialogue OnStartDialogue;
    public delegate void Next();
    public static Next OnNextAction;
    
    public Button btnSiguiente;
    public Button btnFinTutorial;
    public int StepTutorial = 0;
    public GameObject panel;
    private void OnEnable()
    {
        Dialogue.OnEndDialogue += DisablePanel;
        
    }
    private void OnDisable()
    {
        Dialogue.OnEndDialogue -= DisablePanel;
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        btnSiguiente.onClick.AddListener(NextDialogue);
        btnFinTutorial.onClick.AddListener(LoadLevel);
        StartDialogueTutorial(StepTutorial);
        StepTutorial++;
    }
    void DisablePanel()
    {
        panel.SetActive(false);
    }
    void NextDialogue()
    {
        OnNextAction?.Invoke();
    }
    void StartDialogueTutorial(int index)
    {
        OnStartDialogue?.Invoke(index);
    }
    void LoadLevel()
    {        
        SceneManager.LoadScene("Level");
    }
    
}
