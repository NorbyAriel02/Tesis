using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSound : MonoBehaviour
{
    private void OnEnable()
    {
        IdleBattleManager.OnBattleStart += Battle;
        BossQuest.OnBattleStart += Battle;
    }
    private void OnDisable()
    {
        IdleBattleManager.OnBattleStart -= Battle;
        BossQuest.OnBattleStart -= Battle;
    }
    void Start()
    {
        
    }
    void Battle()
    {
        AkSoundEngine.PostEvent("Combat_Start", this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
