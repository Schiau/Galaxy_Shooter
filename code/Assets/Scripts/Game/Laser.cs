using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed=7.5f;
    private AudioSource _audio;
    void Start()
    {
        transform.Translate(new Vector3(Random.Range(-0.01f,0.01f),1,0));
        _audio = this.GetComponent<AudioSource>();
        if(_audio==null)
        {
            Debug.Log("Audio is null for enemy");
        }
        _audio.Play(0);
    }
    public void setSpeed(float speed)
    {
        _speed = speed;
    }
    public float getSpeed()
    {
        return _speed;
    }
    void Update()
    {
        transform.Translate(new Vector3(0, 0.5f, 0) * _speed * Time.deltaTime);
        if(this.transform.position.y>=7.5f)
        {
            if (this.transform.parent != null)
                Destroy(this.transform.parent.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.tag=="Enemy_laser" && other.tag=="Player")
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player==null)
            {
                Debug.Log("player is null");
            }
            player.takeDamage(1);
            this.transform.parent.gameObject.SetActive(false);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
