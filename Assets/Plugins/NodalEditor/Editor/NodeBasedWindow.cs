using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeBasedWindow : EditorWindow
{
    private List<Node> m_nodes = new List<Node>();
    private GUIStyle m_nodeStyle;

    [MenuItem("Window/Window Editor")]
    private static void OpenWindow()
    {
        NodeBasedWindow window = GetWindow<NodeBasedWindow>();
        window.titleContent = new GUIContent("Window Editor");
    }

    private void OnGUI()
    {
        DrawNodes();

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawNodes()
    {
        if ( m_nodes != null )
        {
            int size = m_nodes.Count;
            for ( int idx = 0; idx < size; ++idx )
            {
                m_nodes[idx].Draw();
            }
        }
    }

    private void ProcessEvents(Event e)
    {
        switch ( e.type )
        {
            case EventType.MouseDown:
                if ( e.button == 1 )
                {
                    ProcessContextMenu( e.mousePosition );
                }
                break;
        }
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

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem( new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition) );
        genericMenu.ShowAsContext();
    }

    private void OnClickAddNode(Vector2 mousePosition)
    {
        if ( m_nodes == null )
        {
            m_nodes = new List<Node>();
        }

        m_nodes.Add( new Node( mousePosition, m_nodes.Count ) );
    }
}