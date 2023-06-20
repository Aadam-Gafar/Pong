using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject difficultyMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        difficultyMenu.SetActive(false);
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

    public void ChooseEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        SceneManager.LoadScene("Game");
    }

    public void ChooseMedium()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        SceneManager.LoadScene("Game");
    }

    public void ChooseHard()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        SceneManager.LoadScene("Game");
    }
}
