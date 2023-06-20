using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject difficultyMenu;
    public GameObject creditsMenu;

    public AudioSource clickSFX;
    public AudioSource startSFX;

    private void Start()
    {
        mainMenu.SetActive(true);
        difficultyMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void PlayClick()
    {
        clickSFX.Play();
    }

    public void PlayStart()
    {
        startSFX.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChooseDifficulty()
    {
        mainMenu.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void ViewCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        difficultyMenu.SetActive(false);
    }

    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }

    public void ChooseEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        StartCoroutine(LoadGame());
    }

    public void ChooseMedium()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        StartCoroutine(LoadGame());
    }

    public void ChooseHard()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        StartCoroutine(LoadGame());
    }
}
