using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper 
{
    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static string GetNameSceneActive()
    {
        return SceneManager.GetActiveScene().name;
    }
}
