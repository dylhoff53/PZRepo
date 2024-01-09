using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject stuff;

    public float counter;
    public bool clicked = false;
    public float muti;
    public int sceneToMove;

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            counter -= muti * Time.deltaTime;
            stuff.GetComponent<CanvasGroup>().alpha = counter;
            if (counter <= 0f)
            {
                Invoke("Change", 1.5f);
            }
        }

    }

    public void GotClicked()
    {
        clicked = true;
    }

    public void Change()
    {
        SceneManager.LoadScene(sceneToMove);
    }
}
