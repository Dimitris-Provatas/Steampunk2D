using System;
using System.IO;
using System.Collections;
using UnityEngine;
using LitJson;

public class JSONReader : MonoBehaviour
{
    public string path;
    [TextArea(1, 10)]
    public string jsonString;
    public JsonData data;

    void Start()
    {
        path = Application.dataPath + "/Dialogue/test.json";
        jsonString = File.ReadAllText(path);
        data = JsonMapper.ToObject(jsonString);
    }

    public string[] GetDialogue(int id)
    {
        string[] s = new string[data["dialogue"][id]["sentence"].Count];

        for(int i=0; i < data["dialogue"][id]["sentence"].Count; i++)
        {
            s[i] = data["dialogue"][id]["sentence"][i].ToString();
        }

        return s;
    }
}