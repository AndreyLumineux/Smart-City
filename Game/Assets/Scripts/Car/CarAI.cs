using System.Collections;
using Road;
using UnityEngine;

namespace Car
{
    public class CarAI : MonoBehaviour
    {
        [SerializeField] private float speed;
        public float Speed => speed;

        private bool isMoving;
        public bool IsMoving => isMoving;

        private Coroutine moveCoroutine;

        public void MoveTo(RoadNode node)
        {
            if (isMoving)
                return;

            isMoving = true;
            moveCoroutine = StartCoroutine(MoveToCoroutine(node));
        }

        public void StopMoving()
        {
            isMoving = false;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }

        private IEnumerator MoveToCoroutine(RoadNode node)
        {
            Vector3 position = transform.position;
            Vector3 target = node.transform.position;
            Vector3 direction = target - position;
            float distance = Vector3.Distance(position, target);
            float t = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            while (t < 1)
            {
                t += Time.deltaTime * speed / distance;
                transform.position = Vector3.Lerp(position, target, t);
                yield return null;
            }

            StopMoving();
        }
    }
}
