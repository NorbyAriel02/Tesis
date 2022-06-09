using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenController {
    /// <summary>Elimina un hijo (gameObject) con una referencia especifica del mismo, 
    /// para guardar una referencia especifica del objeto puede utilizar 
    /// un diccionario con una clave que le convenga
    /// <para>Gameobject father es el objeto padre</para>
    /// <para>Gameobject child es la referencia especifica del hijo</para>    
    /// </summary>
    public static void RemoveChild(GameObject father, GameObject child)
    {
        GameObject[] allChildren = GetChildren(father);

        //Now destroy them
        foreach (GameObject _child in allChildren)
        {
            if(GameObject.ReferenceEquals(_child, child))
                Object.DestroyImmediate(_child.gameObject);                
        }
    }

    public static void RemoveAllChildren(GameObject father)
    {
        GameObject[] allChildren = GetChildren(father);

        //Now destroy them
        foreach (GameObject _child in allChildren)
        {
            Object.Destroy(_child.gameObject);
        }
    }

    public static GameObject[] GetChildren(GameObject father)
    {
        int i = 0;
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[father.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform _child in father.transform)
        {
            allChildren[i] = _child.gameObject;
            i += 1;
        }

        return allChildren;
    }

    public static List<GameObject> GetListChildren(GameObject father)
    {     
        List<GameObject> allChildren = new List<GameObject>();
             
        foreach (Transform _child in father.transform)
        {
            allChildren.Add(_child.gameObject);            
        }

        return allChildren;
    }

    public static GameObject GetChild(GameObject father)
    {
        foreach (Transform _child in father.transform)
        {
            return _child.gameObject;
        }

        return null;
    }

    public static bool IsChild(GameObject father, GameObject child)
    {
        GameObject[] allChildren = GetChildren(father);

        foreach (GameObject _child in allChildren)
        {
            if (GameObject.ReferenceEquals(_child, child))
                return true;
        }

        return false;
    }

    public static GameObject GetChildWithTag(GameObject father, string tag)
    {
        GameObject[] allChildren = GetChildren(father);

        foreach (GameObject _child in allChildren)
        {
            if (_child.tag.Equals(tag))
                return _child;
        }

        return null;
    }

}
