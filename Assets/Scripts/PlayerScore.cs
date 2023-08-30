using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private GameObject IdlePlatform;
    [SerializeField] private GameObject CarInsidePlatform;
    [SerializeField] private Image countdownImage;
    [SerializeField] private TMP_Text counddownText;

    //[SerializeField] private GamePlayManager gamePlayManager;

    public delegate void onGameWon();
    public delegate void onGameOver();
    public static event onGameWon gameWon;
    public static event onGameOver gameOver;

    private Rigidbody carRB;

    private bool reachTargetFront = false;
    private bool reachTargetBack = false;

    private void Awake()
    {
        carRB = GetComponent<Rigidbody>();
    }

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
    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;
        if(layer == 3 || layer == 7 || layer == 10 || layer == 11)
        {
            //gamePlayManager.GameOver();
            gameOver?.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void checkIfCarReachedGoal()
    {
        if (reachTargetBack && reachTargetFront)
        {
            StartCoroutine(Countdown());
        }
    }


    private IEnumerator Countdown()
    {
        float duration = 4f; 
        float totalTime = 0;

        IdlePlatform.SetActive(false);
        CarInsidePlatform.SetActive(true);
        
        //Debug.Log("Timer Started");
        while (totalTime <= duration)
        {
            Vector3 velocity = carRB.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            //Debug.Log(localVel.magnitude);

            if (!(reachTargetBack && reachTargetFront) && Mathf.Abs(localVel.magnitude) > 0.1f)
            {
                //Debug.Log("Car exited from goal");
                IdlePlatform.SetActive(true);
                CarInsidePlatform.SetActive(false);
                countdownImage.fillAmount = 0;
                counddownText.text = "";
                yield break;
            }

            countdownImage.fillAmount = totalTime / duration;
            totalTime += Time.deltaTime;
            counddownText.text = Mathf.Clamp(duration-totalTime, 0, duration).ToString("0.00") + "";
            yield return null;
        }

        //gamePlayManager.GameWon();
        gameWon?.Invoke();
    }

}
