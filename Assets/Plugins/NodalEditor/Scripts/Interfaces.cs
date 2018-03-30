using UnityEngine;

namespace Interfaces
{
    public interface IDrawable
    {
        void Draw();
    }

    public interface ISaveable
    {
        bool Save(string path);
    }

    public interface IResetable
    {
        void Reset();
    }

    public interface IDeletable
    {
        bool Delete();
    }

    public interface ICreatable<T>
    {
        T Create();
    }

    public interface IInitializable
    {
        void Init();
    }

    public interface IColorable
    {
        Color CustomColor { get; }
    }

    public interface INameable
    {
        string Title { get; }
    }

}
