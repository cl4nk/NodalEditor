using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Graph : IDrawable, ISaveable, IRestorable
{
    public GraphData data;

    private List<Node> Nodes = new List<Node>();

    public void Draw()
    {
        foreach (Node node in Nodes)
        {
        }
    }

    public bool Restore(ScriptableObject scriptable)
    {
        throw new System.NotImplementedException();
    }

    public bool Save(string path)
    {
        throw new System.NotImplementedException();
    }
}
