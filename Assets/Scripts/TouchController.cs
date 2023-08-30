using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchController : MonoBehaviour
{

    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    private float horizontalInput;
    private float verticalInput;

    public delegate void UpdateTouchInput(float hz, float vt);
    public static event UpdateTouchInput updateTouchInput;

    private void Update()
    {
        updateInput();
        updateTouchInput?.Invoke(horizontalInput, verticalInput);
    }

    private void updateInput()
    {
        if (upButton.GetComponent<Button>()._state)
        {
            verticalInput = 1;
        }
        else if (downButton.GetComponent<Button>()._state)
        {
            verticalInput = -1;
        }
        else
        {
            verticalInput = 0;
        }

        if (rightButton.GetComponent<Button>()._state)
        {
            horizontalInput = 1;
        }
        else if (leftButton.GetComponent<Button>()._state)
        {
            horizontalInput = -1;
        }
        else
        {
            horizontalInput = 0;
        }
    }

    
}
