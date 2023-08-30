using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gearTxt;
    
    private void ChangeGear(string gear)
    {
        gearTxt.text = gear;
    }

    private void OnEnable()
    {
        PlayerController.onGearChanged += ChangeGear;
    }
    private void OnDisable()
    {
        PlayerController.onGearChanged -= ChangeGear;
    }


}
