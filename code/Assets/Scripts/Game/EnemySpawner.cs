using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyCounteiner;
    [SerializeField]
    private GameObject _powerUpCounteiner;
    [SerializeField]
    private List<GameObject> _powerUps;
    private bool _playerIsAlive = true;
    public void StartSpawning()
    {
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PoweUps_SpawnRoutine());
    }
    
    IEnumerator enemySpawnRoutine()
    {
        while(_playerIsAlive)
        {
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-9f,9f),7,0), Quaternion.identity);
            newEnemy.transform.parent = _enemyCounteiner.transform;
            yield return new WaitForSeconds(Random.Range(1f,5f));
        }
    }
    IEnumerator PoweUps_SpawnRoutine()
    {
        while (_playerIsAlive)
        {
            GameObject randomPowerUp = _powerUps[(int)Random.Range(0, _powerUps.Count)];
            GameObject newPowerUp = Instantiate(randomPowerUp, new Vector3(Random.Range(-9f, 9f), 7, 0), Quaternion.identity);
            newPowerUp.transform.parent = _powerUpCounteiner.transform;
            yield return new WaitForSeconds(Random.Range(4.0f,7.0f));
        }
    }
    public void onPlayerDead()
    {
        _playerIsAlive = false;
        for (int i = 0; i < _enemyCounteiner.transform.childCount; i++)
            Destroy(_enemyCounteiner.transform.GetChild(i).gameObject);
    }
}
