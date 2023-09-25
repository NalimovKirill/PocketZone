using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseItemBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action ClickOnUseBtn;
    public void OnPointerDown(PointerEventData eventData)
    {
        ClickOnUseBtn?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

}
