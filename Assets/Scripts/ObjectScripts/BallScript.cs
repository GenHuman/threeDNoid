using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{

    Rigidbody _rigidbody;
    public Transform bar;
    public bool follow = false;
    public int activeIndex = -1;
    public GameObject clone;
    Rigidbody cloneRigidbody;
    BallScriptClone bsc;

    SphereCollider sphereCollider;


    //bool incorner = false;
    bool collidingRight = false;
    bool collidingLeft = false;

    public int currentSpace = 1;

    public float minSize = 0.1f;
    public float size = 0.1f;
    public float maxSize = 1f;

    public Vector3 direction = Vector3.zero;
    public float currentSpeed = 1f;
    public float bounceMultiplier = 1.05f;
    public float maxSpeed = 3f;
   // bool collided = false;
 

    void Start()
    {
        //BallScript clonescript = clone.GetComponent<BallScript>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        clone = Instantiate(clone);
        sphereCollider = GetComponent<SphereCollider>();

        clone.SetActive(false);
        cloneRigidbody = clone.GetComponent<Rigidbody>();

       
        bsc = clone.GetComponent<BallScriptClone>();
        if (follow)
        {
            //Debug.Log("cloning ball position");

            bsc.follow = true;
            bsc.bar = bar.GetComponent<Barcontroller>().clone.transform;
        }
        else
        {
            bsc.follow = false;
            bsc.bar = bar;
        }

    }

    void FixedUpdate()
    {

        
        if (follow)
        {
            bsc.follow = true;
            transform.position = new Vector3(bar.position.x , bar.position.y + 0.05f + (size * 0.6f), bar.position.z);
        }
        else{
            bsc.follow = false;
            _rigidbody.AddForce(_rigidbody.velocity * currentSpeed * Time.deltaTime, ForceMode.Force );
        }

    }

    private void Update()
    {
        /*if (collided) { 
            direction.x = -direction.x;
            direction.y = -direction.y;
            direction.z = -direction.z;
            _rigidbody.AddForce(direction, ForceMode.VelocityChange);
            collided = false;
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Boing!");
        //collided = true;
        if (currentSpeed < maxSpeed)
        {
            //Debug.Log("motto");
            currentSpeed *= 1.05f;
        }

        if (string.Equals(collision.transform.name, "Floor"))
        {
            BallManager.Instance.DeactivateBall(this.gameObject, true);
            /*if (BallManager.Instance.ActiveBalls.Count == 0)
            {
                BallManager.Instance.lives--;
                GameSceneManager.Instance.canvas.transform.Find("LivesNumber").GetComponent<Text>().text = BallManager.Instance.lives.ToString();
            }*/
        }

    }

   

    private void OnTriggerEnter(Collider other)
    {

        
        if (other.transform.parent != null)
        {

            if (string.Equals(other.transform.parent.name, "Corners"))
            {
                //incorner = true;
            }else
            if (string.Equals(other.transform.parent.name, "Portals"))
            {
                //Debug.Log("cloning ball");
                clone.SetActive(true);
                clone.transform.localScale = new Vector3(size, size, size);
                GameSceneManager sm = GameSceneManager.Instance;

                Vector3 speed = _rigidbody.velocity;


                if (string.Equals(other.name, "P1"))
                {
                    //Debug.Log("P1");
                    float displaceDistance = size;
                    Ray ray = new Ray(transform.position, Vector3.right * (size / 2));
                    //Debug.DrawRay(transform.position, Vector3.right * (size / 2),Color.green,5000);
                    RaycastHit rh = new RaycastHit();
                    if (Physics.Raycast(ray, out rh))
                    {
                        displaceDistance = rh.distance * 2;
                    }

                    clone.transform.position = Vector3.Reflect(transform.position, sm.cornerCenters[0]);
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z - displaceDistance);

                    clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed,new Vector3(-1,1,-1)), sm.cornerCenters[0]);

                }
                else if (string.Equals(other.name, "P2"))
                {
                    //Debug.Log("P2");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[0]);
                    //cloneRigidbody.velocity = speed;

                }
                else if (string.Equals(other.name, "P3"))
                {
                    //Debug.Log("P3");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[1]);
                }
                else if (string.Equals(other.name, "P4"))
                {
                    //Debug.Log("P4");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[1]);
                }
                else if (string.Equals(other.name, "P5"))
                {
                    //Debug.Log("P5");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[2]);
                }
                else if (string.Equals(other.name, "P6"))
                {
                    //Debug.Log("P6");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[2]);
                }
                else if (string.Equals(other.name, "P7"))
                {
                    //Debug.Log("P7");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[3]);
                }
                else if (string.Equals(other.name, "P8"))
                {
                    //Debug.Log("P8");
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
                    cloneRigidbody.velocity = Vector3.Reflect(Vector3.Scale(speed, new Vector3(-1, 1, -1)), sm.cornerCenters[3]);
                }

               


            }
            else if (string.Equals(other.transform.name, "Floor"))
            {
                BallManager.Instance.DeactivateBall(this.gameObject, true);
            }
        }
        else if (string.Equals(other.transform.name, "Floor"))
        {
            BallManager.Instance.DeactivateBall(this.gameObject , true);
            /*if (BallManager.Instance.ActiveBalls.Count == 0)
            {
                BallManager.Instance.lives--;
                GameSceneManager.Instance.canvas.transform.Find("LivesNumber").GetComponent<Text>().text = BallManager.Instance.lives.ToString();
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null) { 
            if (string.Equals(other.transform.parent.name, "Portals"))
            {
                //Debug.Log("P exit");

                if (collidingLeft &&  collidingRight && !follow)
                {
                    transform.position = clone.transform.position;
                    transform.rotation = clone.transform.rotation;
                    _rigidbody.velocity = cloneRigidbody.velocity;
                    /*Debug.Log("Exit pos: " + clone.transform.position);
                    Debug.DrawLine(Vector3.zero, cloneRigidbody.velocity,Color.red,5000);
                    Debug.DrawLine(Vector3.zero, (Quaternion.AngleAxis(+45, Vector3.right) * cloneRigidbody.velocity), Color.red, 5000);
                    Debug.DrawLine(Vector3.zero, (Quaternion.AngleAxis(-45, Vector3.right) * cloneRigidbody.velocity), Color.red, 5000);*/
                }
                clone.SetActive(false);

            }
            if (string.Equals(other.transform.parent.name, "Corners"))
            {
               // incorner = false;
            }
        }
        else
        {
            if (string.Equals(other.transform.name, "GameSpace"))
            {
                BallManager.Instance.DeactivateBall(gameObject,true);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.parent !=null)
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
               /* if (other.bounds.Contains(sphereCollider.bounds.center + new Vector3(0.05f, 0, 0)))
            {
                collidingRight = true;
                //rBox.GetComponent<MeshRenderer>().material = triggerMaterial;
            }
            else
            {
                collidingRight = false;
                //rBox.GetComponent<MeshRenderer>().material = normalMaterial;
            }

            if (other.bounds.Contains(sphereCollider.bounds.center - new Vector3(0.05f, 0, 0)))
            {
                collidingLeft = true;
                //lBox.GetComponent<MeshRenderer>().material = triggerMaterial;
            }
            else
            {
                collidingLeft = false;
                //lBox.GetComponent<MeshRenderer>().material = normalMaterial;
            }
            */
        }else if (string.Equals(other.transform.parent.name, "Spaces"))
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

    public void modifySize(bool add)
    {
        if (add)
        {
            size = (size + 0.05f > maxSize) ? maxSize : size + 0.05f;
        }
        else
        {
            size = (size - 0.05f < minSize) ? minSize : size - 0.05f;
        }
        transform.localScale = new Vector3(size, size, size);
    }
}
