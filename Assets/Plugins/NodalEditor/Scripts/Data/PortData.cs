using System;
using UnityEngine;

public enum PortTranslation
{
    In, Out
}

[Serializable]
public class PortData : ScriptableObject
{
    public int ID;
    public string Name; 
    public int NodeID;
    public PortTranslation Translation;
    public int Group;
}
