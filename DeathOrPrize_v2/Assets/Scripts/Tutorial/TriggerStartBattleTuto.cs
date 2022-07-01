using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartBattleTuto : MonoBehaviour
{
    public Tutorial tutorial;
    public IdleBattleManager idleBattle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("FacePlayer"))
        {
            idleBattle.StartBattle(625);
            tutorial.StartBattle();
            gameObject.SetActive(false);
        }
    }
}
