using UnityEngine;

public interface IPlayerState 
{
    public IPlayerState Tick(Transform player, KeyCode input, Frogger frogger, GameManager manager, float farthestRow, InputHandler inputHandler);
    public void Enter();
    public void Exit();
    
}
