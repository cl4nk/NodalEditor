using System;

public enum PortTranslation
{
    In, Out
}

[Serializable]
public class PortData
{
    public string ClassName;
    public int ID;
    public string Name; 
    public int NodeID;
    public PortTranslation Translation;
    public int Group;

    public int ConnectedPortID;
}
