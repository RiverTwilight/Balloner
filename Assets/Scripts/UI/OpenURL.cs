using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenURL : MonoBehaviour, IPointerClickHandler
{
    public string url;

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }
}
