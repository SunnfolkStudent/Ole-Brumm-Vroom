using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class DumSpawner : MonoBehaviour
{
    public static DumSpawner Instance { get; private set; }
    
    [SerializeField] private Vector3[] obstacleSpawnAreas;
    
    [SerializeField] private GameObject[] obstaclePrefab;
    private static List<GameObject> _obstacleInstances;
    
    private static Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private static List<GameObject> _layoutInstancesNoObstacles;

    [SerializeField] private Camera cam;
    private float _camRightEdge;

    private void OnDestroy()
    {
        Debug.Log("DUM SPAWNER DESTROYED!!!!!!!!!!!!!!!!!!!");
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        BoxCollider2D camBoundaries = cam.GetComponent<BoxCollider2D>();
        _camRightEdge = camBoundaries.transform.position.x + camBoundaries.bounds.size.x / 2;
        
        
        Parallax.OnRightEdgeView += EdgeReachView;
        Parallax.ReachEnd += EndReached;
        Parallax.ObstacleReachEnd += DeactivateObstacle;
                        
        _transform = transform;
                
        PopulateInstanceArrays();
        
        _layoutInstancesNoObstacles[0].SetActive(true);
        
        SpawnObstacle(2);
    }

    private void FixedUpdate()
    {
        Debug.Log("DUM SPAWNER FIXEDUPDATE");
        foreach (GameObject go in _layoutInstancesNoObstacles)
        {
            Debug.Log("Layout instance: " + go);
        }
    }

    void PopulateInstanceArrays()
    {
        
        _layoutInstancesNoObstacles = new List<GameObject>();
        for (int x = 0; x < layoutPrefabs.Length; x++) 
        { 
            Debug.Log("Instantiating layouts!");
            Debug.Log("_transform.position: "+_transform.position);
            Debug.Log("layoutPrefabs[x]: "+layoutPrefabs[x]);
            
            _layoutInstancesNoObstacles.Add(Instantiate(layoutPrefabs[x], _transform.position, Quaternion.identity)); 
            _layoutInstancesNoObstacles[x].SetActive(false);
        }
        
        
        _obstacleInstances = new List<GameObject>();
        for (int x = 0; x < obstaclePrefab.Length; x++) 
        { 
            _obstacleInstances.Add(Instantiate(obstaclePrefab[x],obstacleSpawnAreas[x],Quaternion.identity));
            _obstacleInstances[x].SetActive(false);
        }
    }
    
    void InstantiateLayout(int indexOfLayout)
    {
        _layoutInstancesNoObstacles.Add(Instantiate(layoutPrefabs[indexOfLayout], _transform.position, Quaternion.identity));
    }

    void EdgeReachView(int indexOfLayout)
    {
        Debug.Log("Edge reached view!");
        
        indexOfLayout--;
        int x; //Layout about to be spawned
        
        do
        {
            Random rnd = new Random();
            x = rnd.Next(1, layoutPrefabs.Length);
            
        } while (x==indexOfLayout);

        int numberOfActiveLayouts = 0;
        foreach (GameObject go in _layoutInstancesNoObstacles)
        {
            if (go.activeSelf)
            {
                numberOfActiveLayouts++;
            }
        }

        if (numberOfActiveLayouts < 2)
        {
            Debug.Log("Trying to activate: "+_layoutInstancesNoObstacles[x]);
            _layoutInstancesNoObstacles[x].SetActive(true);

            float layoutWidth = _layoutInstancesNoObstacles[x].GetComponent<BoxCollider2D>().bounds.size.x;
            float positionX = _camRightEdge + (layoutWidth / 2);
            float positionY = DumSpawner._transform.position.y;
            _layoutInstancesNoObstacles[x].transform.position = new Vector3(positionX, positionY);
        }
       
        Debug.DrawLine(_layoutInstancesNoObstacles[x].transform.position, _transform.position, Color.yellow,2f);

        // obsPreset can't contain these:
        // {1,2,3} --- 1, 2 and 3 can't be in the same preset
        // {4,5,6}
        // {7,8,9}
        
        int[] obsPreset1 = new[] { 1, 4, 7 }; // preset of PlatformLayout1
        int[] obsPreset2 = new[] { 3, 6, 9 };
        int[] obsPreset3 = new[] { 3, 6, 7 };
        int[] obsPreset4 = new[] { 2, 5, 9 };
        int[] obsPreset5 = new[] { 2, 5, 8 };
        int[] obsPreset6 = new[] { 3, 4, 9 };
        int[][] obsPresets = new int[][] { obsPreset1, obsPreset2, obsPreset3, obsPreset4, obsPreset5, obsPreset6  };
        
        
        // int numberOfActiveObstacles = 0;
        
        int firstObsIndex = obsPresets[x][0];
        int secondObsIndex= obsPresets[x][1];
        int thirdObsIndex = obsPresets[x][2];
        
        SpawnObstacle(firstObsIndex);
        SpawnObstacle(secondObsIndex);
        SpawnObstacle(thirdObsIndex);
        

        // foreach (GameObject go in _obstacleInstances)
        // {
        //     if (go.activeSelf)
        //     {
        //         numberOfActiveObstacles++;
        //     }
        // }

        // if (numberOfActiveObstacles < 2)
        // {
        //     int[] lane1 = new[] { 0, 1, 2 };
        //     Random lr1 = new Random();
        //     firstObsIndex = lane1[lr1.Next(0, lane1.Length - 1)];
        //     SpawnObstacle(firstObsIndex+1);
        //
        //     numberOfActiveObstacles++;
        // }
        //
        // if (numberOfActiveObstacles < 2)
        // {
        //     int[] lane2 = new[] { 3, 4, 5 };
        //     Random lr2 = new Random();
        //     secondObsIndex = lane2[lr2.Next(0, lane2.Length - 1)];
        //     SpawnObstacle(secondObsIndex+1);
        //     
        //     numberOfActiveObstacles++;
        // }
        //
        // if (numberOfActiveObstacles < 2)
        // {
        //     int[] lane3 = new[] { 6, 7, 8 };
        //     Random lr3 = new Random();
        //     thirdObsIndex = lane3[lr3.Next(0, lane3.Length - 1)];
        //     SpawnObstacle(thirdObsIndex+1);
        //     
        //     numberOfActiveObstacles++;
        // }
        
       
        
    }

    void EndReached(int indexOfLayout)
    {
        Debug.Log("Layout"+indexOfLayout+" has reached the end.");
        _layoutInstancesNoObstacles[indexOfLayout-1].SetActive(false);
    }

    void SpawnObstacle(int indexOfObstacle)
    {
        _obstacleInstances[indexOfObstacle-1].SetActive(true);
    }
    
    void DeactivateObstacle(int indexOfObstacle)
    {
        Debug.Log("Deactivate invoked by: "+indexOfObstacle); 
        if (_obstacleInstances[indexOfObstacle - 1].gameObject != null)
        {
            _obstacleInstances[indexOfObstacle - 1].transform.position = obstacleSpawnAreas[indexOfObstacle - 1];
            _obstacleInstances[indexOfObstacle-1].SetActive(false);
        }
    }
}
