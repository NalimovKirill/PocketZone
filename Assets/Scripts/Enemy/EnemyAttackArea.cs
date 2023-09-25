using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackArea : MonoBehaviour
{
    private BoxCollider2D _attackArea;
    [SerializeField] private Animator _enemyAnimator;
    private PlayerGeneral _playerGeneral;
    public UnityEvent EnemyAttack = new UnityEvent();

    private float _attackTimer = 0f;
    [SerializeField] private int _attackDamage = 10;
    void Start()
    {
        _attackArea = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerGeneral>(out PlayerGeneral player))
        {
            _playerGeneral = player;
            Debug.Log("јтакуем");
            
            if (_attackTimer >= 0f)
            {
                _attackTimer -= Time.deltaTime;
                if (_attackTimer <= 0f)
                {
                    StartCoroutine(Attacking());
                    _attackTimer = 1f;
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerGeneral>())
        {
            Debug.Log("∆дем");
        }
    }

    IEnumerator Attacking()
    {
        _enemyAnimator.SetTrigger("Attack");
        _playerGeneral.ApplyDamage(_attackDamage);
        yield return new WaitForSeconds(1f);
    }
}
