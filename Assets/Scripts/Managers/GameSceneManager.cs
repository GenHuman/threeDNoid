using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameSceneManager : MonoBehaviour
{
    #region Singleton
    private static GameSceneManager _instance;
    public static GameSceneManager Instance { get { return _instance; } }
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
    public int height;
    public int width;
    public int spaces;

    public float maxBarrierTime = 10;
    public float currentBarrierTime = 0;

    public GameObject barrier;
    public Canvas canvas;
    public GameObject[] Corners;
    GameObject[] Spaces;
    GameObject[] Portals;
    public Vector3 [] cornerCenters;

    public Text lives;

    public Button ExitButton;

    public TextMeshProUGUI yourScore;
    int score = 0;

    public TextMeshProUGUI scoreText;
    public RectTransform scoreTransform;


    void Start()
    {
        scoreText.text = score.ToString();
        cornerCenters = new Vector3[spaces];
        canvas = GetComponentInChildren<Canvas>();
        lives = canvas.transform.Find("LivesNumber").GetComponent<Text>();
        for (int i = 0; i < spaces; i++)  
        {

            BoxCollider bo = Corners[i].GetComponent<BoxCollider>();
            cornerCenters[i] = bo.bounds.center;
            cornerCenters[i].y = 0;

            cornerCenters[i] = Vector3.Cross(cornerCenters[i],Vector3.up).normalized;
            
            if (i == 0) { 
                //cornerCenters[i] = Vector3. cornerCenters[i]
            Debug.DrawLine(cornerCenters[i], Vector3.zero, Color.red, 5000);
                //Debug.Log(bo.name);
            }

            //Debug.Log("Corner" + i + ": " + cornerCenters[i]);
        }
        
    /*        for (int i = 0; i < ballAmount; i++)
            {
                GameObject obj = Instantiate(ballPrefab);
                obj.SetActive(false);
                InactiveBalls.Enqueue(obj);
                obj.GetComponent<BallScript>().activeIndex = i;
            }       */   
        
    }

    public void activateBarrier()
    {
        barrier.SetActive(true);
        currentBarrierTime = maxBarrierTime;
    }

    private void Update()
    {
        if (currentBarrierTime > 0)
        {
            
            currentBarrierTime -= Time.deltaTime;
            if (currentBarrierTime < 0)
            {
                barrier.SetActive(false);
            }
        }

    }

    public void ShowExit()
    {
        yourScore.enabled = true;
        scoreText.alignment = TextAlignmentOptions.Left;
        scoreTransform.anchoredPosition = new Vector3(100f, -200f, 0f);
        
        ExitButton.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public int modifyScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        return score;
    }
    /*public void modifylives(int Amount)
    {
        int lvs = int.Parse(lives.text);
        lvs += Amount;
        lives.text = lvs.ToString();
    }*/

}

