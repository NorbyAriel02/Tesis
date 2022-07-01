using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextCoints : MonoBehaviour
{
    public Text[] texts;
    private void OnEnable()
    {
        SmithyController.OnForja += UpdateTexts;
        MarketController.OnSell += UpdateTexts;
        MarketController.OnBuy += UpdateTexts;
    }
    private void OnDisable()
    {
        SmithyController.OnForja -= UpdateTexts;
        MarketController.OnSell -= UpdateTexts;
        MarketController.OnBuy -= UpdateTexts;
    }
    void UpdateTexts()
    {
        string coins = PlayerDataHelper.GetCoins();
        foreach (Text t in texts)
            t.text = coins;
    }
}
