using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage = 20;
    private float _speed = 10f;
    private Vector3 _shootDirection;
    private Joystick _joystick;


    private void Start()
    {
        _joystick = FindAnyObjectByType<Joystick>();
        Destroy(gameObject, 5.0f);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.ApplyDamage(_damage);

            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (_shootDirection == null)
        {
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }
        else
        {
            transform.position += (_shootDirection * Time.deltaTime * _speed);
        }
    }

    public void InitializeBullet(Vector3 originalDirection)
    {
        _shootDirection = originalDirection;
    }
}
