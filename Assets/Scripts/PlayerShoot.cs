using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<Transform> InArea = new UnityEvent<Transform>();
    [HideInInspector]
    public UnityEvent OutArea = new UnityEvent();

    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private PlayerMovement _player;

    private SpriteRenderer _shootAreaSprite;
    private Color _defaultColorArea;
    private Joystick _joystick;
    private Transform _enemyTransform;

    private Button _shootButton;
    private Button _reloadButton;
    private TMP_Text _ammoText;

    private int _currentClip = 15, _maxClip = 15, _currentAmmo = 30, _maxAmmo = 100;

    // private bool _isEnemyLock = false;

    private void Start()
    {
        _shootButton = GameObject.Find("ShootButton").GetComponent<Button>();
        _reloadButton = GameObject.Find("ReloadButton").GetComponent<Button>();
        _ammoText = GameObject.Find("AmmoAmountText").GetComponent<TMP_Text>();

        _shootButton.onClick.AddListener(ShootBullet);
        _reloadButton.onClick.AddListener(ReloadGun);

        _joystick = FindAnyObjectByType<Joystick>();
        _shootAreaSprite = GetComponent<SpriteRenderer>();
        _defaultColorArea = _shootAreaSprite.color;

        UpdateAmmoText();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*if (_currentClip > 0)
            {
                ShootBullet();
                _currentClip--;
                Debug.Log(_currentClip);
            }*/
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddAmmo(30);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }

    }

    public void ShootBullet()
    {
        if (_currentClip > 0)
        {
            Quaternion quaternion = new Quaternion();

            quaternion.Set(_joystick.JoystickVecLastPosition.x, _joystick.JoystickVecLastPosition.y, 0f, 1);
            GameObject bullet;

            bullet = Instantiate(_bulletPrefab, _bulletPoint.position, Quaternion.identity) as GameObject;
            //bullet.GetComponent<Bullet>().InitializeBullet(new Vector3(direction.x, direction.y, direction.z));
            if (!_player.IsEnemyLock)
            {
                bullet.GetComponent<Bullet>().InitializeBullet(_joystick.JoystickVecLastPosition);
            }
            else
            {
                Vector3 dir = _enemyTransform.position - _bulletPoint.position;
                bullet.GetComponent<Bullet>().InitializeBullet(dir);
            }

            _currentClip--;
            UpdateAmmoText();
            Debug.Log(_currentClip);
        }
        
    }

    private void ReloadGun() 
    {
        int reloadAmount = _maxClip - _currentClip;
        reloadAmount = (_currentAmmo - reloadAmount) >= 0 ? reloadAmount : _currentAmmo;
        _currentClip += reloadAmount;
        _currentAmmo -= reloadAmount;

        UpdateAmmoText();
    }

    public void AddAmmo(int ammoAmount)
    {
        _currentAmmo += ammoAmount;
        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
        UpdateAmmoText();

    }

    private void UpdateAmmoText()
    {
        _ammoText.text = _currentClip + " / " + _currentAmmo;
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            _player.IsEnemyLock = false;
            _shootAreaSprite.color = _defaultColorArea;
            OutArea?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            _player.IsEnemyLock = true;

            _enemyTransform = collision.GetComponent<Transform>();
            _shootAreaSprite.color = new Color(0, 0, 0, 255);
            InArea?.Invoke(_enemyTransform);
        }
    }
    
}
