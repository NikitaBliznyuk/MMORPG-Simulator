using UnityEngine;

public class LevelPath : MonoBehaviour
{
    public static LevelPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Vector3[] pathPoints;

    public int PathCount
    {
        get { return pathPoints.Length; }
    }

    public Vector3[] Path
    {
        get { return pathPoints; }
    }

    #region Editor only

    private void OnDrawGizmosSelected()
    {
        float radius = 0.5f;
        Color color = Color.cyan;
        
        foreach (var pathPoint in pathPoints)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(pathPoint, radius);
        }
    }

    #endregion
}
