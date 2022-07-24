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
        MarketSlot.OnSell += UpdateTexts;
        CitySlot.OnBuy += UpdateTexts;
        UpdateTexts();
    }
    private void OnDisable()
    {
        SmithyController.OnForja -= UpdateTexts;
        MarketSlot.OnSell -= UpdateTexts;
        CitySlot.OnBuy -= UpdateTexts;
    }
    void UpdateTexts()
    {
        string coins = DataHelper.GetCoins().ToString();
        foreach (Text t in texts)
            t.text = coins;
    }
}
