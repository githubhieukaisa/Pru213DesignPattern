using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player1Move playerMove;

    [SerializeField] private Stack<ICommand> _commandHistory = new Stack<ICommand>();
    private enum PlayerAction
    {
        Move,
        Jump
    }
    private PlayerAction playerAction;
    [SerializeField] private float timerPush = 2f;
    private float timer;

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(inputX) > 0.1f)
        {
            ICommand moveCommand = new MoveCommand(playerMove, inputX);
            moveCommand.Execute();
            if (Time.time - timer > timerPush)
            {
                _commandHistory.Push(moveCommand);
                Debug.Log("Move command executed and pushed to history.");
                timer = Time.time;
            }
        }
        else if (playerAction == PlayerAction.Move)
        {
            ICommand moveCommand = new MoveCommand(playerMove, inputX);
            moveCommand.Execute();
        }

        if (Input.GetButtonDown("Jump"))
        {
            ICommand jumpCommand = new JumpCommand(playerMove);
            jumpCommand.Execute();

            if (playerAction != PlayerAction.Jump)
            {
                _commandHistory.Push(jumpCommand);
                Debug.Log("Jump command executed and pushed to history.");
                playerAction = PlayerAction.Jump;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && _commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
        }
    }
}