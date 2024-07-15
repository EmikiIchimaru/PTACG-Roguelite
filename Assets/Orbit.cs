using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Duration of the timer in seconds
    public Transform parent;
    public float speed;

    void Update()
    {
        transform.Rotate(0f,0f, speed * Time.deltaTime);
        transform.position = parent.transform.position;
    }
}
