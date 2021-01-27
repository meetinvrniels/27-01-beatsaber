using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool wasCut = false;
    private Vector2 EnterPosition;
    private Vector2 ExitPosition;
    public Vector2 TargetDirection;
    public float ThresholdAngle = 5f;

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
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnterPosition = other.gameObject.transform.position;
    }
}