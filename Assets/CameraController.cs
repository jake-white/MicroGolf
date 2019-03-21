using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += 0.1f * Input.GetAxis("Horizontal");
        newPosition.z += 0.1f * Input.GetAxis("Vertical");
        transform.position = newPosition;
    }
}
