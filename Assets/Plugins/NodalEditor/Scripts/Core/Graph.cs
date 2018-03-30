using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Graph : IDrawable, ISaveable, IRestorable, IInitializable
{
    public GraphData data;

    private List<Node> Nodes = new List<Node>();

    public void Draw()
    {
        foreach (Node node in Nodes)
        {
            node.Draw();
        }
    }

    public bool Restore(ScriptableObject scriptable)
    {
        if (scriptable is GraphData)
        {
            data = scriptable as GraphData;
            Init();
            return true;
        }

        return false;
    }

    public bool Save(string path)
    {
        throw new System.NotImplementedException();
    }

    public void Init()
    {
        Nodes.Clear();



    }
}
