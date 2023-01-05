using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToxicFamilyGames
{
    namespace TutorialEditor
    {
        public class FollowHand : MonoBehaviour
        {
            [Range(0.1f, 5f)]
            [SerializeField]
            private float endTime = 1, speed = 1;
            // Start is called before the first frame update
            private Vector2 startPosition;
            private RectTransform start, end;
            void Start()
            {
                start = transform.GetChild(0).GetComponent<RectTransform>();
                end = transform.GetChild(1).GetComponent<RectTransform>();
                startPosition = start.localPosition;
            }

            private float time = 0;
            void Update()
            {
                time += Time.deltaTime;
                if (time >= endTime)
                {
                    time = 0;
                    start.localPosition = startPosition;
                }
                start.localPosition = Vector2.Lerp(start.localPosition, end.localPosition, speed * Time.deltaTime);
            }
        }
    }
}
