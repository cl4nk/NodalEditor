using System;
using Interfaces;
using UnityEngine;

public class Port : ScriptableObject, IDrawable, IResetable, IRestorable<PortData>, IDeletable, INameable, IColorable
{
    public static Rect PortRect = new Rect(0, 0, 100, 100);

    public GUIStyle style;

    public PortData Data { get; private set; }
    
    public string Title
    {
        get
        {
            return "Port";
        }
    }

    public virtual Color CustomColor
    {
        get
        {
            return Color.white;
        }
    }

    // DataType must hinerit from @PortData
    public virtual Type DataType
    {
        get { return typeof(PortData); }
    }

    // type must hinerit from @Port
    public static Port Create(Type type)
    {
        //TODO: throw exception
        if (type.IsAssignableFrom(typeof(Port)))
        {
            Port Port = ScriptableObject.CreateInstance(type) as Port;
            Port.Data = ScriptableObject.CreateInstance(Port.DataType) as PortData;
            Port.Data.ClassName = type.FullName;
            return Port;
        }

        return null;
    }

    public static Port Restore(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Fill(object dataObject)
    {
        throw new NotImplementedException();
    }

    public void Draw()
    {
        GUI.Button(PortRect, "", style);
    }

    public void Reset()
    {
        Data.ConnectedPortID = -1;
    }

    public bool Delete()
    {
        throw new System.NotImplementedException();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PortAttribute : Attribute
    {
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
            return port.Data.Translation != m_translation;
        }

        public Port CreateNew(Node body)
        {
            return Port.Create(PortType);
        }
    }

}
