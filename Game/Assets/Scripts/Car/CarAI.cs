using System.Collections;
using Road;
using UnityEngine;

namespace Car
{
    public class CarAI : MonoBehaviour
    {
        [SerializeField] private float raycastDistance = 2;
        [SerializeField] private float speed;
        private bool isMoving;
        public float Speed => speed;
        public bool IsMoving => isMoving;

        private Coroutine moveCoroutine;
        private int carLayerMask;
        private RoadNode nextNode;

        private void Awake()
        {
            carLayerMask = LayerMask.GetMask("Car");
        }
        
        public void QueueMoveTo(RoadNode next)
        {
            nextNode = next;
            if (!isMoving)
            {
                MoveTo(next);
            }
        }

        private void MoveTo(RoadNode node)
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            nextNode = null;
            moveCoroutine = StartCoroutine(MoveToCoroutine(node));
        }

        public void StopMoving()
        {
            isMoving = false;
            // nextNode = null;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }
        
        private IEnumerator MoveToCoroutine(RoadNode node)
        {
            Vector3 position = transform.position;
            Vector3 target = node.transform.position;
            Vector3 direction = target - position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            float distance = Vector3.Distance(position, target);
            float t = 0;
            while (t < 1)
            {
                if (!Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit _,
                    raycastDistance, carLayerMask))
                {
                    t += Time.deltaTime * speed / distance;
                    transform.position = Vector3.Lerp(position, target, t);
                }

                yield return null;
            }

            isMoving = false;
            if (nextNode)
            {
                MoveTo(nextNode);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Transform t = transform;
            Vector3 position = t.position;
            Gizmos.DrawLine(position, t.forward * raycastDistance + position);
        }
    }
}
