using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")] 
    
    [SerializeField] private float dampTime = 0.5f;
    
    private Transform currentTarget;
    private Vector3 offset;
    
    private void Awake()
    {
        Loader.DataUpdated += LoaderOnDataUpdated;
    }

    private void LoaderOnDataUpdated(LevelCurrentData data)
    {
        offset = transform.position - data.PlayerReference.transform.position;
        currentTarget = data.PlayerReference.transform;
    }

    private void LateUpdate()
    {
        if (currentTarget != null)
        {
            Vector3 currentVelocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, currentTarget.transform.position + offset,
                ref currentVelocity, dampTime);
        }
    }
}
