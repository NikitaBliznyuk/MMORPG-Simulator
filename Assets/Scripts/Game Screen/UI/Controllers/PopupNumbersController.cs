using System.Collections.Generic;
using UnityEngine;

public class PopupNumbersController : MonoBehaviour
{
    #region Singletone

    public static PopupNumbersController Instance;

    private void Awake()
    {
        Instance = this;
        TextPool = new Stack<PopupTextBehaviour>();
    }

    #endregion

    [Header("References")]
    
    [SerializeField] private PopupTextBehaviour textPrefab;

    public Stack<PopupTextBehaviour> TextPool { get; private set; }

    public void CreateText(Vector3 position, Color color, string text)
    {
        var popupText = TextPool.Count == 0 ? Instantiate(textPrefab, transform) : TextPool.Pop();
        
        popupText.Show(text, color, position);
    }
}
