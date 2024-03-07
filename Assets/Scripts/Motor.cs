using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float initialVelocity;
    public float acceleration;
    private float _currentVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentVelocity = initialVelocity;
    }
    
    void FixedUpdate()
    {
        if (Time.fixedTime < Timer.PredictedTime)
        {
            _currentVelocity += acceleration * Time.fixedDeltaTime;
            transform.Translate(Vector3.left * (_currentVelocity * Time.fixedDeltaTime)); 
        }
    }
}
