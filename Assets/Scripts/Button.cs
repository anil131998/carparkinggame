using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool _state = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _state = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _state = false;
    }
}
