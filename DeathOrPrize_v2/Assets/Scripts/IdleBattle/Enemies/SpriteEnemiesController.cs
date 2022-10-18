using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEnemiesController : MonoBehaviour
{
    public List<Sprite> sprites;
    public Sprite noneSprite;

    public Sprite GetSprite(int index)
    {
        if (sprites.Count <= index)
            return noneSprite;

        return sprites[index];
    }
    public void SetSprites(List<EnemyModel> enemies, GameObject[] gEnemies)
    {
        int count = 0;
        foreach(EnemyModel e in enemies)
        {
            gEnemies[count].GetComponent<SpriteRenderer>().sprite = GetSprite(e.type.id);
            count++;
        }
    }
}
