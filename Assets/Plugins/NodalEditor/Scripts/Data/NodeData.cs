using System;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class NodeData : ScriptableObject
{
    public int ID;
    public string Name;
    public Vector2 Position;
    public List<PortData> PortDatas;
    public List<int> ModulableInGroupPorts;
    public List<int> ModulableOutGroupPorts;
}
