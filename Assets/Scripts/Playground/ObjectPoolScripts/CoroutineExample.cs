using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineExample : MonoBehaviour
{
    Text timeText;

    IEnumerator tmp;

    private void Start()
    {
        timeText = GetComponent<Text>();
        // tmp = CountStuff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (tmp != null) { StopCoroutine(tmp); }

            tmp = CountStuff();

            StartCoroutine(tmp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopCoroutine(tmp);
        }
    }

    IEnumerator CountStuff()
    {
        float startTimer = Time.time;

        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            ++counter;

            timeText.text = "counting: " + counter;

            if (Time.time - startTimer > 10f) { break; }
        }
    }
}
