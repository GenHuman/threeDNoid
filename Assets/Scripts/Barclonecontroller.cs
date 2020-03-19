using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Barclonecontroller : MonoBehaviour
{

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;

    
    Rigidbody _rigidbody;
    GameObject rBox;
    GameObject lBox;


    public Material normalMaterial;
    public Material triggerMaterial;

    public GameObject Clone;

    //bool horizontal = true;

    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;

        Transform[] transformArr = GetComponentsInChildren<Transform>();
        foreach(Transform child in transformArr)
        {
            if (string.Equals(child.name, "RBox"))
            {
                rBox = child.gameObject;
            }else if (string.Equals(child.name, "LBox"))
            {
                lBox = child.gameObject;
            }
        }
        //rBox = GetComponentInChildren<Transform>();

    }

    void FixedUpdate()
    {

        // Calculate how fast we should be moving
        Vector3 targetVelocity = Vector3.zero;
       
             targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        
       
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

            
        

    }

    
    private void OnTriggerStay(Collider other)
    {
        /* Debug.Log(other.transform.name);
         Vector3 v = Vector3.zero;
         Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
         Vector3[] vertex = mesh.vertices;
         */
        //Debug.Log(other.transform.parent.name);
    /*
        if (string.Equals(other.transform.parent.name, "Corners"))
        {
            if (other.bounds.Contains(rBox.GetComponent<BoxCollider>().bounds.center))
            {
                rBox.GetComponent<MeshRenderer>().material = triggerMaterial;
            }
            else
            {
                rBox.GetComponent<MeshRenderer>().material = normalMaterial;
            }

            if (other.bounds.Contains(lBox.GetComponent<BoxCollider>().bounds.center))
            {
                lBox.GetComponent<MeshRenderer>().material = triggerMaterial;
            }
            else
            {
                lBox.GetComponent<MeshRenderer>().material = normalMaterial;
            }
        }*/
        /*if(string.Equals(other.transform.parent.name, "Spaces"))
        {
            if (string.Equals(other.name, "S1") || string.Equals(other.name, "S3"))
            {
                //Debug.Log("S1/S3");
                horizontal = true;
            }
            else
            {
                //Debug.Log("S2/S4");
                horizontal = false;
            }
        }
        if (string.Equals(other.transform.parent.name, "Portals"))
        {
            if (string.Equals(other.name, "S1") || string.Equals(other.name, "S3"))
            {
                //Debug.Log("S1/S3");
                horizontal = true;
            }
            else
            {
                //Debug.Log("S2/S4");
                horizontal = false;
            }
        }*/


        //v = otherbox.bounds;
    }

    void collidedpospos()
    {
        Debug.Log("pospos");

    }
    void collidednegpos()
    {
        Debug.Log("negpos");

    }
    void collidednegneg()
    {
        Debug.Log("negneg");

    }
    void collidedposneg()
    {
        Debug.Log("posneg");

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit "+ other.transform.name);
        if (other.transform.name == "cornerxz")
        {
            Debug.Log("cornerxz");
        }
        else if (other.transform.name == "corner-xz")
        {
            Debug.Log("corner-xz");
        }
        else if(other.transform.name == "corner-x-z")
        {
            Debug.Log("corner-x-z");
        }
        else if (other.transform.name == "cornerx-z")
        {
            Debug.Log("cornerx-z");
        }
        else
        {

        }
            //Debug.Log("aaa");
        //other.gameObject.SendMessage("collided" + name);
    }
}

