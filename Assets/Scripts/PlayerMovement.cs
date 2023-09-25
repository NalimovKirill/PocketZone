using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    private Joystick _joystick;
    [SerializeField] private PlayerShoot _areaShoot;
    [SerializeField] private Transform _bodyPlayer;
    [SerializeField] private Transform _gunTransform;

    private BoxCollider2D _boxCollider;
    private RaycastHit2D _hit;

    private bool _isEnemyLock = false;
    private Vector3 _directionToEnemy;
    public Vector3 DirectionToEnemy { get { return _directionToEnemy;}}
    public bool IsEnemyLock { get { return _isEnemyLock; } set { _isEnemyLock = value; } }
    private void Start()
    {
       _joystick = FindAnyObjectByType<Joystick>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _areaShoot.InArea.AddListener(RotateGunToEnemy);
        _areaShoot.OutArea.AddListener(RotateGunDefault);
    }
    
    private void FixedUpdate()
    {
        Move();

        if (!_isEnemyLock)
        {
           RotateGunDefault();
        }
    }

    private void Move()
    {
        if (_joystick.JoystikVec != Vector2.zero)
        {
            //transform.Translate(0, _joystick.JoystickVecLastPosition.y * Time.deltaTime, 0);
            _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, _joystick.JoystickVecLastPosition.y),
                Mathf.Abs(_joystick.JoystickVecLastPosition.y * Time.deltaTime),LayerMask.GetMask("Actor","Blocking"));
            if (_hit.collider == null)
            {
                transform.Translate(0,_joystick.JoystickVecLastPosition.y * Time.deltaTime,0);
            }

            _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(_joystick.JoystickVecLastPosition.x,0),
                Mathf.Abs(_joystick.JoystickVecLastPosition.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
            if (_hit.collider == null)
            {
                transform.Translate(_joystick.JoystickVecLastPosition.x * Time.deltaTime, 0, 0);
            }


            if (_joystick.JoystickVecLastPosition.x < 0)
            {
                _bodyPlayer.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _bodyPlayer.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void RotateGunToEnemy(Transform enemyTransform)
    {
        //_isEnemyLock = true;
        if (_isEnemyLock)
        {
            _directionToEnemy = enemyTransform.position - _gunTransform.position;
            float angle = Mathf.Atan2(_directionToEnemy.y, _directionToEnemy.x) * Mathf.Rad2Deg;
            _gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            FlipGunSprite();
        }
    }

    private void RotateGunDefault()
    {
        Vector3 dir = _joystick.JoystickVecLastPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        FlipGunSprite();
    }

    private void FlipGunSprite()
    {
        if (_gunTransform.rotation.z > 0.7)
        {
            _gunTransform.localScale = new Vector3(1, -1, 1);
        }
        else if (_gunTransform.rotation.z > -0.7)
        {
            _gunTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (_gunTransform.rotation.z < -0.7)
        {
            _gunTransform.localScale = new Vector3(1, -1, 1);
        }
    }
}
