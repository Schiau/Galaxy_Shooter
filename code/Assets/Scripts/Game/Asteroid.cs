using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private GameObject _explosionAnimation;
    private EnemySpawner _enemySpawner;
    private AudioSource _audio;
    void Start()
    {
        _rotationSpeed = 3f;
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        if(_enemySpawner==null)
        {
            Debug.Log("spawner is null");
        }
        _audio = this.GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.Log("Audio is null for enemy");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(GetComponent<Collider2D>());
            _audio.Play();
            GameObject explosion=Instantiate(_explosionAnimation, this.transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 1.25f);
            _enemySpawner.StartSpawning();
            Destroy(explosion, 1.5f);
        }
    }
    void Update()
    {
        this.transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }
}
