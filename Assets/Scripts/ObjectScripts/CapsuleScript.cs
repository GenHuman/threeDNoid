using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    // Start is called before the first frame update
    /*int type_enlarge_bar = 1;
      type_reduce_bar = 2
    int type_enlarge_ball = 3;
    type_diminish_ball = 4;
    int type_dupe_ball = 5;
    int type_triple_ball = 6;
    int type_extra_life = 7;
    int type_invulnerable = 8;*/
    public int type;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(other.transform.name, "Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
