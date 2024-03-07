using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float PredictedTime;
    public Motor objectA;
    public Motor objectB;

    // Start is called before the first frame update
    void Start()
    {
        float h = objectA.transform.position.x - objectB.transform.position.x;

        float a = objectB.acceleration - objectA.acceleration;
        float b = 2 * (objectB.initialVelocity - objectA.initialVelocity);
        float c = -2 * h;

        PredictedTime = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        print(PredictedTime);
    }
}

