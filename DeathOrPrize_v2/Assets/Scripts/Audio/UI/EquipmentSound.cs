using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSound : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            //if (i.properties.typeItem == TypeItemInventory.Armor || i.properties.typeItem == TypeItemInventory.Weapon)
            //    AkSoundEngine.PostEvent("Item_Equip", this.gameObject);

        }
    }
}
