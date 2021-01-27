using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    public GameObject SpawnBox()
    {
        return Instantiate(boxPrefab);
    }
}
