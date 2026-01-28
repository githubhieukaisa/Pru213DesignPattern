
public interface ICommand
{
    void Execute(Player1Move player);
}

public class JumpCommand : ICommand
{
    public void Execute(Player1Move player)
    {
        player.PerformJump();
    }
}

public class MoveCommand : ICommand
{
    private float _direction;

    public MoveCommand(float direction)
    {
        _direction = direction;
    }

    public void Execute(Player1Move player)
    {
        player.PerformMove(_direction);
    }
}