using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private Laser _laserRight, _laserLeft;
    private float _speed;
    void Start()
    {
        _speed = _laserRight.getSpeed();
        _laserRight.setSpeed(-_speed);
        _laserLeft.setSpeed(-_speed);
    }
    public void Delete()
    {
        Destroy(_laserLeft.gameObject);
        Destroy(_laserRight.gameObject);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (this.transform.position.y <= -6.5f)
            Delete();
    }
}
