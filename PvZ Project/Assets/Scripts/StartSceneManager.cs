using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class StartSceneManager : MonoBehaviour
{
    public GameObject stuff;
    public GameObject optionsMenu;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public AudioManager am;
    public GameObject mainMenu;
    public CurrentLoadout cL;
    public GameObject selectionMenu;

    public float counter;
    public bool clicked = false;
    public float muti;
    public int sceneToMove;

    public float master;
    public float music;
    public float sfx;


    // Update is called once per frame
    void Update()
    {
        master = Mathf.Lerp(-80, 10, masterVolumeSlider.value);
        music = Mathf.Lerp(-80, 10, musicVolumeSlider.value);
        sfx = Mathf.Lerp(-80, 10, sfxVolumeSlider.value);
        am.UpdateVolume(master, music, sfx);

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

    public void OpenSelectionMenu()
    {
        selectionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Change()
    {
        cL.LockItIn();
        SceneManager.LoadScene(sceneToMove);
    }

    public void PressedOptionsButton()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
