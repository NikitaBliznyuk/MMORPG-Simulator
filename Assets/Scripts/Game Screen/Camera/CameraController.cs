using UnityEngine;

public class CameraController : MonoBehaviour
{
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
            transform.position = currentTarget.transform.position + offset;
        }
    }
}
