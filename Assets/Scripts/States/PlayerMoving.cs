using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : IPlayerState
{
    public void Enter()
    {
        return;
    }

    public void Exit()
    {
        return;
    }

    public IPlayerState Tick(Transform player, KeyCode input, Frogger frogger, GameManager manager, float farthestRow, InputHandler inputHandler)
    {
        if (input.Equals(KeyCode.W))
        {
            frogger.Rotation(0);
            inputHandler.ExecuteCommand(new MoveCommand(frogger, Vector3.up, manager, farthestRow, frogger));
        }
        else if (input.Equals(KeyCode.S))
        {
            frogger.Rotation(180);
            inputHandler.ExecuteCommand(new MoveCommand(frogger, Vector3.down, manager, farthestRow, frogger));
        }
        else if (input.Equals(KeyCode.A))
        {
            frogger.Rotation(90);
            inputHandler.ExecuteCommand(new MoveCommand(frogger, Vector3.left, manager, farthestRow, frogger));
        }
        else if (input.Equals(KeyCode.D))
        {
            frogger.Rotation(-90);
            inputHandler.ExecuteCommand(new MoveCommand(frogger, Vector3.right, manager, farthestRow, frogger));
        }
        return new PlayerIdle();
    }
}
