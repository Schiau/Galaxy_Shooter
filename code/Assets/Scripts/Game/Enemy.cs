using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private Player _player;
    private Animator _animator;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _onDeathAudio;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.5f;
    void movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.5f)
        {
            transform.position = new Vector3(Random.Range(-11f, 11f), -transform.position.y, 0);
        }
    }
    private void Death()
    {
        Destroy(GetComponent<Collider2D>());
        AudioSource.PlayClipAtPoint(_audio.clip, Vector3.zero);
        _animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 0.3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.addScore(10);
            }
            Destroy(other.gameObject);
            Death();
        }
        if (other.tag.CompareTo("Player") == 0)
        {
            Player playerComponent = other.transform.GetComponent<Player>();
            if (playerComponent != null) playerComponent.takeDamage(1);
            Death();
        }
    }
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is null");
        }
        _animator = this.GetComponent<Animator>();
        if (_player == null)
        {
            Debug.Log("Animator is null");
        }
        _audio = this.GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.Log("Audio is null for laser");
        }
        else
        {
            if (_onDeathAudio == null)
            {
                Debug.Log("Sound for the enemy death is null");
            }
            else
                _audio.clip = _onDeathAudio;
        }
    }
    void Update()
    {
        if(_canFire<Time.time)
        {
            _fireRate = Random.Range(2.5f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laser, transform.position, Quaternion.identity);
        }
        movement();
    }
}
