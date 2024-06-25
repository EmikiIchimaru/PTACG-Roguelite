using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationZ = rotationSpeed * Time.deltaTime;

        // Apply the rotation around the Z axis
        transform.Rotate(0, 0, rotationZ);
    }
}
