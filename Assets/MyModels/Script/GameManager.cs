using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carrotHolders;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerText;
    public GameObject panel;
    public SongHandler sh;
    public GameObject buttons;
    void Start()
    {
        setupCarrots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel.activeInHierarchy)
            {
                panel.SetActive(true);
                sh.enabled = false;
                buttons.SetActive(false);
            }
            
        }
    }
    public void pause()
    {
        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
            sh.enabled = false;
            buttons.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void updateScore(int value)
    {
        GetComponent<AudioSource>().Play();
        score += value;
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    void setupCarrots()
    {
        carrotHolders.SetActive(true);
        foreach(Transform carrotPair in carrotHolders.transform)
        {
            int value = Random.Range(0, 2);
            carrotPair.gameObject.transform.GetChild(value).gameObject.SetActive(false);
            int scaleNum = (value == 0) ? 1 : 0;
            if (Random.Range(0,10) == 5)
            {
                Debug.Log("here");
                carrotPair.name = "bigCarrots";
                carrotPair.gameObject.transform.GetChild(scaleNum).localScale *= 3;
                carrotPair.gameObject.transform.GetChild(scaleNum).position = new Vector3(carrotPair.gameObject.transform.GetChild(value).position.x, 4.03000021f, carrotPair.gameObject.transform.GetChild(value).position.z);

            }

        }
    }
    public void removeCarrot(GameObject obj)
    {
        StartCoroutine(afterCollision(obj));
    }
    public IEnumerator afterCollision(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(30);
        obj.transform.parent.transform.GetChild(Random.Range(0, 2)).gameObject.SetActive(true);
        Debug.Log("hereee");
    }
    public void playButton()
    {
        playerText.text = "Resume";
        panel.SetActive(false);
    }
}
