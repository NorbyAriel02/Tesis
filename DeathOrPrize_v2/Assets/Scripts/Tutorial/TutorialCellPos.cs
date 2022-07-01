using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCellPos : MonoBehaviour
{
    Cell cell;
    void Start()
    {
        cell = GetComponent<Cell>();
        cell.cellData.x = transform.position.x;
        cell.cellData.y = transform.position.y;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
