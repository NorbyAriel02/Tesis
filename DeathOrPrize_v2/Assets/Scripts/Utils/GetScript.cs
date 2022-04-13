using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class GetScript {
    public static T Type<T>(string tag, string callFrom = "no especifica") where T : class, new()
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);

        if (go != null)
            return go.GetComponent<T>();
        else
            Logger.WriteLog("No se halla el objeto de tag " + tag + " desde el script " + callFrom);

        return null;
    }
}
