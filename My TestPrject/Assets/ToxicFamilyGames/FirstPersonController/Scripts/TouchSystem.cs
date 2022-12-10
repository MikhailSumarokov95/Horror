using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace ToxicFamilyGames
{
    namespace FirstPersonController
    {
        public class TouchSystem : MonoBehaviour, IDragHandler, IBeginDragHandler
        {
            private Vector2 delta = Vector2.zero;
            public Vector2 Delta
            {
                get
                {
                    Vector2 copy = delta.normalized;
                    delta = Vector2.zero;
                    return copy;
                }
            }

            private Vector2 startPosition = Vector2.zero;

            public void OnBeginDrag(PointerEventData eventData)
            {
                startPosition = eventData.position;
            }

            public void OnDrag(PointerEventData eventData)
            {
                delta = (eventData.position - startPosition);
                startPosition = eventData.position;
            }
        }
    }
}