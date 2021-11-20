using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoOriginal : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originalPos;
    private Quaternion originalRotation;
    void Start()
    {
        originalPos = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        transform.position = originalPos;
        transform.rotation = originalRotation;
    }
}
