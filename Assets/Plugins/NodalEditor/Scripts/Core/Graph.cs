using Interfaces;
using UnityEngine;

public class Graph : IDrawable, ISaveable, IRestorable
{
    private GraphData data;

    public void Draw()
    {
        throw new System.NotImplementedException();
    }

    public bool Save()
    {
        throw new System.NotImplementedException();
    }

    public bool Restore(ScriptableObject scriptable)
    {
        throw new System.NotImplementedException();
    }
}
