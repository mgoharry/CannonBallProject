using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject StartMenu;
    [SerializeField] Animator anim;
    public bool isGameRunning;
    public bool isTitle;


    private void Start()
    {
        isGameRunning = false;
        isTitle = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && ! isTitle)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isGameRunning = false;
            Cursor.lockState = CursorLockMode.None;

        }
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    public void returnToMenu()
    {
        anim.SetBool("StartGame", false);
        PauseMenu.SetActive(false);
        Time.timeScale = 0.5f;
        isGameRunning = false;
        isTitle = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartMenuCor());
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        isGameRunning = true;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void StartGame()
    {
        anim.SetBool("StartGame", true);
        Time.timeScale = 1.0f;
        StartMenu.SetActive(false);
        isGameRunning = true;
        isTitle = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RestartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        

    }
    
    IEnumerator StartMenuCor()
    {
        yield return new WaitForSeconds(0.6f);
        StartMenu.SetActive(true);
    }
}
