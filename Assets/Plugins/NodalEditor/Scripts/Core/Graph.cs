using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Graph : IDrawable, ISaveable, IRestorable, IInitializable
{
    private GraphData m_data;
    private List<Node> m_nodes = new List<Node>();

    public static Graph Create(Vector2 position, int id)
    {
        Graph graph = new Graph();
        graph.m_data = ScriptableObject.CreateInstance<GraphData>();
        graph.m_data.Nodes = new List<NodeData>();
        return graph;
    }

    public void Draw()
    {
        int size = m_nodes.Count;
        for (int idx = 0; idx < size; ++idx)
        {
            m_nodes[idx].Draw();
        }
    }

    public void Process()
    {
        ProcessNodeEvents(Event.current);
    }

    private void ProcessNodeEvents(Event e)
    {
        if (m_nodes != null)
        {
            for (int i = m_nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = m_nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    public void AddNode(Vector2 mousePosition)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }

        m_nodes.Add(Node.Create(typeof(Node), mousePosition, this));
    }

    public bool Restore(ScriptableObject scriptable)
    {
        if (scriptable is GraphData)
        {
            m_data = scriptable as GraphData;
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
        m_nodes.Clear();
    }
}
