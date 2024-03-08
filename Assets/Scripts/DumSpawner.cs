using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class DumSpawner : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private List<GameObject> _layoutInstancesNoObstacles;

    [SerializeField] private Camera cam;
    private float _camRightEdge;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        BoxCollider2D camBoundaries = cam.GetComponent<BoxCollider2D>();
        _camRightEdge = camBoundaries.transform.position.x + (camBoundaries.bounds.size.x / 2);
        
        
        Parallax.OnRightEdgeView += EdgeReachView;
        Parallax.ReachEnd += EndReached;
        
        _transform = this.transform;
        _layoutInstancesNoObstacles = new List<GameObject>();

        for (int x = 0; x < layoutPrefabs.Length; x++)
        {
            _layoutInstancesNoObstacles.Add(Instantiate(layoutPrefabs[x], _transform.position, Quaternion.identity));
            _layoutInstancesNoObstacles[x].SetActive(false);
        }
        _layoutInstancesNoObstacles[0].SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.DrawLine(new Vector3(_camRightEdge, _transform.position.y), _transform.position, Color.black);
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
    }

    void EndReached(int indexOfLayout)
    {
        Debug.Log("Layout"+indexOfLayout+" has reached the end.");
        _layoutInstancesNoObstacles[indexOfLayout-1].SetActive(false);
    }
}
