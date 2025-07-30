using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class dialogueVariable
{
    public string name;
    public dialogueVariable(string name)
    {
        this.name = name;
    }
}
public class dialogueState : MonoBehaviour
{
    public static dialogueState instance;
    public List<dialogueVariable> var = new List<dialogueVariable>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    public bool CheckVarUpdate(string a)
    {
        foreach (dialogueVariable variable in var)
        {
            if (variable.name == a)
            {
                return false;
            }
        }
        var.Add(new dialogueVariable(a));
        return true;
    }
    public bool CheckVar(string a)
    {
        foreach (dialogueVariable variable in var)
        {
            if (variable.name == a)
            {
                return false;
            }
        }
        return true;
    }
}
