using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfballController : MonoBehaviour
{
    Vector2 initialMousePoint, endMousePoint;
    bool putting = true;
    int hits = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            initialMousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(initialMousePoint);
        }
        else if (Input.GetMouseButtonUp(0)) {
            endMousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(endMousePoint);
            Vector3 velocity = endMousePoint - initialMousePoint;
            if(putting) {
                velocity = new Vector3(velocity.x, 0f, velocity.y);
            }
            else {
                velocity = new Vector3(velocity.x, 0.5f, velocity.y);
            }
            velocity*=40;
            GetComponent<Rigidbody>().velocity = velocity;
            ++hits;
        }
    }
}
