using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Graph : ScriptableObject, IDrawable, ISaveable, IInitializable, IRestorable<GraphData>
{
    private List<Node> m_nodes = new List<Node>();

    public GraphData Data { get; private set; }

    // DataType must hinerit from @GraphData
    public virtual Type DataType
    {
        get { return typeof(GraphData); }
    }

    // type must hinerit from @Graph
    public static Graph Create(Type type)
    {
        //TODO: throw exception
        if (type.IsAssignableFrom(typeof(Graph)))
        {
            Graph graph = ScriptableObject.CreateInstance(type) as Graph;
            graph.Data = ScriptableObject.CreateInstance(graph.DataType) as GraphData;
            graph.Data.ClassName = type.FullName;
            return graph;
        }

        return null;
    }

    public static Graph Restore(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Fill(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Draw()
    {
        int size = m_nodes.Count;
        foreach (Node node in m_nodes)
        {
            node.Draw();
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
            foreach (Node node in m_nodes)
            {
                bool guiChanged = node.ProcessEvents(e);

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

        Node createdNode = Node.Create(typeof(Node));
        createdNode.Init(mousePosition, this);
        m_nodes.Add(createdNode);
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
