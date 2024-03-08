using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DumSpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] obstacleSpawnAreas;
    [SerializeField] private GameObject obstaclePrefab;
    private List<GameObject> _obstacleInstances;
    
    private Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private List<GameObject> _layoutInstancesNoObstacles;

    [SerializeField] private Camera cam;
    private float _camRightEdge;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        BoxCollider2D camBoundaries = cam.GetComponent<BoxCollider2D>();
        _camRightEdge = camBoundaries.transform.position.x + camBoundaries.bounds.size.x / 2;
        
        _obstacleInstances = new List<GameObject>();
        
        Parallax.OnRightEdgeView += EdgeReachView;
        Parallax.ReachEnd += EndReached;
        Parallax.ObstacleReachEnd += DestroyObstacle;
                        
        _transform = transform;
        _layoutInstancesNoObstacles = new List<GameObject>();
                
        for (int x = 0; x < layoutPrefabs.Length; x++) 
        { 
            _layoutInstancesNoObstacles.Add(Instantiate(layoutPrefabs[x], _transform.position, Quaternion.identity)); 
            _layoutInstancesNoObstacles[x].SetActive(false);
        }
        _layoutInstancesNoObstacles[0].SetActive(true);
    }
    void InstantiateLayout(int indexOfLayout)
    {
        _layoutInstancesNoObstacles.Add(Instantiate(layoutPrefabs[indexOfLayout], _transform.position, Quaternion.identity));
    }

    void EdgeReachView(int indexOfLayout)
    {
        indexOfLayout--;
        int x;
        do
        {
            Random rnd = new Random();
            x = rnd.Next(1, layoutPrefabs.Length);
            
        } while (x==indexOfLayout);
       
        
        _layoutInstancesNoObstacles[x].SetActive(true);

        float layoutWidth = _layoutInstancesNoObstacles[x].GetComponent<BoxCollider2D>().bounds.size.x;
        float positionX = _camRightEdge + (layoutWidth / 2);
        float positionY = this.transform.position.y;
        _layoutInstancesNoObstacles[x].transform.position = new Vector3(positionX, positionY);
        
        Debug.DrawLine(_layoutInstancesNoObstacles[x].transform.position, _transform.position, Color.yellow,2f);

        int[] lane1 = new int[] { 0, 1, 2 };
        var random1 = new Random();
        var randomResult1 = lane1[random1.Next(0, 2)];
        SpawnObstacle(obstacleSpawnAreas[randomResult1]);
        
        int[] lane2 = new int[] { 3, 4, 5};
        var random2 = new Random();
        var randomResult2 = lane2[random1.Next(0, 2)];
        SpawnObstacle(obstacleSpawnAreas[randomResult2]);

        int[] lane3 = new int[] { 6, 7, 8 };
        var random3 = new Random();
        var randomResult3 = lane3[random1.Next(0, 2)];
        SpawnObstacle(obstacleSpawnAreas[randomResult3]);


        
        
        // for (int z = 1; z <= 3; z++)
        // {
        //     Random r1 = new Random();
        //     int y1 = r1.Next(1, obstacleSpawnAreas.Length);
        //     SpawnObstacle(obstacleSpawnAreas[y1-1]);
        // }
        
        
        
    }

    void EndReached(int indexOfLayout)
    {
        Debug.Log("Layout"+indexOfLayout+" has reached the end.");
        _layoutInstancesNoObstacles[indexOfLayout-1].SetActive(false);
    }

    void SpawnObstacle(Vector3 spawnArea)
    {
        _obstacleInstances.Add(Instantiate(obstaclePrefab, spawnArea, Quaternion.identity));
    }
    
    void DestroyObstacle(GameObject obj)
    {
        _obstacleInstances.Remove(obj);
        Destroy(obj);
    }
}
