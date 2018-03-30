using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GraphData : ScriptableObject
{
    public string ClassName; // To use reflection, keep class name

    public List<NodeData> Nodes = new List<NodeData>();
}
