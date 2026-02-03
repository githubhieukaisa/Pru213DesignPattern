using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class JumpCommand : ICommand
{
    private readonly Player1Move _player;

    public JumpCommand(Player1Move player)
    {
        _player = player;
    }

    public void Execute()
    {
        _player.PerformJump();
    }

    public void Undo() { }
}

public class MoveCommand : ICommand
{
    private readonly Player1Move _player;
    private readonly float _direction;
    private readonly Vector3 _previousPosition;

    public MoveCommand(Player1Move player, float direction)
    {
        _player = player;
        _direction = direction;
        _previousPosition = player.transform.position;
    }

    public void Execute()
    {
        _player.PerformMove(_direction);
    }

    public void Undo()
    {
        _player.Teleport(_previousPosition);
    }
}