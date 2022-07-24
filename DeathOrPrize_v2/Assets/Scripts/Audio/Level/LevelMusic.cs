using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    private void OnEnable()
    {
        IdleBattleManager.OnBattleStart += Battle;
        BossQuest.OnBattleStart += Battle;
        IdleBattleManager.OnBattleEnd += GamePlay;
        BossQuest.OnBattleEnd += GamePlay;
        CityController.OnEnterCity += City;
        CityController.OnExitCity += GamePlay;
        MenuPausa.OnMenuOpen += MenuOn;
        MenuPausa.OnMenuClose += MenuOff;
    }
    private void OnDisable()
    {
        IdleBattleManager.OnBattleStart -= Battle;
        IdleBattleManager.OnBattleEnd -= GamePlay;
        CityController.OnEnterCity -= City;
        CityController.OnExitCity -= GamePlay;
        MenuPausa.OnMenuOpen -= MenuOn;
        MenuPausa.OnMenuClose -= MenuOff;
    }
    void Start()
    {
        GamePlay();
    }
    void City()
    {
        AkSoundEngine.PostEvent("Play_City", this.gameObject);
        AkSoundEngine.PostEvent("Amb_City", this.gameObject);
    }
    void Battle()
    {
        if(this.gameObject != null)
            AkSoundEngine.PostEvent("Combat_Start", this.gameObject);
    }
    void GamePlay(float x = 0f, float y = 0f)
    {
        GamePlay();
    }
    void GamePlay()
    {
        AkSoundEngine.PostEvent("Play_Gameplay", this.gameObject);
        AkSoundEngine.PostEvent("Amb_Forest", this.gameObject);
    }
    void MenuOn()
    {
        AkSoundEngine.PostEvent("Pausa_On", this.gameObject);
    }
    void MenuOff()
    {
        AkSoundEngine.PostEvent("Pausa_Off", this.gameObject);
    }
}
