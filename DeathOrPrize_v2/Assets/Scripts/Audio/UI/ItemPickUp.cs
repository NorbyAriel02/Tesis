using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnEnable()
    {
        Drop.OnPickupItem += PlaySound;
        Drop.OnCantPickupItem += CantGetUp;
    }
    private void OnDisable()
    {
        Drop.OnPickupItem -= PlaySound;
        Drop.OnCantPickupItem -= CantGetUp;
    }
    public void PlaySound(GameObject item)
    {
        AkSoundEngine.PostEvent("UI_Click", this.gameObject);
    }

    public void CantGetUp()
    {
        AkSoundEngine.PostEvent("Field_Error", this.gameObject);
    }
}
