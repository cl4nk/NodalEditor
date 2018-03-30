using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Interfaces;

public class Node : ScriptableObject, IDrawable
{
    private Rect m_rect = new Rect();
    private bool m_isDragged = false;
    private GUIStyle m_style = new GUIStyle();
    private NodeData m_data = new NodeData();

    //TODO Add static id for creation
    //TODO Create with Type or class name

    public static Node Create(Vector2 position, int id)
    {
        Node node = ScriptableObject.CreateInstance<Node>();
        node.m_data.ID = id;
        node.m_data.Position = position;
        node.m_data.PortDatas = new List<PortData>();
        node.m_data.Name = "No Title";
        node.m_data.ModulableInGroupPorts = new List<int>();
        node.m_data.ModulableOutGroupPorts = new List<int>();
        return node;
    }

    public Node(Vector2 position)
    {
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
        GUILayout.Label(m_data.Name);
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