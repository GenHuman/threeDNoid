using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float translateTime;
    float t = 0;
    bool movingRight = false;
    bool movingLeft = false;
    //float degrees = 0;
    Camera _camera;

    Vector3 finalRotEuler;

    void Start()
    {
        //transform.LookAt(new Vector3(0f, 2.5f, 0f));
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t >= 0.65)
        {
            t = 0;
            movingLeft = false;
            movingRight = false;

        }

        if (Input.GetButtonDown("MoveCameraRight") && !movingRight && !movingLeft)
        {
            finalRotEuler = transform.rotation.eulerAngles;
            finalRotEuler.y -= 90;
            movingRight = true;
            //transform.Translate(new Vector3(1, 0, 1).normalized * Vector3.Distance(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0)));
            /*transform.position = Vector3.Slerp(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0),t);
            transform.LookAt(new Vector3(0, 2.5f, 0));*/
        }else
        if (Input.GetButtonDown("MoveCameraLeft") && !movingRight && !movingLeft)
        {
            movingLeft = true;
            finalRotEuler = transform.rotation.eulerAngles;
            finalRotEuler.y += 90;
            /*transform.Translate(new Vector3(-1, 0, 1).normalized * Vector3.Distance(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0)));
            transform.LookAt(new Vector3(0, 2.5f, 0));*/
        }

        if (movingLeft)
        {
            t += Time.deltaTime / translateTime;
            //transform.Translate(new Vector3(-1, 0, 1).normalized * Vector3.Distance(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0)));       
            //Debug.Log(t);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finalRotEuler), t);
            //_camera.transform.LookAt(new Vector3(0, 2.5f, 0));
            //transform.LookAt(new Vector3(0, 2.5f, 0));
        }
        else if(movingRight)
        {

            t += Time.deltaTime / translateTime;
            //transform.Translate(new Vector3(-1, 0, 1).normalized * Vector3.Distance(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0)));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finalRotEuler), t);
            //_camera.transform.LookAt(new Vector3(0, 2.5f, 0));
            //transform.LookAt(new Vector3(0, 2.5f, 0));
            //transform.position = Vector3.Slerp(new Vector3(0, 2.5f, -20), new Vector3(20, 2.5f, 0), t*8);
            //transform.LookAt(new Vector3(0, 2.5f, 0));
        }
       
    }
}
