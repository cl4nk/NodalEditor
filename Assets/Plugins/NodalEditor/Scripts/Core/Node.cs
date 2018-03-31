using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Interfaces;

//TODO: abstract it when finished
public class Node : ScriptableObject, IDrawable, INameable, IColorable, IRestorable<NodeData>
{
    private static int GlobalID = 0;

    private Graph m_ownerGraph = null;
    private Rect m_rect = new Rect();
    private bool m_isDragged = false;
    private bool m_isSelected = false;
    private GUIStyle m_currentStyle = null;
    private GUIStyle m_style = new GUIStyle();
    private GUIStyle m_selectedStyle = new GUIStyle();

    private List<Port> m_ports = new List<Port>();

    public NodeData Data { get; private set; }

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

    public virtual int[] ModulableGroupPorts
    {
        get { return new[] {0}; }
    }

    // DataType must hinerit from @NodeData
    public virtual Type DataType
    {
        get { return typeof(NodeData); }
    }

    public static Node Create(string className)
    {
        NodeTypeData data = NodeTypes.getNodeData(className);
        return Create(data.type);
    }

    // type must hinerit from @Node
    public static Node Create(Type type)
    {
        //TODO: throw exception
        if (type.IsAssignableFrom(typeof(Node)))
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.Data = ScriptableObject.CreateInstance(node.DataType) as NodeData;
            node.Data.ClassName = type.FullName;
            return node;
        }

        return null;
    }

    public static Node Restore(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Fill(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Init(Vector2 position, Graph ownerGraph)
    {
        Data.ID = GlobalID++;
        Data.Position = position;

        m_ownerGraph = ownerGraph;
        m_rect = new Rect(position.x, position.y, 100, 100);
        m_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_style.border = new RectOffset(12, 12, 12, 12);
        m_selectedStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        m_selectedStyle.border = new RectOffset(12, 12, 12, 12);

        m_currentStyle = m_style;
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public void Draw() 
    {
        GUI.Box(m_rect, Title, m_currentStyle);
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
                        m_isSelected = true;
                        m_currentStyle = m_selectedStyle;
                        GUI.changed = true;
                    }
                    else
                    {
                        m_isSelected = false;
                        m_currentStyle = m_style;
                        GUI.changed = true;
                    }
                }
                else if (e.button == 1 && m_isSelected && m_rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
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

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        m_ownerGraph.DeleteNode(this);
    }
}