using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipValues : MonoBehaviour
{
    public Text tittle;
    public Text damage;
    public Text defending;
    public Text speedAttack;
    public Text value;
    public Text level;
    void Start()
    {
        
    }
    public void Assign(ItemProperties item)
    {
        if(item.tItem == TypeItemInventory.Weapon)
        {
            tittle.text = "Weapon";
            damage.gameObject.SetActive(true);
            damage.text = "Damage " + item.damageBase.ToString();
            speedAttack.gameObject.SetActive(true);
            speedAttack.text = "Speed Attack " + item.attackSpeedEquipped.ToString();
            defending.gameObject.SetActive(false);
        }

        if (item.tItem == TypeItemInventory.Armor)
        {
            tittle.text = "Armor";
            damage.gameObject.SetActive(false);            
            speedAttack.gameObject.SetActive(false);            
            defending.gameObject.SetActive(true);
            defending.text = "Defending " + item.defending.ToString();
        }
        value.text = "Value " + item.value.ToString();
        level.text = "Level " + item.level.ToString();
    }
    
    void Update()
    {
        
    }
}
