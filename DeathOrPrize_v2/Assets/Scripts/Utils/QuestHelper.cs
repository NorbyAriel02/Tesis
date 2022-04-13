using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHelper 
{
    public static void ShowQuest(GameObject panelQuest, List<QuestModel> quests)
    {

    }
    public static List<QuestModel> GetQuests(string idQuest)
    {
        DataFileController fileController = new DataFileController();
        List<QuestModel> quests = fileController.GetData<List<QuestModel>>(PathHelper.QuestDataFile(idQuest));
        
        return quests;
    }
    public static void Save(List<QuestModel> quests, string idQuest)
    {
        DataFileController fileController = new DataFileController();
        fileController.Save<List<QuestModel>>(quests, PathHelper.QuestDataFile(idQuest));
    }
}
