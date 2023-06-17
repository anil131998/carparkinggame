using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class study : MonoBehaviour
{

    [SerializeField] private GameObject cube1;
    [SerializeField] private GameObject cube2;
    [SerializeField] [Range(50,200)] private float rotateSpeed = 5f;
    [SerializeField] [Range(1,5)] private float duration = 2f;

    public void rotateCubes()
    {
        StartCoroutine(rotate());
    }

    private IEnumerator rotate()
    {
        float timer = 0;

        while (timer < duration)
        {

            cube1.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;

        while (timer < duration)
        {
            cube2.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

    }

}
