using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("@@ BUTTONS")]
    public Button playButton;
    public Button tryAgainButton;
    public Button playAgainButton;
    
    [Header("@@ PANELS")]
    public GameObject mainPanel;
    public GameObject levelFailPanel;
    public GameObject youWinPanel;

    [Header("@@ TEXT")]
    public GameObject youWinText;
    public GameObject playAgainText;
    public TextMeshProUGUI scoreText;

    [Header("@@ VARIABLES")]
    public int score;    
    private float timeLeft = 1f;
    public int scoreIncreaseAmount = 1;
    public bool canStartTime = false;
    
    [Header("@@ CONTROLLERS")]
    public BallController BallController; 

    private void Start()
    {
        playButton.onClick.AddListener(playButtonClick);
        tryAgainButton.onClick.AddListener(tryAgainButtonClick);
        playAgainButton.onClick.AddListener(tryAgainButtonClick);
        BallController.StopBallMovement();
    }


    private void OnEnable()
    {
        BallController.onStatus += OnStatus;
        BallController.onWon += OnWon;
        BallController.onJumpArea += OnJumpArea;
        BallController.onStart += OnStart;
        GameManager.onStatus += OnStatus;
    }

    private void OnDisable()
    {
        BallController.onStatus -= OnStatus;
        BallController.onWon -= OnWon;
        BallController.onJumpArea -= OnJumpArea;
        BallController.onStart -= OnStart;
        GameManager.onStatus -= OnStatus;
        
     
    }

    private void OnStart()
    {
        canStartTime = true;
    }

    private void OnJumpArea()
    {
        scoreIncreaseAmount += 1;
    }

    private void OnWon()
    {
        youWinPanel.SetActive(true);
        BallController.StopBallMovement();
        youWinText.transform.DOLocalMove(Vector3.up * 50f, 1f).From();
        canStartTime = false;

    }

    public void playButtonClick()
    {
        
        //Main panel set active false
        // Reset transform of the ball
        mainPanel.SetActive(false);
        BallController.StartBallMovement();
        
        ResetTheScore();


    }

    public void tryAgainButtonClick()
    {
        levelFailPanel.SetActive(false);
        youWinPanel.SetActive(false);
        BallController.StartBallMovement();
        ResetTheScore();
    }
    
    private void OnStatus(GameStatus s)
    {
        if (s == GameStatus.GameOver)
        {
            levelFailPanel.SetActive(true);
            canStartTime = false;
        }

        if (s == GameStatus.InMainMenu)
        {
            mainPanel.SetActive(true);
            levelFailPanel.SetActive(false); 
            youWinPanel.SetActive(false);
            BallController.StopBallMovement();
            canStartTime = false; 
            playButton.transform.DOScale(Vector3.one, .75f).From().SetLoops(-1, LoopType.Yoyo);

        }
    }


    private void Update()
    {
        if (canStartTime)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                IncreaseScore();
                timeLeft = 0.5f;
            }
        }
    }

    void IncreaseScore()
    {
        score += scoreIncreaseAmount;
        scoreText.text =  score.ToString();
    }

    private void ResetTheScore()
    {
        scoreIncreaseAmount = 1;
        score = 0;
        scoreText.text = score.ToString();
    }


}
