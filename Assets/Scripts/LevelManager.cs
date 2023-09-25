using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private GameObject _playerPrefab;
    private PlayerShoot _playerShoot;

    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _reloadButton;
    void Awake()
    {
        /*Instantiate(_playerPrefab, _spawnPlace);
        _playerShoot = GetComponent<PlayerShoot>();*/
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
