using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesTextScript : MonoBehaviour
{
    #region Singleton
    private static LivesTextScript _instance;
    public static LivesTextScript Instance { get { return _instance; } }
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
    public Text lives;

    private void Start()
    {
        lives = GetComponent<Text>();
    }

    public int modifylives(int Amount)
    {
        int lvs = int.Parse(lives.text);
        lvs += Amount;
        lives.text = lvs.ToString();
        return lvs;
    }

}
