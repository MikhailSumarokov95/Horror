using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace ToxicFamilyGames
{
    namespace FirstPersonController
    {
        public class TouchSystem : MonoBehaviour, IDragHandler, IPointerDownHandler
        {
            public Action<Vector2> OnDragForMove; 
            private Vector2 delta = Vector2.zero;

            private Vector2 startPosition = Vector2.zero;

            public void OnPointerDown(PointerEventData eventData)
            {
                startPosition = eventData.position;
                OnDrag(eventData);
            }

            public void OnDrag(PointerEventData eventData)
            {
                delta = (eventData.position - startPosition);
                startPosition = eventData.position;
                OnDragForMove?.Invoke(delta);
            }
        }
    }
}