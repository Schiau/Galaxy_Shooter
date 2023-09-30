using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private GameObject _ammo;
    [SerializeField]
    private GameObject _tripleShoot;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.5f;
    [SerializeField]
    private int _lives = 3;
    private EnemySpawner _enemySpawner;
    private bool _isTripleShoot = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _bestScore;
    private UI_Manager _UIManager;
    [SerializeField]
    private GameObject _rightEngineDamge;
    [SerializeField]
    private GameObject _leftEngineDamge;
    [SerializeField]
    private GameObject _animPlayerExplosion;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _onDeathAudio;
    void Start()
    {
        Time.timeScale = 1;
        _score = 0;
        _bestScore = PlayerPrefs.GetInt("bestScore", 0);
        _shieldVisualizer.SetActive(false);
        transform.position = new Vector3(0, 0, 0);
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        if (_enemySpawner == null)
        {
            Debug.LogError("enemy spawner is NULL");
        }
        _UIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if (_UIManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        _UIManager.UpdateBestScore(_bestScore);
        _audio = this.GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.Log("Audio is null for player");
        }
    }
    public void toggleTripleShot()
    {
        _isTripleShoot = true;
        StartCoroutine(tripleShot());
    }
    IEnumerator tripleShot()
    {
        yield return new WaitForSeconds(5);
        _isTripleShoot = false;
    }
    public void toggleSpeedUp()
    {
        _speed += 5.0f;
        StartCoroutine(speedUp());
    }
    IEnumerator speedUp()
    {
        yield return new WaitForSeconds(5);
        _speed -= 5.0f;
    }
    public void acttiveShield()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    public bool isShieldActive()
    {
        if (_isShieldActive)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return true;
        }
        return false;
    }
    void InputUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveVector = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(moveVector * _speed * Time.deltaTime);
    }
    public void takeDamage(int amountOfDamageTaken)
    {
        if (!isShieldActive())
        {
            _lives -= amountOfDamageTaken;
            if (_lives <= 0) _lives = 0;
            else
            {
                if (_leftEngineDamge.activeSelf == false && _rightEngineDamge.activeSelf == false)
                {
                    if (Random.Range(0, 2) == 1)
                        _leftEngineDamge.SetActive(true);
                    else
                        _rightEngineDamge.SetActive(true);
                }
                else
                {
                    if (_leftEngineDamge.activeSelf == false)
                        _leftEngineDamge.SetActive(true);
                    else
                        _rightEngineDamge.SetActive(true);
                }
            }
            _UIManager.UpdateLives(_lives);
            if (_lives == 0)
            {
                _speed = 0f;
                _leftEngineDamge.SetActive(false);
                _rightEngineDamge.SetActive(false);
                GameObject explosion = Instantiate(_animPlayerExplosion, this.transform.position, Quaternion.identity);
                _enemySpawner.onPlayerDead();
                if (_onDeathAudio == null)
                {
                    Debug.Log("Sound for the enemy death is null");
                }
                else
                {
                    _audio.clip = _onDeathAudio;
                    _audio.volume+=_audio.volume/2%1;
                    _audio.Play(0);
                }
                Destroy(this.gameObject, 1f);
                Destroy(explosion, 2f);
            }
        }
    }
    void LoopAround()
    {
        if (Mathf.Abs(transform.position.x) >= 11.0f)
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        if (Mathf.Abs(transform.position.y) >= 6.5f)
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
    }

    void Movement()
    {
        InputUpdate();
        LoopAround();
    }

    void shootLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShoot)
        {
            Instantiate(_tripleShoot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_ammo, transform.position, Quaternion.identity);
        }
    }
    public void addScore(int score)
    {
        _score += score;
        _UIManager.UpdateScore(_score);
        if (_score > _bestScore)
        {
            _bestScore = _score;
            _UIManager.UpdateBestScore(_bestScore);
            PlayerPrefs.SetInt("bestScore", _bestScore);
        }    
    }
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) shootLaser();
    }
}
