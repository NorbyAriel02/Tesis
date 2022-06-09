using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public delegate void RollDice(int valueDice);
    public static RollDice OnRollDice;
    public delegate bool ShiftAvailable();
    public static ShiftAvailable OnShiftAvailable;

    public int maxValueDice = 7;
    public int currentValueDice = 0;
    public Button btnDado;
    public Text textDice;
    public Texture2D[] cursos;

    private void OnEnable()
    {
        PlayerMove.OnPlayerMove += UpdateCurrentDiceValue;
        CityController.OnEnterCity += ResetCurrentDiceValue;
    }
    private void OnDisable()
    {
        PlayerMove.OnPlayerMove -= UpdateCurrentDiceValue;
        CityController.OnEnterCity -= ResetCurrentDiceValue;
    }
    void Start()
    {
        btnDado.onClick.AddListener(Roll);
    }
    void ResetCurrentDiceValue()
    {
        currentValueDice = 0;
        UpdateTextValue();
        SetInteractiveBtnDice();
    }
    void Roll()
    {        
        GetNewValueDeci();
        UpdateTextValue();
        SetInteractiveBtnDice();
        OnRollDice?.Invoke(currentValueDice);
    }
    void UpdateCurrentDiceValue(int value)
    {
        currentValueDice -= value;
        UpdateTextValue();
        SetInteractiveBtnDice();
    }
    void GetNewValueDeci()
    {
        AkSoundEngine.PostEvent("Throw_Dice", this.gameObject);
        currentValueDice = Random.Range(1, maxValueDice);
    }
    void SetInteractiveBtnDice()
    {
        if (currentValueDice > 0)
        {
            btnDado.interactable = false;
            Cursor.SetCursor(cursos[0], new Vector2(5, 5), CursorMode.Auto);
        }
        else
        {
            btnDado.interactable = true;
            Cursor.SetCursor(cursos[1], new Vector2(5, 5), CursorMode.Auto);
        }
        OnShiftAvailable?.Invoke();
    }
    void UpdateTextValue()
    {
        textDice.text = currentValueDice.ToString();
    }
}
