using Road;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CarGenerator))]
    public class CarGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                CarGenerator generator = (CarGenerator) serializedObject.targetObject;
                foreach (RoadNode roadNode in FindObjectsOfType<RoadNode>())
                {
                    if (generator.chancePerNode > Random.value)
                    {
                        GameObject prefab = generator.prefabs[Random.Range(0, generator.prefabs.Length)];
                        GameObject clone = Instantiate(prefab, 
                            roadNode.transform.position, Quaternion.identity, generator.root);
                    }
                }
                DestroyImmediate(generator.gameObject);
            }
        }

        [MenuItem("RoadNodes/Generate Cars")]
        private static void AddGenerator()
        {
            GameObject generator = new GameObject("CarGenerator");
            generator.AddComponent<CarGenerator>();
            Selection.activeGameObject = generator;
        }
    }
}
