using System;
using UnityEngine;

public enum PortTranslation
{
    In, Out
}

[Serializable]
public class PortData : ScriptableObject
{
    public string ClassName;
    public int ID;
    public PortTranslation Translation; // Keep it? 
    public int Group;

    public int ConnectedPortID;
}
