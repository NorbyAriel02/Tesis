using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("Game_Start", this.gameObject);
        AkSoundEngine.PostEvent("Play_Music", this.gameObject);
        AkSoundEngine.PostEvent("Play_Menu", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
