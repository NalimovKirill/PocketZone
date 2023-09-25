using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneral : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private int _currentHealthPlayer = 100;
    [SerializeField] private int _maxHealthPlayer = 100;
    public int Health { get { return _currentHealthPlayer; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damage)
    {
        _currentHealthPlayer -= damage;
        
        if (_currentHealthPlayer <= 0)
        {
            Destroy(gameObject);
        }
        UpdateHealthBar(_currentHealthPlayer);
    }

    public void AddHealth(int value)
    {
        _currentHealthPlayer += value;
        if (_currentHealthPlayer > _maxHealthPlayer)
        {
            _currentHealthPlayer = _maxHealthPlayer;
        }
        UpdateHealthBar(_currentHealthPlayer);
    }

    private void UpdateHealthBar(int value)
    {
        /*if (value < 0) 
        {
            _healthPlayer -= value;
        }
        else
        {
            _healthPlayer += value;
        }*/
        _healthBar.fillAmount = value / 100f;
    }
}
