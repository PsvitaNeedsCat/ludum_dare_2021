using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainCamera = null;

    [SerializeField]
    private GameObject m_tutorialCamera = null;

    public void Play()
    {
        SceneManager.LoadScene("main_scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowTutorial()
    {
        m_tutorialCamera.SetActive(true);

        m_mainCamera.SetActive(false);
    }

    public void ShowMainScreen()
    {
        m_mainCamera.SetActive(true);

        m_tutorialCamera.SetActive(false);
    }
}
