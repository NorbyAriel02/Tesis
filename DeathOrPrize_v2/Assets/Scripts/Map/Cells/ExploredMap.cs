using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploredMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            ClearFog();
        
    }

    void ClearFog()
    {
        Cell cell = GetComponentInParent<Cell>();
        cell.cellData.Fog = false;
        gameObject.SetActive(false);
    }
}
