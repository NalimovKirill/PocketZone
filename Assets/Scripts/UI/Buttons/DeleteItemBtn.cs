using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteItemBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action ClickOnDropBtn;
    public void OnPointerDown(PointerEventData eventData)
    {
        ClickOnDropBtn?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

}
