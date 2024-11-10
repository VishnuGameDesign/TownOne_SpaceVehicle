using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class FindThePartMiniGame : MonoBehaviour, IDragHandler
    {
        [SerializeField] private RectTransform _partToMove;
        [SerializeField] private Button _button;
        
        public void OnDrag(PointerEventData eventData)
        {
            _partToMove.anchoredPosition += eventData.delta;
        }

        private void CompareParts()
        {
            
        }
    }
}