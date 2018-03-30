using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
    //TODO: Add class name

    public int ID;
    public string Name;
    public Vector2 Position;
    public List<PortData> PortDatas;
    public List<int> ModulableInGroupPorts;
    public List<int> ModulableOutGroupPorts;
}
