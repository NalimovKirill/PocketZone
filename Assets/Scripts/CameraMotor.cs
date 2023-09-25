using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform _lookAt;
    [SerializeField] private float _boundX;
    [SerializeField] private float _boundY;

    private void Start()
    {
        _lookAt = FindAnyObjectByType<PlayerGeneral>().transform;
    }
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = _lookAt.position.x - transform.position.x;
        if (deltaX > _boundX || deltaX < - _boundX)
        {
            if (transform.position.x < _lookAt.position.x)
            {
                delta.x = deltaX - _boundX;
            }
            else
            {
                delta.x = deltaX + _boundX;
            }
        }

        float deltaY = _lookAt.position.y - transform.position.y;
        if (deltaY > _boundY || deltaY < - _boundY)
        {
            if (transform.position.y < _lookAt.position.y)
            {
                delta.y = deltaY - _boundY;
            }
            else
            {
                delta.y = deltaY + _boundY;
            }
        }
        
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
