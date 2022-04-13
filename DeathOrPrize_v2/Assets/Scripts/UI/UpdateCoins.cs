using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoins : MonoBehaviour
{
    public Text textCoinHUB;
    public Text textCoinCity;
    private void Start()
    {
        textCoinHUB.text = PlayerDataHelper.GetCoins();
    }
    void Update()
    {
        textCoinCity.text = textCoinHUB.text;
    }
}
