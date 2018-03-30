using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Interfaces;

//TODO: abstract it when finished
public class Node : ScriptableObject, IDrawable, INameable, IColorable
{
    private List<int> ModulableGroupPorts = new List<int>();
    private Rect m_rect = new Rect();
    private bool m_isDragged = false;
    private GUIStyle m_style = new GUIStyle();
    private NodeData m_data = new NodeData();

    //TODO: remove it when the class is finished and abstract
    public virtual string Title
    {
        get
        {
            return "Node";
        }
    }

    public virtual Color CustomColor
    {
        get
        {
            return Color.blue;
        }
    }

    private static int GlobalID = 0;

    public static Node Create(Type type, Vector2 position, Graph ownerGraph)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.m_data.ClassName = type.FullName;
        node.Init(position, ownerGraph);
        return node;
    }

    public static Node Create(string ClassName, Vector2 position, Graph ownerGraph)
    {
        NodeTypeData data = NodeTypes.getNodeData(ClassName);
        return Create(data.type, position, ownerGraph);
    }

    public void Init(Vector2 position, Graph ownerGraph)
    {
        m_data.ID = GlobalID++;
        m_data.Position = position;
        m_data.PortDatas = new List<PortData>();

        m_rect = new Rect(position.x, position.y, 100, 100);
        m_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_style.border = new RectOffset(12, 12, 12, 12);
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public void Draw() 
    {
        GUI.Box(m_rect, "", m_style);
        GUI.BeginGroup(m_rect);
        GUILayout.Label(Title);
        GUI.EndGroup();
    }


    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (m_rect.Contains(e.mousePosition))
                    {
                        m_isDragged = true;
                        GUI.changed = true;
                    }
                    else
                    {
                        GUI.changed = true;
                    }
                }
                break;

            case EventType.MouseUp:
                m_isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    
}