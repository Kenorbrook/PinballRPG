using Script.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class TriggerPanel : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.ClickEvent(eventData.position.x > Screen.width/2.0);
        }

    }
}
