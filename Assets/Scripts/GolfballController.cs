using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GolfballController : MonoBehaviour
{
    public Transform[] startingPoints;
    public CameraController camera;
    public Image clubImage;
    public Sprite putter, iron;
    public TextMeshProUGUI score;
    Vector2 initialMousePoint, endMousePoint;
    int levelIndex = 1;
    bool putting = false, held = false;
    int strokes = 0;

    void Start() {
        transform.position =  startingPoints[levelIndex].position;
    }
    
    void Update()
    {
        bool clubswap = Input.GetButtonDown("Club");
        if(clubswap) {
            putting = !putting;
            if(putting) {
                clubImage.sprite = putter;
            }
            else {
                clubImage.sprite = iron;
            }
        }
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
        bool isSteady = (currentVelocity.magnitude < 0.7f);
        Debug.Log(currentVelocity.magnitude);
        if (Input.GetMouseButtonDown(0) && isSteady) {
            held = true;
            initialMousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(initialMousePoint);
        }
        else if (Input.GetMouseButtonUp(0) && held) {
            held = false;
            camera.LockToBall();
            endMousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(endMousePoint);
            Vector3 velocity = endMousePoint - initialMousePoint;
            if(putting) {
                velocity = new Vector3(velocity.x, 0f, velocity.y);
            }
            else {
                velocity = new Vector3(velocity.x, 0.4f, velocity.y);
            }
            velocity*=60;
            GetComponent<Rigidbody>().velocity = velocity;
            ++strokes;
            score.text = "Strokes: " + strokes;
        }
    
        if(transform.position.y < -40) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = startingPoints[levelIndex].position;
        }
    }

    public void Hole() {
        ++levelIndex;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position =  startingPoints[levelIndex].position;
    }
}
