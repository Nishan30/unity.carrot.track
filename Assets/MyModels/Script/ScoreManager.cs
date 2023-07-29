using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(transform.localScale.x > 3)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().updateScore(8);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().updateScore(3);
            }
            
            GameObject.Find("GameManager").GetComponent<GameManager>().removeCarrot(this.gameObject);
            
        }
    }

}
