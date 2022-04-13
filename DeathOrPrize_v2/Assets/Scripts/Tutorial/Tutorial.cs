using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button btnFinTutorial;
    public Transform playerPos;
    private void Awake()
    {
        PlayerDataHelper.UpdatePosition(new Vector3(0, 0, 0));
        PlayerDataHelper.UpdateIdKingdom(0);
        InventoryHelper.StartInventoryAndEquipmentFile(PathHelper.InventoryDataFile, PathHelper.EquipmentDataFile);
    }
    void Start()
    {        
        btnFinTutorial.onClick.AddListener(LoadLevel);
    }
    void LoadLevel()
    {
        playerPos.position = new Vector3(26, 17, 0);
        SceneManager.LoadScene("Level");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
