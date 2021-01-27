using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    public bool wasCut = false;
    private Vector2 EnterPosition;
    private Vector2 ExitPosition;
    public Vector2 TargetDirection;
    public float ThresholdAngle = 5f;
    [SerializeField] private GameObject arrow;

    private void Start()
    {
        // var ran = Random.Range(0, 2);
        // var ran1 = Random.Range(0, 2);
        // if (ran == 0)
        // {
        //     SetTargetDirection(new Vector2(0, ran1 == 0 ? -1 : 1));
        // }
        // else if (ran == 1)
        // {
        //     SetTargetDirection(new Vector2((ran1 == 0 ? -1 : 1), 0));
        // }
        //
        // Random.Range(-1, 1);
    }

    public void SetTargetDirection(Vector2 direction)
    {
        TargetDirection = direction;

        arrow.transform.Rotate(new Vector3(0, 0, 1), Vector2.Angle(Vector2.right, direction));
    }
    
    private Vector2 CutDirection()
    {
        return ExitPosition - EnterPosition;
    }

    public bool SuccessfulCut()
    {
        return Vector2.Angle(TargetDirection, CutDirection()) < ThresholdAngle;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ExitPosition = other.gameObject.transform.position;
        wasCut = true;
        if (SuccessfulCut())
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnterPosition = other.gameObject.transform.position;
    }
    
}