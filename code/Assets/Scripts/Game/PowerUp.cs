using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private int _powerUpId;//0-triple shoot 1-speed up 2-shield
    private AudioSource _audio;
    void Start()
    {
        transform.Translate(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0));
        _audio = this.GetComponent<AudioSource>();
        if (_audio == null)
        {
            Debug.Log("Audio is null for power up");
        }
    }

    void Update()
    {
        transform.Translate(_speed * Vector3.down * Time.deltaTime);
        if (transform.position.y < -7.5f) Destroy(this.gameObject);

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_audio.clip, Vector3.zero);
                switch(_powerUpId)
                {
                    case 0:
                        player.toggleTripleShot();
                        break;
                    case 1:
                        player.toggleSpeedUp();
                        break;
                    case 2:
                        player.acttiveShield();
                        break;
                    default:
                        Debug.Log("power up is not implmeneted");
                        break;
                }
                Destroy(this.gameObject);
            }

        }
    }
}
