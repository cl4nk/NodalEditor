using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
    public string ClassName; // To use reflection, keep class name

    public int ID;
    public string Name; // TODO: move it into node
    public Vector2 Position;
    public List<PortData> PortDatas = new List<PortData>();
    public List<int> ModulableGroupPorts = new List<int>(); // TODO: move it into node
}
