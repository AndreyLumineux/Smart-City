using System.Collections.Generic;
using System.Linq;
using Car;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Road
{
    public class RoadNode : MonoBehaviour
    {
        public List<RoadNode> adjacentNodes = new List<RoadNode>();

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!(other.gameObject.GetComponent<CarAI>() is CarAI carAI)) return;
            carAI.StopMoving();
            if (adjacentNodes.Any())
            {
                RoadNode next = adjacentNodes[Random.Range(0, adjacentNodes.Count)];
                // carAI.transform.position = transform.position;
                carAI.QueueMoveTo(next);
            }
        }

        private void OnDrawGizmos()
        {
            DrawGizmos(Color.red);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos(Color.green);
        }

        private void DrawGizmos(Color color)
        {
            Gizmos.DrawIcon(transform.position + Vector3.up * 0.5f, "roadicon.png");
            Gizmos.color = color;
            foreach (RoadNode node in adjacentNodes)
            {
                if (node != null)
                {
                    Gizmos.DrawLine(transform.position, node.transform.position);
                }
            }
        }
    }
}
