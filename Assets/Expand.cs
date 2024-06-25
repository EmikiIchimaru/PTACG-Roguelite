using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public float scaleSpeed;
    public float scale = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale += scaleSpeed * Time.deltaTime;
        transform.localScale = new Vector3(scale,scale,1f);
    }
}
