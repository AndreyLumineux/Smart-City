using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Road
{
    public class RoadNode : MonoBehaviour
    {
        public List<RoadNode> adjacentNodes = new List<RoadNode>();

        public void AddNode(RoadNode node)
        {
            adjacentNodes.Add(node);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.gameObject.GetComponent<CarAI>() is CarAI carAI)) return;
            carAI.StopMoving();
            RoadNode next = adjacentNodes[Random.Range(0, adjacentNodes.Count)];
            carAI.MoveTo(next);
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
            Gizmos.DrawIcon(transform.position, "roadicon.png");
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
