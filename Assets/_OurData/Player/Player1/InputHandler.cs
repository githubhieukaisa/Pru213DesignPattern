using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player1Move playerMove;

    private JumpCommand _jumpCommand;
    private MoveCommand _moveCommand;

    private void Awake()
    {
        _jumpCommand = new JumpCommand(playerMove);
        _moveCommand = new MoveCommand(playerMove);
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        _moveCommand.UpdateDirection(inputX);
        _moveCommand.Execute();

        if (Input.GetButtonDown("Jump"))
        {
            _jumpCommand.Execute();
        }
    }
}