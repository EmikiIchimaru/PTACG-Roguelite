using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    // Duration of the timer in seconds
    public Transform parent;
    public float speed;

    void Update()
    {
        if (speed > 0f) {transform.Rotate(0f,0f, speed * Time.deltaTime);}
        transform.position = parent.position;
    }
}
