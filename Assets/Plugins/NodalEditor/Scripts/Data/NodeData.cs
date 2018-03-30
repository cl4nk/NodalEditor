using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData : ScriptableObject
{
    public string ClassName; // To use reflection, keep class name

    public int ID;
    public Vector2 Position;
    public List<PortData> PortDatas = new List<PortData>();
}
