using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuest : MonoBehaviour
{
    public string questVersion = "Version1";
    void Start()
    {
        QuestHelper.CreateFileQuest(questVersion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
