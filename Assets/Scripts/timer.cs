using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int timeToLose;
    public TextMeshProUGUI countDownText;
    public GameObject winLose;

    private TextMeshProUGUI winLoseText;


    // Start is called before the first frame update
    void Start()
    {
        winLoseText = winLose.GetComponent<TextMeshProUGUI>();
        countDownText.text = "Time Left: " + timeToLose;
        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown() {
        while (timeToLose > 0) {
            yield return new WaitForSeconds(1);
            timeToLose--;
            countDownText.text = "Time Left: " + timeToLose;
        }
        winLoseText.text = "You ran out of time!";
        winLose.SetActive(true);        
    }

    public void StopCountDown() {
        StopCoroutine("CountDown");
    }
}
