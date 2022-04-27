using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button btnFinTutorial;
    public Transform playerPos;
    public GameObject[] msjs;
    private void Awake()
    {
        PlayerDataHelper.Heal();
        PlayerDataHelper.SetTutorialPositionPlayer();
        InventoryHelper.StartInventoryAndEquipmentFile(PathHelper.InventoryDataFile, PathHelper.EquipmentDataFile);
    }
    void Start()
    {        
        btnFinTutorial.onClick.AddListener(LoadLevel);
    }
    void LoadLevel()
    {
        playerPos.position = PlayerDataHelper.GetStartPosition();
        SceneManager.LoadScene("Level");
    }
    
}
