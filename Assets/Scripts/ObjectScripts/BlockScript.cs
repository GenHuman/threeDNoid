using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    public int amount;
    public int maxHits = 1;
    public float hits = 0;

    public Vector3 direction = Vector3.zero;
    public int type;
    public GameObject capsulePrefab;
    int currentSpeed;
    int maxSpeed;
    Renderer rend;
    public Material popupMat;
    public Material currentMaterial;

    public float popupTime = 0;
    public bool popupActive = false;
 

    void Start()
    {

        //rend = GetComponent<Renderer>();
        //rend.material.SetFloat("Hits", hits);
        //rend.material.SetFloat("MaxHits", maxHits);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
       
    }

    private void OnEnable()
    {
        if(rend==null)
        rend = GetComponent<MeshRenderer>();
        popupTime = 0;
        popupActive = true;
        currentMaterial = rend.material;
        rend.material = popupMat;
        rend.material.SetFloat("Time",popupTime);
    }

    void Update()
    {
        /*if (follow)
        {
            transform.position = new Vector3(bar.position.x , bar.position.y + (bar.localScale.y * 1.5f), bar.position.z);
        }
        else{
            
        }*/
        
        if (popupActive)
        {
            popupTime += Time.deltaTime;
            rend.material.SetFloat("Time", popupTime);
            if (popupTime >= 1)
            {
                //rend.material = currentMaterial;
                popupActive = false;      
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        hits++;
        //rend = GetComponent<Renderer>();
        //rend.material.SetFloat("Hits", hits);
        if (string.Equals(collision.collider.tag, "Floor"))
        {
            BlockManager.Instance.DeactivateBlock(gameObject);
        }
        if (hits >= maxHits)
        {
            BlockManager.Instance.DeactivateBlock(gameObject);
            GameSceneManager.Instance.modifyScore(10);
            if (type > 0)
            {
                GameSceneManager.Instance.modifyScore(10);
                Quaternion r = new Quaternion();
                r.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 90);
                GameObject obj = Instantiate(capsulePrefab, transform.position, r);
                //obj.transform.position = transform.position;
                obj.GetComponent<Rigidbody>().AddForce(Vector3.down * 6, ForceMode.Force);
                obj.GetComponent<MeshRenderer>().material = BlockManager.Instance.capsuleMaterials[type];
                obj.GetComponent<CapsuleScript>().type = type;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(other.tag, "Floor"))
        {
            //BlockManager.Instance.DeactivateBlock(gameObject);
            BallManager.Instance.modifyLife(-BallManager.Instance.lives);
        }


    }

}
