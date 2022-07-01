using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FilePathEnum 
{
    DialoguesTutorial,
    DialoguesGameplay
}

public class FilePath
{
    public static string Get(FilePathEnum file)
    {
        if (file == FilePathEnum.DialoguesTutorial)
            return PathHelper.DialogueTutorialDataFile;

        if (file == FilePathEnum.DialoguesGameplay)
            return PathHelper.DialogueDataFile;

        return string.Empty;
    }
}
