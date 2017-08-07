using UnityEngine;

public class LevelPath : MonoBehaviour
{
    public static LevelPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Vector3[] pathPoints;

    public Vector3 NextPosition(Vector3 currentPosition)
    {
        float previousDistance = float.MaxValue;
        int i = 0;

        for (; i < pathPoints.Length; i++)
        {
            float currentDistance = Vector3.Distance(currentPosition, pathPoints[i]);
            if (currentDistance < previousDistance)
            {
                previousDistance = currentDistance;
            }
            else
            {
                break;
            }
        }
        
        return pathPoints[i < pathPoints.Length ? i : pathPoints.Length - 1];
    }
}
