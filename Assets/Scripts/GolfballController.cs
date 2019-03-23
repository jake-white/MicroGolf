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
    public TextMeshPro instructions;
    public LineRenderer line;
    Vector2 initialMousePoint, endMousePoint;
    int levelIndex = 0, lastLevel = 2;
    bool putting = false, held = false;
    int strokes = 0;

    void Start() {
        transform.position =  startingPoints[levelIndex].position;
        line.SetWidth(0.2f, 0.2f);
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
        if(held) {
            Vector3 initialWorld = Camera.main.ViewportToWorldPoint(new Vector3(initialMousePoint.x, initialMousePoint.y, 5));
            Vector3 endWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, new Vector3(initialWorld.x, 20, initialWorld.z));
            line.SetPosition(1, new Vector3(endWorld.x, 20, endWorld.z));
        }
        else {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    
        if(transform.position.y < -40) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = startingPoints[levelIndex].position;
        }
    }

    public void Hole() {
        if(levelIndex < lastLevel) {
            ++levelIndex;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position =  startingPoints[levelIndex].position;            
        }
        else {
            instructions.text = "Game complete! You had " + strokes + " strokes.";
            strokes = 0;
            levelIndex = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position =  startingPoints[levelIndex].position;
        }
    }
}
