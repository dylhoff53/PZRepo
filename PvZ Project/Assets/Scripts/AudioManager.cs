using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static float volume;
    public float inputVolume;
    public static Vector3 location;
    FMOD.Studio.VCA master;
    FMOD.Studio.VCA music;
    FMOD.Studio.VCA sfx;

    [SerializeField]
    [Range(-80f, 10f)]
    private float masterVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float musicVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float sfxVolume;

    private void Awake()
    {
        location = this.transform.position;

    }
    private void Start()
    {
        master = RuntimeManager.GetVCA("vca:/Master");
        music = RuntimeManager.GetVCA("vca:/Music");
        sfx = RuntimeManager.GetVCA("vca:/SFX");
    }
    private void Update()
    {
        master.setVolume(DecibelToLinear(masterVolume));
        music.setVolume(DecibelToLinear(musicVolume));
        sfx.setVolume(DecibelToLinear(sfxVolume));
    }

    public static void PlayOneShot(string path, float volume, Vector3 position = new Vector3())
    {
        try
        {
            PlayOneShot(RuntimeManager.PathToGUID(path), volume, position);
        }
        catch (EventNotFoundException)
        {
            Debug.LogWarning("[FMOD] Event not found: " + path);
        }
    }

    public static void PlayOneShot(FMOD.GUID guid, float volume, Vector3 position = new Vector3())
    {
        var instance = RuntimeManager.CreateInstance(guid);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        instance.setVolume(volume);
        instance.start();
        instance.release();
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
}
