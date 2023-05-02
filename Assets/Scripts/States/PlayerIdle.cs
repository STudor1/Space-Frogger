using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IPlayerState
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
        if (input.Equals(KeyCode.W)) return new PlayerMoving();
        if (input.Equals(KeyCode.S)) return new PlayerMoving();
        if (input.Equals(KeyCode.A)) return new PlayerMoving();
        if (input.Equals(KeyCode.D)) return new PlayerMoving();
        return null;
    }
}
