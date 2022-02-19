using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    public List<Sprite> sprites;
    public Sprite noneSprite;

    public Sprite GetSprite(int index)
    {
        if (sprites.Count <= index)
            return noneSprite;

        return sprites[index];
    }
}
