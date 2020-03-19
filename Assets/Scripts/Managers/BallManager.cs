using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Singleton
    private static BallManager _instance;
    public static BallManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    /*[System.Serializable]
    public class BallPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }*/
    public int ballAmount = 50;
    public GameObject ballPrefab;

    public List<GameObject> ActiveBalls = new List<GameObject>();
    public Queue<GameObject> InactiveBalls = new Queue<GameObject>();
    public int queueSize = 50;
    public int lives = 3;



    void Start()
    {
            for (int i = 0; i < ballAmount; i++)
            {
                GameObject obj = Instantiate(ballPrefab);
                obj.SetActive(false);
                InactiveBalls.Enqueue(obj);
                obj.GetComponent<BallScript>().activeIndex = i;
            }          
        
    }

    public GameObject ActivateBall()
    {
        GameObject ball = InactiveBalls.Dequeue();
        ActiveBalls.Add(ball);
       
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.SetActive(true);

        queueSize = InactiveBalls.Count;
        return ball;

    }

    public GameObject[] ActivateBalls(int amount)
    {
        GameObject[] ballArray = new GameObject[amount];
        for(int i = 0; i < amount; i++)
        {
            GameObject ball = InactiveBalls.Dequeue();
            ActiveBalls.Add(ball);
            ball.SetActive(true);
            ballArray[i] = ball;
        }
        queueSize = InactiveBalls.Count;

        return ballArray;

    }

    public void DeactivateBall(GameObject ball, bool isDrop)
    {
        ActiveBalls.Remove(ball);
        InactiveBalls.Enqueue(ball);
        ball.SetActive(false);
        if (isDrop && ActiveBalls.Count == 0) modifyLife(-1);
        queueSize = InactiveBalls.Count;
    }

    public void dupeBalls()
    {

        int ballsToActivate = (ActiveBalls.Count * 2 > ballAmount) ? ballAmount - ActiveBalls.Count : ActiveBalls.Count;
        if (ActiveBalls.Count != 1 || !ActiveBalls[0].GetComponent<BallScript>().follow)
        {
            GameObject[] balls = ActivateBalls(ballsToActivate);
            for (int i = 0; i < ballsToActivate; i++)
            {
                if (!ActiveBalls[i].GetComponent<BallScript>().follow)
                {
                    BallScript ballsc = ActiveBalls[i].GetComponent<BallScript>();

                    Rigidbody ballrb = ActiveBalls[i].GetComponent<Rigidbody>();
                    Rigidbody ballrb2 = balls[i].GetComponent<Rigidbody>();
                    Vector3 position = ActiveBalls[i].transform.position;
                    Vector3 velocity = ballrb.velocity;

                    balls[i].transform.position = ActiveBalls[i].transform.position;
                    balls[i].transform.rotation = ActiveBalls[i].transform.rotation;
                    float displacement = ballsc.size / 2;

                    int a = ballsc.currentSpace;
                    Debug.Log("Case " + a);
                    switch (a)
                    {
                        case 1:
                            ActiveBalls[i].transform.position = new Vector3(position.x - displacement, position.y, position.z);
                            balls[i].transform.position = new Vector3(position.x + displacement, position.y, position.z);
                            ballrb.velocity = Quaternion.AngleAxis(45, Vector3.forward) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.forward) * velocity;
                            break;
                        case 2:
                            ActiveBalls[i].transform.position = new Vector3(position.x, position.y, position.z - displacement);
                            balls[i].transform.position = new Vector3(position.x, position.y, position.z + displacement);
                            ballrb.velocity = Quaternion.AngleAxis(45, Vector3.left) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.left) * velocity;
                            break;
                        case 3:
                            ActiveBalls[i].transform.position = new Vector3(position.x + displacement, position.y, position.z);
                            balls[i].transform.position = new Vector3(position.x - displacement, position.y, position.z);
                            ballrb.velocity = Quaternion.AngleAxis(45, Vector3.back) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.back) * velocity;
                            break;
                        case 4:
                            ActiveBalls[i].transform.position = new Vector3(position.x, position.y, position.z + displacement);
                            balls[i].transform.position = new Vector3(position.x, position.y, position.z - displacement);
                            ballrb.velocity = Quaternion.AngleAxis(45, Vector3.right) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.right) * velocity;
                            break;
                    }
                }
                else
                {
                    DeactivateBall(balls[i], false);
                }
                //balls[i] = 
                //balls[i];
            }
        }
    }

    public void tripleBalls()
    {
        int ballsToActivate = (ActiveBalls.Count * 3 > ballAmount) ? ballAmount - ActiveBalls.Count : ActiveBalls.Count*2;
        int ballsActiveOld = ActiveBalls.Count<ballsToActivate? ActiveBalls.Count : ballsToActivate;
        
        if (ActiveBalls.Count != 1 || !ActiveBalls[0].GetComponent<BallScript>().follow)
        {
            GameObject[] balls = ActivateBalls(ballsToActivate);
            int ballsActivated = balls.Length;
            for (int i = 0; i < ballsActiveOld; i++)
            {
                if (!ActiveBalls[i].GetComponent<BallScript>().follow)
                {
                    BallScript ballsc = ActiveBalls[i].GetComponent<BallScript>();

                    Rigidbody ballrb = ActiveBalls[i].GetComponent<Rigidbody>();
                    Debug.Log(i+ " ballsActiveOld" + ballsActiveOld +" ballstoactivate" +ballsToActivate);
                    Rigidbody ballrb2 = balls[i].GetComponent<Rigidbody>();
                    Debug.Log("notCreash");
                    Rigidbody ballrb3 = balls[ballsActivated - 1 - i].GetComponent<Rigidbody>();
                    Vector3 position = ActiveBalls[i].transform.position;
                    Vector3 velocity = ballrb.velocity;

                    balls[ballsActivated - 1 - i].transform.position = ActiveBalls[i].transform.position;
                    balls[ballsActivated - 1 - i].transform.rotation = ActiveBalls[i].transform.rotation;
                    balls[i].transform.position = ActiveBalls[i].transform.position;
                    balls[i].transform.rotation = ActiveBalls[i].transform.rotation;

                    float displacement = ballsc.size;

                    int a = ballsc.currentSpace;
                    Debug.Log("Case " + a);
                    switch (a)
                    {
                        case 1:
                            balls[ballsActivated - 1 - i].transform.position = new Vector3(position.x - displacement, position.y, position.z);
                            balls[i].transform.position = new Vector3(position.x + displacement, position.y, position.z);
                            ballrb3.velocity = Quaternion.AngleAxis(45, Vector3.forward) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.forward) * velocity;
                            break;
                        case 2:
                            balls[ballsActivated - 1 - i].transform.position = new Vector3(position.x, position.y, position.z - displacement);
                            balls[i].transform.position = new Vector3(position.x, position.y, position.z + displacement);
                            ballrb3.velocity = Quaternion.AngleAxis(45, Vector3.left) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.left) * velocity;
                            break;
                        case 3:
                            balls[ballsActivated - 1 - i].transform.position = new Vector3(position.x + displacement, position.y, position.z);
                            balls[i].transform.position = new Vector3(position.x - displacement, position.y, position.z);
                            ballrb3.velocity = Quaternion.AngleAxis(45, Vector3.back) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.back) * velocity;
                            break;
                        case 4:
                            balls[ballsActivated - 1 - i].transform.position = new Vector3(position.x, position.y, position.z + displacement);
                            balls[i].transform.position = new Vector3(position.x, position.y, position.z - displacement);
                            ballrb3.velocity = Quaternion.AngleAxis(45, Vector3.right) * velocity;
                            ballrb2.velocity = Quaternion.AngleAxis(-45, Vector3.right) * velocity;
                            break;
                    }
                }
                else
                {
                    DeactivateBall(balls[i], false);
                }
                //balls[i] = 
                //balls[i];
            }
        }
    }
    public void enlargeBalls()
    {
        modifyBallSize(true);
    }
    public void reduceBalls()
    {
        modifyBallSize(false);
    }

    public int modifyLife(int value)
    {
        lives += value;
        LivesTextScript.Instance.modifylives(value);
        if (lives == 0)
        {
            TimerText.Instance.stopTimer();
            GameSceneManager.Instance.ShowExit();
        }
        return lives;
    }

    public void modifyBallSize(bool add)
    {
        GameObject[] copyQueue = InactiveBalls.ToArray();
        for(int i = 0; i < copyQueue.Length; i++)
        {
            copyQueue[i].GetComponent<BallScript>().modifySize(add);
        }
        InactiveBalls = new Queue<GameObject>(copyQueue);
        for (int i = 0; i < ActiveBalls.Count; i++)
        {
            ActiveBalls[i].GetComponent<BallScript>().modifySize(add);
        }
        
    }

    private void Update()
    {
        //Debug.Log("Debug button");
        if (Input.GetButtonDown("DebugButton1"))
        {
            Debug.Log("Debug button1");
            
        }
        else if (Input.GetButtonDown("DebugButton2"))
        {
            Debug.Log("Debug button2");
            
        }
        else if (Input.GetButtonDown("DebugButton3"))
        {
            Debug.Log("Debug button3");
            enlargeBalls();
        }
        else if (Input.GetButtonDown("DebugButton4"))
        {
            Debug.Log("Debug button4");
            reduceBalls();
        }
        else if (Input.GetButtonDown("DebugButton5"))
        {
            Debug.Log("Debug button5");
            dupeBalls();
        }
        else if (Input.GetButtonDown("DebugButton6"))
        {
            Debug.Log("Debug button6");
            tripleBalls();
        }
        else if (Input.GetButtonDown("DebugButton7"))
        {
            Debug.Log("Debug button7");
            modifyLife(1);
        }
        else if (Input.GetButtonDown("DebugButton8"))
        {
            //Debug.Log("Debug button8");
            //reduceBalls();
        }
        else if (Input.GetButtonDown("DebugButton9"))
        {
           // Debug.Log("Debug button9");
            //reduceBalls();
        }
        else if (Input.GetButtonDown("DebugButton0"))
        {
            Debug.Log("Debug button0");
            //reduceBalls();
        }
    }

}

