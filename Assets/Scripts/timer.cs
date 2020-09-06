using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int timeToLose;
    public TextMeshProUGUI countDownText;
    public GameObject winLose;
    public GameObject restartButton;

    private TextMeshProUGUI winLoseText;


    // Start is called before the first frame update
    void Start()
    {
        winLoseText = winLose.GetComponent<TextMeshProUGUI>();
        countDownText.text = "Time Left: " + timeToLose;
        StartCoroutine("CountDown");
        restartButton.SetActive(false);
    }

    private IEnumerator CountDown() {
        while (timeToLose > 0) {
            yield return new WaitForSeconds(1);
            timeToLose--;
            countDownText.text = "Time Left: " + timeToLose;
        }
        winLoseText.text = "You ran out of time!";
        winLose.SetActive(true);
        restartButton.SetActive(true);
    }

    public void StopCountDown() {
        StopCoroutine("CountDown");
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
