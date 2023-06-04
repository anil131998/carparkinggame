using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private GameObject IdlePlatform;
    [SerializeField] private GameObject CarInsidePlatform;
    [SerializeField] private Image countdownImage;
    [SerializeField] private TMP_Text counddownText;

    private bool reachTargetFront = false;
    private bool reachTargetBack = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TargetBack")
        {
            reachTargetBack = true;
            checkIfCarReachedGoal();
        }
        if (other.gameObject.tag == "TargetFront")
        {
            reachTargetFront = true;
            checkIfCarReachedGoal();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TargetBack")
        {
            reachTargetBack = false;
        }
        if (other.gameObject.tag == "TargetFront")
        {
            reachTargetFront = false;
        }
    }

    private void checkIfCarReachedGoal()
    {
        if(reachTargetBack && reachTargetFront)
        {
            StartCoroutine(Countdown());
        }
    }


    private IEnumerator Countdown()
    {
        float duration = 2f; 
        float totalTime = 0;

        IdlePlatform.SetActive(false);
        CarInsidePlatform.SetActive(true);
        
        Debug.Log("Timer Started");
        while (totalTime <= duration)
        {
            if(!(reachTargetBack && reachTargetFront))
            {
                Debug.Log("Car exited from goal");
                IdlePlatform.SetActive(true);
                CarInsidePlatform.SetActive(false);
                countdownImage.fillAmount = 0;
                counddownText.text = "";
                yield break;
            }

            countdownImage.fillAmount = totalTime / duration;
            totalTime += Time.deltaTime;
            counddownText.text = (duration-totalTime).ToString("0.00") + "";
            yield return null;
        }
        Debug.Log("we won");
    }

}
