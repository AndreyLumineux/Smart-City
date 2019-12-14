using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Road;
using UnityEngine;

namespace Car
{
    public class CarAI : MonoBehaviour
    {
        [SerializeField] private float raycastDistance = 1f;
        [SerializeField] private float speed;
        private bool isMoving;
        public float Speed => speed;
        public bool IsMoving => isMoving;

        private Coroutine moveCoroutine;
        private int carLayerMask;

        private void Awake()
        {
            carLayerMask = LayerMask.GetMask("Car");
        }

        public void MoveTo(RoadNode node)
        {
            if (isMoving)
            {
                return;
            }

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

        [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
        private IEnumerator MoveToCoroutine(RoadNode node)
        {
            Vector3 position = transform.position;
            Vector3 target = node.transform.position;
            Vector3 direction = target - position;
            float distance = Vector3.Distance(position, target);
            float t = 0;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            while (t < 1)
            {
                if (!Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit _,
                    raycastDistance, carLayerMask, QueryTriggerInteraction.Collide))
                {
                    t += Time.deltaTime * speed / distance;
                    transform.position = Vector3.Lerp(position, target, t);
                }

                yield return null;
            }

            StopMoving();
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
