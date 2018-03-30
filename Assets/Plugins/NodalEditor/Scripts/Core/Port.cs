using System;
using Interfaces;
using UnityEngine;

public class Port : IDrawable, IRestorable, IResetable, ICreatable<Port>, IDeletable, IInitializable
{
    public static Rect PortRect = new Rect(0, 0, 100, 100);

    public GUIStyle style;

    private PortData data;

    public virtual Color BackgroundColor
    {
        get
        {
            return Color.white;
        }
    }

    public void Draw()
    {
        GUI.Button(PortRect, "", style);
    }

    public bool Restore(ScriptableObject scriptable)
    {
        if (scriptable is PortData)
        {
            data = scriptable as PortData;
        }

        return false;
    }

    public void Reset()
    {
        data.ConnectedPortID = -1;
    }

    public virtual Port Create()
    {
        Port port = new Port();
        port.data = ScriptableObject.CreateInstance<PortData>();
        return port;
    }

    public bool Delete()
    {
        throw new System.NotImplementedException();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PortAttribute : Attribute
    {
        public Type ValueType;

        public virtual Type PortType { get { return typeof(Port); } }

        private string m_name;
        public string Name
        {
            get { return m_name; }
        }
        private PortTranslation m_translation;
        private int m_group;

        public PortAttribute(string Name, PortTranslation Translation, int Group)
        {
            m_name = Name;
            m_translation = Translation;
            m_group = Group;
        }

        public virtual bool IsCompatibleWith(Port port)
        {
            return port.data.Translation != m_translation;
        }

        public virtual Port CreateNew(Node body)
        {
            Port port = new Port();
            port.Create();
            //TODO: link node
            port.data.Name = m_name;
            port.data.Translation = m_translation;
            port.data.Group = m_group;
            return port;
        }
    }

    public void Init()
    {
        throw new NotImplementedException();
    }
}
