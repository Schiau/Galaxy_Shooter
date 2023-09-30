using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestScoreText;
    [SerializeField]
    private Image _liveDisplay;
    [SerializeField]
    private List<Sprite> _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _resetText;
    private bool _playerIsDead;
    [SerializeField]
    private Sprite _musicOff,_musicOn;
    [SerializeField]
    private AudioSource _backgroundSound;
    private bool _isMusicOn=true;
    [SerializeField]
    private Button _buttonMute;
    [SerializeField]
    private GameObject _instructionsPanel; 
    void Start()
    {
        _playerIsDead = false;
        _resetText.gameObject.SetActive(_playerIsDead);
        _gameOverText.gameObject.SetActive(_playerIsDead);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
    public void UpdateBestScore(int score)
    {
        _bestScoreText.text = "Best: " + score;
    }
    public void UpdateLives(int currentLives)
    {
        _liveDisplay.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            EndGame();
        }
    }
    public bool PlayerIsDead()
    {
        return _playerIsDead;
    }
    private void EndGame()
    {
        _playerIsDead = true;
        StartCoroutine(GameOverEnabel());
        _resetText.gameObject.SetActive(_playerIsDead);
    }

    IEnumerator GameOverEnabel()
    {
        _gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f); 
        StartCoroutine(GameOverDisable());

    }
    IEnumerator GameOverDisable()
    {
        _gameOverText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f); 
        StartCoroutine(GameOverEnabel());
    }
    public void ResumeGame()
    {
        GameMannager gameMannager= GameObject.Find("Game_Mannager").GetComponent<GameMannager>();
        if (gameMannager == null)
            Debug.Log("gamme mannager is NULL");
        gameMannager.ResumeGame();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ToggleMusic()
    {
        if(_isMusicOn)
        {
            _isMusicOn = false;
            _backgroundSound.mute = true;
            _buttonMute.image.sprite = _musicOff;
        }
        else
        {
            _backgroundSound.mute = false;
            _isMusicOn = true;
            _buttonMute.image.sprite = _musicOn;
        }
    }
    public void CloseInstruction()
    {
        Time.timeScale = 1;
        _instructionsPanel.gameObject.SetActive(false);
    }
    public void OpenInstructions()
    {
        Time.timeScale = 0;
        _instructionsPanel.gameObject.SetActive(true);
    }
}
