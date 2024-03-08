using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class DumSpawner : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private List<GameObject> _layoutInstances;

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
        _layoutInstances = new List<GameObject>();

        for (int x = 0; x < layoutPrefabs.Length; x++)
        {
            _layoutInstances.Add(Instantiate(layoutPrefabs[x], _transform.position, Quaternion.identity));
            _layoutInstances[x].SetActive(false);
        }
        _layoutInstances[0].SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.DrawLine(new Vector3(_camRightEdge, _transform.position.y), _transform.position, Color.black);
    }

    void InstantiateLayout(int indexOfLayout)
    {
        _layoutInstances.Add(Instantiate(layoutPrefabs[indexOfLayout], _transform.position, Quaternion.identity));
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
       
        
        _layoutInstances[x].SetActive(true);

        float layoutWidth = _layoutInstances[x].GetComponent<BoxCollider2D>().bounds.size.x;
        float positionX = _camRightEdge + (layoutWidth / 2);
        float positionY = this.transform.position.y;
        _layoutInstances[x].transform.position = new Vector3(positionX, positionY);
        
        
        Debug.DrawLine(_layoutInstances[x].transform.position, _transform.position, Color.yellow,2f);
    }

    void EndReached(int indexOfLayout)
    {
        Debug.Log("Layout"+indexOfLayout+" has reached the end.");
        _layoutInstances[indexOfLayout-1].SetActive(false);
    }
}
