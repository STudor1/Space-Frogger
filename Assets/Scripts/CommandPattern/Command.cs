public abstract class Command 
{
    protected IEntity entity;

    public Command(IEntity e)
    {
        entity = e;
    }

    public abstract void Execute();

    public abstract void Undo();
}
