namespace Interfaces
{
    public interface IDrawable
    {
        void Draw();
    }

    public interface ISaveable
    {
        bool Save();
    }

    public interface IRestorable
    {
        bool Restore();
    }

    public interface IResetable
    {
        void Reset();
    }

    public interface IDeletable
    {
        bool Delete();
    }

    public interface ICreatable
    {
        bool Create();
    }
}
