using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMan : MonoBehaviour
{
    private bool gameEnded = false;
    public static bool died;
    public static bool win;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if(died)
        {
            BadEnd();
        }
    }

    public void GameEnd()
    {
        if (win)
        {
            GoodEnd();
        } else if (died)
        {
            BadEnd();
        }
    }


    public void BadEnd()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
    }

    public void GoodEnd()
    {
        gameEnded = true;
        Debug.Log("You Win!");
    }
}
