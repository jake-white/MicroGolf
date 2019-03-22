using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform golfball;
    bool locked;
    
    void Update()
    {
        if(locked) {
            transform.position = new Vector3(golfball.position.x, transform.position.y, golfball.position.z);
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(horizontal != 0 || vertical != 0) {
            locked = false;
            Vector3 newPosition = transform.position;
            newPosition.x += 0.2f * horizontal;
            newPosition.z += 0.2f * vertical;
            transform.position = newPosition;
        }
    }

    public void LockToBall() {
        locked = true;
    }
}
