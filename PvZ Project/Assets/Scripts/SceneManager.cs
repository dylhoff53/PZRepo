using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private bool gameEnded = false;
    public bool Died;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if(Died)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
    }
}
