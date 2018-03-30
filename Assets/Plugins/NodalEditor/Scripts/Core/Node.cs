using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Interfaces;

public class Node : IDrawable
{
    private Rect m_rect = new Rect();
    private string m_title = "No Title";
    private bool m_isDragged = false;
    private GUIStyle m_style = new GUIStyle();
    private int m_id = -1;
    private List<Port> m_ports = new List<Port>();
    private List<int> m_modGroup = new List<int>();

    public Node(Vector2 position, int id)
    {
        m_rect = new Rect(position.x, position.y, 100, 100);
        m_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_style.border = new RectOffset(12, 12, 12, 12);
        m_id = id;
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public void Draw() 
    {
        GUI.Box(m_rect, "", m_style);
        GUI.BeginGroup(m_rect);
        GUILayout.Label(m_title);
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