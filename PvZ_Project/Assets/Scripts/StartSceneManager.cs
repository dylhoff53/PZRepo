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


    private void Start()
    {
        /*
        master = Mathf.Lerp(-80, 10, masterVolumeSlider.value);
        music = Mathf.Lerp(-80, 10, musicVolumeSlider.value);
        sfx = Mathf.Lerp(-80, 10, sfxVolumeSlider.value);
        */
    }
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

    public void setMasterVolumeSlider()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        master = Mathf.Lerp(-80, 10, masterVolumeSlider.value);
        am.UpdateVolume(master, music, sfx);
    }

    public void setMusicVolumeSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        music = Mathf.Lerp(-80, 10, musicVolumeSlider.value);
        am.UpdateVolume(master, music, sfx);
    }

    public void setSFXVolumeSlider()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        sfx = Mathf.Lerp(-80, 10, sfxVolumeSlider.value);
        am.UpdateVolume(master, music, sfx);
    }

    public void UpdateSliderValues()
    {
        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        //am.UpdateVolume(master, music, sfx);
    }

    private void OnEnable()
    {
        UpdateSliderValues();
        cL.Clear();
    }
}
