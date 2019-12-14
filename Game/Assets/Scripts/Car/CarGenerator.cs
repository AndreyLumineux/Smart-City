using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public Transform root;
    [Range(0, 1)]
    public float chancePerNode;
    public GameObject[] prefabs;
}
