using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverButton : MonoBehaviour
{
    public void MouseOver()
    {
        AkSoundEngine.PostEvent("UI_ButtonHover", this.gameObject);
    }
}
