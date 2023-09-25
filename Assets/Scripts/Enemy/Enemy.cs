using Inventory.Model;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _healthEnemy = 100;
    [SerializeField] private Image _healthBar;

    [SerializeField] private GameObject _droppedItem;
    private Vector3 _deathPos;
    
    public void ApplyDamage(int damage)
    {
        _healthEnemy -= damage;
        _healthBar.fillAmount = _healthEnemy / 100f;

        if (_healthEnemy <= 0)
        {
            _deathPos = transform.position;
            if (_droppedItem != null)
            {
                GameObject go = Instantiate(_droppedItem, _deathPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
