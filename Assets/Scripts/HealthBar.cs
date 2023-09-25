using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    private PlayerGeneral _player;
    private int _healthAmount;
    private void Start()
    {
        _healthAmount = GetComponent<PlayerGeneral>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) 
        {
            UpdateHealthBar(20);
        }*/
    }

    private void UpdateHealthBar(int damage)
    {
        _healthAmount -= damage;
        _healthBar.fillAmount = _healthAmount / 100f;
    }
}
