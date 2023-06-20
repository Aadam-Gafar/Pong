using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    public AudioSource clickSFX;
    public AudioSource startSFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        clickSFX.Play();
    }

    public void PlayStart()
    {
        startSFX.Play();
    }
}
