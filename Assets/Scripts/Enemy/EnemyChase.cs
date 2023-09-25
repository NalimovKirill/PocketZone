using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private EnemyAttackArea _areaToAttack;
    [SerializeField] private LayerMask _playerLayer;
    private NavMeshAgent _agent;
    private float _speed = .2f;
    private Rigidbody2D _rb;
    private Vector3 _startPosition;

    private float _distance;
    private bool _isReadyToAttack = false;
    void Start()
    {
        _player = FindAnyObjectByType<PlayerGeneral>().gameObject;
        _agent = GetComponent<NavMeshAgent>();

        _startPosition = transform.position;


        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.stoppingDistance = 0.5f;

        _agent.isStopped = true;

        //_areaToAttack.EnemyAttack.AddListener(StartAttack);

    }

    // Update is called once per frame
    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);
        _agent.SetDestination(_player.transform.position);

        if (transform.position.x < _player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Physics2D.OverlapCircle(transform.position, 1.5f, _playerLayer))
        {
            _agent.isStopped = false;
        }
        if (_distance > 1.5f)
        {
            _agent.SetDestination(_startPosition);
        }

        

        //Vector2 direction = _player.transform.position - transform.position;
        /*if (_distance < 1.5f)
        {
            _agent.isStopped = true;
            Debug.Log(_agent.remainingDistance);
            //_agent.stoppingDistance = 0.5f;


            // _distance = Vector2.Distance(transform.position, _player.transform.position);
        }*/

        /*_distance = Vector2.Distance(transform.position, _player.transform.position);
            transform.position = Vector2.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime);
            if (_distance <= 0.5f)
            {
                Debug.Log("sdfs");
                transform.position += Vector3.zero;
            }*/

        //Debug.Log(_distance);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1.5f);
    }*/
}
