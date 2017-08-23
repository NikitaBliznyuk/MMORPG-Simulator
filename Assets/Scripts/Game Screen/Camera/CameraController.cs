using UnityEngine;

/// <summary>
/// Class is used to folow target.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Current target, that camera follows.
    /// </summary>
    private Transform currentTarget;

    /// <summary>
    /// Offset from target.
    /// </summary>
    private Vector3 offset;

    #region Unity functions

    private void Awake()
    {
        Loader.DataUpdated += LoaderOnDataUpdated;
    }

    private void LateUpdate()
    {
        if (currentTarget != null)
        {
            transform.position = currentTarget.transform.position + offset;
        }
    }

    #endregion

    /// <summary>
    /// Triggers when data updated from loader.
    /// </summary>
    /// <param name="data">Current level data.</param>
    private void LoaderOnDataUpdated(LevelCurrentData data)
    {
        offset = transform.position - data.PlayerReference.transform.position;
        currentTarget = data.PlayerReference.transform;
    }
}