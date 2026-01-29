
public interface ICommand
{
    void Execute();
}

public class JumpCommand : ICommand
{
    private Player1Move _player;

    public JumpCommand(Player1Move player)
    {
        _player = player;
    }

    public void Execute()
    {
        _player.PerformJump();
    }
}

public class MoveCommand : ICommand
{
    private Player1Move _player;
    private float _direction;

    public MoveCommand(Player1Move player)
    {
        _player = player;
    }

    public void UpdateDirection(float direction)
    {
        _direction = direction;
    }

    public void Execute()
    {
        _player.PerformMove(_direction);
    }
}