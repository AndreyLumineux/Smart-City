using System.Collections.Generic;
using System.Linq;
using Road;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RoadNode))]
    public class RoadNodeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Add Adjacent Node"))
            {
                RoadNode currentNode = (RoadNode) serializedObject.targetObject;
                Transform transform = currentNode.transform;

                GameObject prefab = PrefabUtility.LoadPrefabContents("Assets/Prefabs/RoadNode.prefab");

                GameObject instance = Instantiate(prefab, transform.position, transform.rotation, transform.parent);
                RoadNode node = instance.GetComponent<RoadNode>();
                currentNode.AddNode(node);

                PrefabUtility.UnloadPrefabContents(prefab);

                serializedObject.ApplyModifiedPropertiesWithoutUndo();

                Selection.activeGameObject = instance;
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem("RoadNodes/Cleanup")]
        public static void CleanupRoadNodes()
        {
            RoadNode[] roadNodes = FindObjectsOfType<RoadNode>();
            List<RoadNode> valid = new List<RoadNode>();
            foreach (RoadNode roadNode in roadNodes)
            {
                List<RoadNode> nodes = roadNode.adjacentNodes.Where(node => node != null).ToList();
                roadNode.adjacentNodes = nodes;
                if (roadNode.adjacentNodes.Any())
                {
                    valid.Add(roadNode);
                    valid.AddRange(roadNode.adjacentNodes);
                }
            }

            foreach (RoadNode roadNode1 in roadNodes.Except(valid))
            {
                Destroy(roadNode1);
            }
        }
    }
}
