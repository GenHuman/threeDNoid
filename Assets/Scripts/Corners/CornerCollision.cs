using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider boxCollider;
    public string _name ="";
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(name);
        //other.gameObject.SendMessage("collided" + name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("coll "+name);
    }

}
