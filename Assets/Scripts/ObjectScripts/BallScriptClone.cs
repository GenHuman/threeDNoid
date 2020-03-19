using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScriptClone : MonoBehaviour
{

    Rigidbody _rigidbody;
    public Transform bar;
    public bool follow = false;
    public int activeIndex = -1;


    public Vector3 direction = Vector3.zero;
    int currentSpeed;
    int maxSpeed;
    //bool collided = false;
 

    void Start()
    {
        //BallScript clonescript = clone.GetComponent<BallScript>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;


    }

    void FixedUpdate()
    {
        if (follow)
        {
    
            transform.position = new Vector3(bar.position.x , bar.position.y + (bar.localScale.y * 1.5f), bar.position.z);
        }
        else{
            
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
    /*private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Boing!");
        collided = true;
        
    }*/

   

   /* private void OnTriggerEnter(Collider other)
    {

        if (string.Equals(other.transform.parent.name, "Portals"))
        {
            if (string.Equals(other.name, "P1"))
            {
                Debug.Log("P1");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(-transform.position.z, transform.position.y, -transform.position.x - transform.localScale.x);
                clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);

            }
            else if (string.Equals(other.name, "P2"))
            {
                Debug.Log("P2");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(-transform.position.z + transform.localScale.x, transform.position.y, -transform.position.x);
                clone.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            }
            else if (string.Equals(other.name, "P3"))
            {
                Debug.Log("P3");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(transform.position.z + transform.localScale.x, transform.position.y, transform.position.x);
                clone.transform.eulerAngles = new Vector3(0f, -180f, 0f);

            }
            else if (string.Equals(other.name, "P4"))
            {
                Debug.Log("P4");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(transform.position.z, transform.position.y, transform.position.x + transform.localScale.x);
                clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);

            }
            else if (string.Equals(other.name, "P5"))
            {
                Debug.Log("P5");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(-transform.position.z, transform.position.y, -transform.position.x + transform.localScale.x);
                clone.transform.eulerAngles = new Vector3(0f, -270f, 0f);
            }
            else if (string.Equals(other.name, "P6"))
            {
                Debug.Log("P6");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(-transform.position.z - transform.localScale.x, transform.position.y, -transform.position.x);
                clone.transform.eulerAngles = new Vector3(0f, -180f, 0f);
            }
            else if (string.Equals(other.name, "P7"))
            {
                Debug.Log("P7");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(transform.position.z - transform.localScale.x, transform.position.y, transform.position.x);
                clone.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (string.Equals(other.name, "P8"))
            {
                Debug.Log("P8");
                //Instantiate(clone);
                clone.SetActive(true);
                clone.transform.position = new Vector3(transform.position.z, transform.position.y, transform.position.x - transform.localScale.x);
                clone.transform.eulerAngles = new Vector3(0f, -270f, 0f);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isClone)
        if (string.Equals(other.transform.parent.name, "Portals"))
        {
            Debug.Log("P exit");

            if (clone != null)
            {

                if (!string.Equals(other.transform.parent.name,"Corners"))
                {
                    transform.position = clone.transform.position;
                    transform.rotation = clone.transform.rotation;
                }
                clone.SetActive(false);
            }
        }
    }*/
}
