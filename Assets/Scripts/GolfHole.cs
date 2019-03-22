using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("COLLISION");
        if(collider.tag == "Ball") {
            collider.GetComponent<GolfballController>().Hole();
        }
    }
}
