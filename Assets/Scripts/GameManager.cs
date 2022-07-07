using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public float level;
    public float score;
    public int stage;
    public int maxStage;
    List<GameObject> balls;
    public float totalTimer = 0f;
    public float timeLeft;
    public float timeToAddCompletedWave;

    public GameObject endPanel;
    public GameObject win;
    public GameObject lost;
    public TextMeshProUGUI totalTime;
    public Button replay;

    public GameObject remainingTime;
    private TextMeshProUGUI _remainingTimeText;

    private bool _gameStarted;

    public GameObject combo;
    private TextMeshProUGUI _comboText;

    public int comboCounter = 0;
    public float comboTimer = 3f;
    private float _timeSinceLastHit = 0f;

    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        balls = GameObject.FindGameObjectsWithTag("Score").ToList();

        foreach (var ball in balls)
        {
            ScoreBall scoreBall = ball.GetComponent<ScoreBall>();
            if (scoreBall.activeStage != stage)
            {
                scoreBall.spawned = false;
                ball.transform.localScale = Vector3.zero;
            }
        }

        _remainingTimeText = remainingTime.GetComponent<TextMeshProUGUI>();
        _comboText = combo.GetComponent<TextMeshProUGUI>();
        remainingTime.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStarted)
        {
            if (stage == maxStage)
            {
                PlayerWon();
            }
            else if (timeLeft <= 0)
            {
                PlayerLost();
            }
            else
            {

                CheckCombo();

                totalTimer += Time.deltaTime;

                bool ballsRemaining = false;

                foreach (var scoreBall in balls)
                {
                    if (scoreBall != null && scoreBall.GetComponent<ScoreBall>().activeStage == stage)
                    {
                        ballsRemaining = true;
                    }
                }

                if (!ballsRemaining)
                {
                    stage++;
                    SpawnNewBalls();
                }

                timeLeft -= Time.deltaTime;
            }

            _remainingTimeText.text = "" + timeLeft.ToString("F1");
            _comboText.text = "Combo: " + comboCounter;

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void SpawnNewBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] == null)
            {
                balls.RemoveAt(i);
            }
        }

        foreach (var ball in balls)
        {
            if (ball != null)
            {
                ScoreBall scoreBall = ball.GetComponent<ScoreBall>();
                if (scoreBall.activeStage == stage)
                {
                    scoreBall.spawned = true;
                    ball.transform.DOScale(Vector3.one * 3, 0.15f).SetEase(Ease.OutBack);
                }
            }
        }

        timeLeft += timeToAddCompletedWave;
        _remainingTimeText.transform.DORotate(new Vector3(0, 0, 360), 0.225f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        _remainingTimeText.transform.DOPunchScale(Vector3.one * 0.8f, 0.15f, 3, 1);
    }

    public void StartGame()
    {
        stage++;
        _gameStarted = true;
        _remainingTimeText.transform.DOShakePosition(80, 10, 2, 10, false, false);
        SpawnNewBalls();
        MusicController.current.StartPlaying();
    }

    private void PlayerWon()
    {
        Cursor.lockState = CursorLockMode.None;
        _gameStarted = false;
        endPanel.SetActive(true);
        win.SetActive(true);
        lost.SetActive(false);
        totalTime.text = "You played for a total of " + totalTimer.ToString("F") + "seconds";
        remainingTime.SetActive(false);
        Time.timeScale = 0;
        //Debug.Log("You won");
    }

    private void PlayerLost()
    {
        Cursor.lockState = CursorLockMode.None;
        _gameStarted = false;
        endPanel.SetActive(true);
        win.SetActive(false);
        lost.SetActive(true);
        totalTime.text = "You played for a total of " + totalTimer.ToString("F") + "seconds";
        remainingTime.SetActive(false);
        Time.timeScale = 0;
        //Debug.Log("You lost");
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BallHit()
    {
        _timeSinceLastHit = comboTimer;
        comboCounter++;
        _comboText.transform.DORotate(new Vector3(0, 0, 360), 0.175f * UnityEngine.Random.Range(0.9f, 1.1f), RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
        _comboText.transform.DOPunchScale(Vector3.one * (0.7f * UnityEngine.Random.Range(0.8f, 1f)), 0.175f, 3, 1);
    }

    private void CheckCombo()
    {
        if (_timeSinceLastHit <= 0f)
        {
            comboCounter = 0;
        }
        _timeSinceLastHit -= Time.deltaTime;
    }
}
