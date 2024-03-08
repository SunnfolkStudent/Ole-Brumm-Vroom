using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumSpawner : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject[] layoutPrefabs;
    private List<GameObject> _layoutInstances;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Parallax.OnRightEdgeView += EdgeReachView;
        
        _transform = this.transform;
        _layoutInstances = new List<GameObject>();
        
        InstantiateLayout(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateLayout(int indexOfLayout)
    {
        _layoutInstances.Add(Instantiate(layoutPrefabs[indexOfLayout], _transform.position, Quaternion.identity));
    }

    void EdgeReachView(int indexOfLayout)
    {
        Debug.Log("Edge reached view!");
    }
    
    
}
