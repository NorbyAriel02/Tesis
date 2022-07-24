using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class QuestHelper 
{
    public static void CreateFileQuest(string version)
    {
        DataFileController file = new DataFileController();
        DataTable dt = file.GetData(PathHelper.CSVQuestDataFile);
        List<QuestModel> quests = new List<QuestModel>();
        foreach(DataRow row in dt.Rows)
        {
            QuestModel quest = new QuestModel();
            
            if (row[0].ToString().ToUpper().Contains("TITTLE"))
                continue;

            quest.tittle = (string)row[0];
            quest.initialMessage = GetListMessage((string)row[1]);
            quest.middleMessage = GetListMessage((string)row[2]);
            quest.finalMessage = GetListMessage((string)row[3]);
            quest.idkingdom = System.Convert.ToInt32(row[4].ToString());
            quest.idQuest = System.Convert.ToInt32(row[5].ToString());
            quest.status = QuestStatus.esperando;
            quests.Add(quest);
        }
        Save(quests, version);
    }
    static List<string> GetListMessage(string value)
    {
        List<string> lMessage = new List<string>();
        string[] vMessage = value.Split('#');
        for(int index = 0; index < vMessage.Length; index++)
        {
            lMessage.Add(vMessage[index]);
        }
        return lMessage;
    }
    public static void ShowQuest(GameObject panelQuest, List<QuestModel> quests)
    {

    }
    public static List<QuestModel> GetQuests(string idQuest)
    {
        DataFileController fileController = new DataFileController();
        List<QuestModel> quests = fileController.GetEncryptedData<List<QuestModel>>(PathHelper.QuestsDataFile(idQuest));
        
        return quests;
    }
    public static RewardModel GetRewardQuests(string idRewards, int idKingdom)
    {
        DataFileController fileController = new DataFileController();
        List<RewardModel> rewards = fileController.GetEncryptedData<List<RewardModel>>(PathHelper.RewardsDataFile(idRewards));

        return rewards[idKingdom];
    }
    public static void CompletedBossQuests(string idQuest, int boss)
    {
        DataFileController fileController = new DataFileController();
        List<QuestModel> quests = fileController.GetEncryptedData<List<QuestModel>>(PathHelper.QuestsDataFile(idQuest));
        
        if(quests != null && quests.Count > boss)
            quests[boss].status = QuestStatus.completa;
        
        fileController.SaveEncrypted<List<QuestModel>>(quests, PathHelper.QuestsDataFile(idQuest));
    }
    public static void Save(List<QuestModel> quests, string idQuest)
    {
        DataFileController fileController = new DataFileController();
        fileController.SaveEncrypted<List<QuestModel>>(quests, PathHelper.QuestsDataFile(idQuest));
    }
}
