using System;
using System.Collections.Generic;

public enum QuestStatus { esperando, activa, completa };

[Serializable]
public class QuestModel 
{
    public int idQuest = -1;
    public string tittle;
    public List<string> initialMessage;
    public List<string> middleMessage;
    public List<string> finalMessage;
    public QuestStatus status;    
    public int idkingdom;
}
