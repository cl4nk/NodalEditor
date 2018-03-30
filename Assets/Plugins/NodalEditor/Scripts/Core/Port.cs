using Interfaces;
using UnityEngine;

public class Port : IDrawable, IRestorable, IResetable, ICreatable<Port>, IDeletable
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

    public Port Create()
    {
        Port port = new Port();
        port.data = ScriptableObject.CreateInstance<PortData>();
        return port;
    }

    public bool Delete()
    {
        throw new System.NotImplementedException();
    }
}
