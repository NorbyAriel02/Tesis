using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public class SpriteList 
{
    static Dictionary<string, string> dicSprites;

    public static Sprite GetSprite(string name, int index)
    {
        if (dicSprites == null)
            LoadSpritesFile();

        Sprite[] Sheet = Resources.LoadAll<Sprite>(dicSprites[name]);

        if(Sheet == null)
            return null;

        if (Sheet.Length <= index)
            return Sheet[0];

        return Sheet[index];        
    }
    static void LoadSpritesFile()
    {
        DataFileController df = new DataFileController();
        DataTable dt = df.GetData(PathHelper.SpriteFile);
        dicSprites = new Dictionary<string, string>();
        foreach (DataRow row in dt.Rows)
        {            
            dicSprites.Add(row[0].ToString(), row[1].ToString());
        }
    }
}
