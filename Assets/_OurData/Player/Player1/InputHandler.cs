using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player1Move playerMove;

    private ICommand _jumpCommand = new JumpCommand();

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        ICommand moveCommand = new MoveCommand(inputX);
        moveCommand.Execute(playerMove);

        if (Input.GetButtonDown("Jump"))
        {
            _jumpCommand.Execute(playerMove);
        }
    }
}