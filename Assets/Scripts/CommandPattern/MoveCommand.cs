using UnityEngine;
using System.Collections;

public class MoveCommand : Command
{
    private Vector3 _direction;
    private GameManager _manager;
    private float _farthestRow;
    private Frogger _frogger; 

    public MoveCommand(IEntity entity, Vector3 direction, GameManager manager, float farthestRow, Frogger frogger) : base (entity)
    {
        _direction = direction;
        _manager = manager;
        _farthestRow = farthestRow;
        _frogger = frogger;
    }

    public override void Execute()
    {
        Vector3 destination = entity.transform.position + _direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        //This means that if there is a barrier on the next destination we return without doing the code after
        if (barrier != null)
        {
            return;
        }

        if (platform != null)
        {
            entity.transform.SetParent(platform.transform); //attach our self to platform
        }
        else
        {
            entity.transform.SetParent(null); //detach from platform
        }

        if (obstacle != null && platform == null)
        {
            entity.transform.position = destination;
            _frogger.Death();
        }
        else
        {
            if (destination.y > _farthestRow)
            {
                _farthestRow = destination.y;
                _manager.AdvancedRow();
            }
            _frogger.StartCoroutine(_frogger.Leap(destination));
        }
    }

    public override void Undo()
    {
        Vector3 destination = entity.transform.position - _direction;

        _frogger.StartCoroutine(_frogger.Leap(destination));

    }


}
