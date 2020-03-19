using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Barcontroller : MonoBehaviour
{

    public float speed = 10000.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public float minSize = 0.25f;
    public float size = 0.5f;
    public float maxSize = 5.0f;

    int currentSpace = 1;


    Rigidbody _rigidbody;
    GameObject rBox;
    GameObject lBox;
    MeshRenderer mesh;
    BoxCollider boxCollider;


    public Material normalMaterial;
    public Material triggerMaterial;

    public GameObject clone;

    //bool horizontal = true;
    bool collidingLeft = false;
    bool collidingRight = false;

    BallManager ballManager;

    GameObject ball;
    BallScript bs;

    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        boxCollider = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();

        Transform[] transformArr = GetComponentsInChildren<Transform>();
        foreach (Transform child in transformArr)
        {
            if (string.Equals(child.name, "RBox"))
            {
                rBox = child.gameObject;
            }
            else if (string.Equals(child.name, "LBox"))
            {
                lBox = child.gameObject;
            }
        }

        clone = GameObject.Find("Clone");
        //rBox = GetComponentInChildren<Transform>();

    }
    private void Start()
    {
        ballManager = BallManager.Instance;

    }

    void FixedUpdate()
    {

        // Calculate how fast we should be moving
        Vector3 targetVelocity = Vector3.zero;
        /*if (horizontal)
        {*/
        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        /*}
        else
        {
             targetVelocity = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        }*/
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        //Debug.Log(targetVelocity);
        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);


        if (ballManager.ActiveBalls.Count == 0 && ballManager.lives > 0)
        {
            //Debug.Log("No active balls");
            ball = ballManager.ActivateBall();
            if (ball != null)
            {
                bs = ball.GetComponent<BallScript>();
                bs.bar = this.transform;
                bs.follow = true;
                TimerText.Instance.stopTimer();
            }
        }


    }

    private void Update()
    {
        if (Input.GetButtonDown("FireBall") && bs.follow)
        {
            bs.follow = false;
            bs.direction = (ball.transform.position - transform.position).normalized;
            //Debug.DrawLine(ball.transform.position, transform.position, Color.red,500);
            ball.GetComponent<Rigidbody>().AddForce(bs.direction, ForceMode.VelocityChange);
            TimerText.Instance.startTimer();
            //Debug.Log(ball.GetComponent<Rigidbody>().velocity);
        }
        if (Input.GetButtonDown("DebugButton1"))
        {
            Debug.Log("Debug button1");
            size = (size + 0.25f > maxSize) ? maxSize : size + 0.25f;
           

        }
        else if (Input.GetButtonDown("DebugButton2"))
        {
            Debug.Log("Debug button2");

            size = (size - 0.25f < minSize) ? minSize : size - 0.25f;
        }
        else if (Input.GetButtonDown("DebugButton8"))
        {
            Debug.Log("Debug button8");
            GameSceneManager.Instance.activateBarrier();
            //reduceBalls();
        }
    }


    void OnTriggerStay(Collider other)
    {
        /* Debug.Log(other.transform.name);
         Vector3 v = Vector3.zero;
         Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
         Vector3[] vertex = mesh.vertices;
         */
        //Debug.Log(other.transform.parent.name);
        if (other.transform.parent != null)
            if (string.Equals(other.transform.parent.name, "Corners"))
            {
                switch (currentSpace)
                {
                    case 1:
                        if (other.bounds.Contains(transform.position + new Vector3(size / 2, 0, 0)))
                        {
                            collidingRight = true;
                        }
                        else
                        {
                            collidingRight = false;
                        }

                        if (other.bounds.Contains(transform.position - new Vector3(size / 2, 0, 0)))
                        {
                            collidingLeft = true;
                        }
                        else
                        {
                            collidingLeft = false;
                        }
                        break;
                    case 2:
                        if (other.bounds.Contains(transform.position + new Vector3(0, 0, size / 2)))
                        {
                            collidingRight = true;
                        }
                        else
                        {
                            collidingRight = false;
                        }

                        if (other.bounds.Contains(transform.position - new Vector3(0, 0, size / 2)))
                        {
                            collidingLeft = true;
                        }
                        else
                        {
                            collidingLeft = false;
                        }
                        break;
                    case 3:
                        if (other.bounds.Contains(transform.position - new Vector3(size / 2, 0, 0)))
                        {
                            collidingRight = true;
                        }
                        else
                        {
                            collidingRight = false;
                        }

                        if (other.bounds.Contains(transform.position + new Vector3(size / 2, 0, 0)))
                        {
                            collidingLeft = true;
                        }
                        else
                        {
                            collidingLeft = false;
                        }
                        break;
                    case 4:
                        if (other.bounds.Contains(transform.position - new Vector3(0, 0, size / 2)))
                        {
                            collidingRight = true;
                        }
                        else
                        {
                            collidingRight = false;
                        }

                        if (other.bounds.Contains(transform.position + new Vector3(0, 0, size / 2)))
                        {
                            collidingLeft = true;
                        }
                        else
                        {
                            collidingLeft = false;
                        }
                        break;
                }
                Debug.Log(collidingLeft + " " + collidingRight);
            }
            else if (string.Equals(other.transform.parent.name, "Spaces"))
            {
                switch (other.transform.name)
                {
                    case "S1":
                        currentSpace = 1;
                        break;
                    case "S2":
                        currentSpace = 2;
                        break;
                    case "S3":
                        currentSpace = 3;
                        break;
                    case "S4":
                        currentSpace = 4;
                        break;
                }
            }
        /*if(string.Equals(other.transform.parent.name, "Spaces"))
        {
            if (string.Equals(other.name, "S1") || string.Equals(other.name, "S3"))
            {
                Debug.Log("S1/S3");
                horizontal = true;
            }
            else
            {
                Debug.Log("S2/S4");
                horizontal = false;
            }
        }*/



        //v = otherbox.bounds;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameSceneManager sm = GameSceneManager.Instance;
        if (other.transform.parent != null)
            if (string.Equals(other.transform.parent.name, "Portals"))
            {
                clone.transform.localScale = transform.localScale;
                clone.SetActive(true);
                if (string.Equals(other.name, "P1"))
                {
                    Debug.Log("P1");
                    //calculo de colision para el desplazamiento del clon
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.right * (size / 2));
                    //Debug.DrawRay(transform.position, Vector3.right * (size / 2),Color.green,5000);
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance*2;
                    }
                    
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[0]);
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z - displaceDistance);
                    clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);

                }
                else if (string.Equals(other.name, "P2"))
                {
                    Debug.Log("P2");
                    
                    //calculo de colision para el desplazamiento del clon
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.back * (size / 2));
                    //Debug.DrawRay(transform.position, Vector3.back * (size / 2), Color.green, 5000);
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }

                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[0]);
                    clone.transform.position = new Vector3(clone.transform.position.x + displaceDistance, clone.transform.position.y, clone.transform.position.z);
                    clone.transform.eulerAngles = new Vector3(0f, 0f, 0f);

                }
                else if (string.Equals(other.name, "P3"))
                {
                    Debug.Log("P3");

                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.forward * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[1]);
                    clone.transform.position = new Vector3(clone.transform.position.x + displaceDistance, clone.transform.position.y, clone.transform.position.z);
                    clone.transform.eulerAngles = new Vector3(0f, -180f, 0f);

                }
                else if (string.Equals(other.name, "P4"))
                {
                    Debug.Log("P4");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.right * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[1]);
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z + displaceDistance);
                    clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);

                }
                else if (string.Equals(other.name, "P5"))
                {
                    Debug.Log("P5");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.left * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[2]);
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z + displaceDistance);
                    clone.transform.eulerAngles = new Vector3(0f, -270f, 0f);
                }
                else if (string.Equals(other.name, "P6"))
                {
                    Debug.Log("P6");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.forward * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[2]);
                    clone.transform.position = new Vector3(clone.transform.position.x - displaceDistance, clone.transform.position.y, clone.transform.position.z);
                    clone.transform.eulerAngles = new Vector3(0f, -180f, 0f);
                }
                else if (string.Equals(other.name, "P7"))
                {
                    Debug.Log("P7");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.back * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[3]);
                    clone.transform.position = new Vector3(clone.transform.position.x - displaceDistance, clone.transform.position.y, clone.transform.position.z);
                    clone.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                else if (string.Equals(other.name, "P8"))
                {
                    Debug.Log("P8");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.left * (size / 2));
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }
                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[3]);
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z - displaceDistance);
                    clone.transform.eulerAngles = new Vector3(0f, -270f, 0f);
                }

            }
        if (string.Equals(other.tag, "Capsule"))
        {
            //size = size + 0.25f;
            //transform.localScale = new Vector3(size, transform.localScale.y, transform.localScale.z);
            int type = other.GetComponent<CapsuleScript>().type;
            Debug.Log(type);
            switch (type)
            {
                case 1:
                    size = (size + 0.25f > maxSize) ? maxSize : size + 0.25f;
                    transform.localScale = new Vector3(size, transform.localScale.y, transform.localScale.z);
                    break;
                case 2:
                    size = (size - 0.25f < minSize) ? minSize : size - 0.25f;
                    transform.localScale = new Vector3(size, transform.localScale.y, transform.localScale.z);
                    break;
                case 3:
                    ballManager.enlargeBalls();
                    break;
                case 4:
                    ballManager.reduceBalls();
                    break;
                case 5:
                    ballManager.dupeBalls();
                    break;
                case 6:
                    ballManager.tripleBalls();
                    break;
                case 7:
                    ballManager.modifyLife(1);
                    break;
                case 8:
                    GameSceneManager.Instance.activateBarrier();
                    break;

            }
            GameSceneManager.Instance.modifyScore(100);
            other.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null)
            if (string.Equals(other.transform.parent.name, "Portals"))
            {
                Debug.Log("P exit");

                if (clone != null)
                {

                    if (collidingLeft && collidingRight)
                    {
                        transform.position = clone.transform.position;
                        transform.rotation = clone.transform.rotation;
                    }
                    clone.SetActive(false);
                    //clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y+999, clone.transform.position.z);
                }
            }
    }


}

