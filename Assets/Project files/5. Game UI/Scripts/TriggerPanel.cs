using ProjectFiles.LevelInfrastructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace  ProjectFiles.GameUI
{
    public class TriggerPanel : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.ClickEvent(eventData.position.x > Screen.width/2.0);
        }

    }
}
