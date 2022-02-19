using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{
    public int maxValueDice = 6;
    public Button btnDado;
    public Text textDice;
    PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        btnDado.onClick.AddListener(GetNewValueDeci);
    }

    void GetNewValueDeci()
    {
        playerMove.diceValue = Random.Range(1, maxValueDice);
    }
    // Update is called once per frame
    void Update()
    {
        SetInteractiveBtnDice();

        UpdateTextValue();
    }
    void SetInteractiveBtnDice()
    {
        if (playerMove.diceValue > 0)
            btnDado.interactable = false;
        else
            btnDado.interactable = true;
    }

    void UpdateTextValue()
    {
        textDice.text = playerMove.diceValue.ToString();
    }
}
