using System;
using UnityEngine;

public enum PortTranslation
{
    In, Out
}

[Serializable]
public class PortData
{
    //TODO: Add class name
    public int ID;
    public string Name; 
    public int NodeID;
    public PortTranslation Translation;
    public int Group;

    public int ConnectedPortID;
}
