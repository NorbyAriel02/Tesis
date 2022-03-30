using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoins : MonoBehaviour
{
    public Text textCoinHUB;
    public Text textCoinCity;
    void Update()
    {
        textCoinCity.text = textCoinHUB.text;
    }
}
