using UnityEngine;

public class BoxCutter : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    public void SetPosition(Vector2 position)
    {
        // Set mouse position
        transform.position = new Vector3(position.x, position.y);
    }

    private void Update()
    {
        // Get mouse position and move mouse cutter
        SetPosition(_camera.ScreenToWorldPoint(Input.mousePosition));
    }
}
