using System;
using System.Collections;
using System.Collections.Generic;

public class MyAppConfig
{
    #region PlayerPref
    //public static string CurrentLevel { get { return "CURRENTLEVEL"; } }
    //public static string MaxLevelUnlocked { get { return "MAXLEVELUNLOCKED"; } }
    //public static string JoystickMonoCommand { get { return "JoystickMonoCommand"; } }
    //public static string AudioEffectVolume { get { return "EffectVolume"; } }
    public static string AudioMusictVolume { get { return "MusicVolume"; } }
    //public static string SendUserFeedback { get { return "SendUserFeedback"; } }
    #endregion

    #region SCENES    
    public static string Stage { get { return "Level"; } }
    public static string Menu { get { return "Menu"; } }
    public static string LevelSelector { get { return "LevelsSelector"; } }
    public static string Controllers { get { return "Controller"; } }
    #endregion

    #region Files
    
    public static string WorldDataFile { get { return "ADR{0}.qti"; } }
    public static string BiomesDataFile { get { return "typesOfBiomes.qti"; } }
    public static string InventoryDataFile { get { return "Inventory.qti"; } }
    public static string EquipmentDataFile { get { return "Equipment.qti"; } }
    public static string NeighborDataFile { get { return "ADN.qti"; } }
    public static string CellTypesDataFile { get { return "typesOfCells.qti"; } }
    public static string CellSubTypesDataFile { get { return "subTypesOfCells.qti"; } }
    

    #endregion

    #region Value Config
    public static int SizeGridX { get { return 165; } }
    public static int SizeGridY { get { return 165; } }
    #endregion
}
