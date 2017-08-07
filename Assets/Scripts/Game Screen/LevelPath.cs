using UnityEngine;

public class LevelPath : MonoBehaviour
{
    public static LevelPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Transform[] pathPoints;

    public int PathCount
    {
        get { return pathPoints.Length; }
    }

    public Vector3 NextPosition(int index)
    {
        return pathPoints[index < pathPoints.Length ? index : pathPoints.Length - 1].position;
    }
}
