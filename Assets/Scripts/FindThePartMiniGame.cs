using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class FindThePartMiniGame : MonoBehaviour, IDragHandler
    {
        [SerializeField] private RectTransform _partToMove;
        
        public void OnDrag(PointerEventData eventData)
        {
            _partToMove.anchoredPosition += eventData.delta;
        }

        private void CompareParts()
        {
            
        }
    }
}