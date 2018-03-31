using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class NodeBasedWindow : EditorWindow
{
    [MenuItem("Window/Window Editor")]
    private static void OpenWindow()
    {
        NodeBasedWindow window = GetWindow<NodeBasedWindow>();
        window.titleContent = new GUIContent("Window Editor");
    }

    private void OnGUI()
    {
        DrawNodes();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawNodes()
    {
    }

    private void ProcessEvents(Event e)
    {
    }
    /*   private GUIStyle m_nodeStyle;
       //private Graph m_graph;

       [MenuItem("Window/Window Editor")]
       private static void OpenWindow()
       {
           NodeBasedWindow window = GetWindow<NodeBasedWindow>();
           window.titleContent = new GUIContent("Window Editor");
           //window.m_graph = Graph.Create(typeof(Graph));
       }

       private void OnGUI()
       {
           if (m_graph != null)
           {
               m_graph.Draw();
               m_graph.Process();
           }

           ProcessEvents(Event.current);

           if (GUI.changed)
           {
               Repaint();
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

       private void ProcessContextMenu(Vector2 mousePosition)
       {
           //GenericMenu genericMenu = new GenericMenu();
           //genericMenu.AddItem( new GUIContent("Add node"), false, () => m_graph.AddNode(mousePosition) );
           //genericMenu.ShowAsContext();
       }*/
}