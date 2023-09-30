using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMannager : MonoBehaviour
{
    private UI_Manager _UIManager;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    private Animator _pauseAnimator;
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if (_UIManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        _pauseAnimator = GameObject.Find("PauseMenu_Panel").GetComponent<Animator>();
        if (_pauseAnimator == null)
        {
            Debug.LogError("Pause Animator is NULL");
        }
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void ResetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
    void Update()
    {
        if (_UIManager.PlayerIsDead())
            ResetLevel();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.GetKeyDown(KeyCode.P))
        {
            _pauseAnimator.SetBool("isPause", true);
            _pauseMenuPanel.SetActive(true);
            Time.timeScale=0;
        }
    }
    public void ResumeGame()
    {
        _pauseAnimator.SetBool("isPause", false);
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
