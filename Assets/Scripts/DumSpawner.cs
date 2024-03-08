using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DumSpawner : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private List<GameObject> _layoutInstances;
    
    
    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {
        
    }

    void InstantiateLayout(int indexOfLayout)
    {
        _layoutInstances.Add(Instantiate(layoutPrefabs[indexOfLayout], _transform.position, Quaternion.identity));
    }

    void EdgeReachView(GameObject g)
    {
        Random rnd = new Random();
        int x = rnd.Next(1, layoutPrefabs.Length);
        
        _layoutInstances[x].SetActive(true);
    }

    void EndReached(int indexOfLayout)
    {
        Debug.Log("Layout"+indexOfLayout+" has reached the end.");
        _layoutInstances[indexOfLayout-1].SetActive(false);
    }
    
    
}
