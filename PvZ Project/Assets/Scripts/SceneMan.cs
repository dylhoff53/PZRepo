using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public bool gameEnded = false;
    public GameObject winText;
    public GameObject loseText;
    public bool died;
    public bool win;
    public CanvasGroup blue;
    public CanvasGroup red;
    private float alphaCounter = 0;
    public float endingTime;
    public float incomeTimer;
    public int passiveIncomeValue;
    public float incomeGenerationTime;

    private void Start()
    {
        Debug.Log("THis is a test!!!");
    }
    // Update is called once per frame
    void Update()
    {
        if(gameEnded && win)
        {
            blue.alpha += Time.deltaTime * (1f / endingTime);
        } else if(gameEnded && died)
        {
            red.alpha += Time.deltaTime * (1f / endingTime);
        }

        if(died && !gameEnded)
        {
            BadEnd();
        } else if (win && !gameEnded)
        {
            GoodEnd();
        } else if(!gameEnded)
        {
            incomeTimer += Time.deltaTime;
            if(incomeTimer >= incomeGenerationTime)
            {
                incomeTimer = 0f;
                PlayerStats.Money += passiveIncomeValue;
            }
        }
    }


    public void BadEnd()
    {
        gameEnded = true;
        loseText.SetActive(true);
        red.gameObject.SetActive(true);
        Debug.Log("Game Over!");
        Invoke("SwitchScene", endingTime);
    }

    public void GoodEnd()
    {
        gameEnded = true;
        winText.SetActive(true);
        blue.gameObject.SetActive(true);
        Debug.Log("You Win!");
        Invoke("SwitchScene", endingTime);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }
}
