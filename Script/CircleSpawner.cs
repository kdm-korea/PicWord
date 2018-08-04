using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    private readonly float Degree = 360.0f;
    
    public GameObject[] cubes;
    public Transform parent;
    public float spawnHeight = 1.0f;
    public float spawnRadius = 2.0f;

    private int alphabetCount;
    private float individualDegree;

    private Calculator calculator;

    private void Awake()
    {
        calculator = GetComponent<Calculator>();

        alphabetCount = cubes.Length;
        individualDegree = Degree / alphabetCount;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < alphabetCount; i++)
        {
            Vector3 position = calculator.GetPosition(individualDegree * i, spawnRadius, spawnHeight);
            GameObject _cube = Instantiate(cubes[i], position, Quaternion.identity, parent);

            Transform cubeTransform = _cube.GetComponent<Transform>();
            cubeTransform.LookAt(new Vector3(0, spawnHeight + 0.5f, 0));
        }
    }
}