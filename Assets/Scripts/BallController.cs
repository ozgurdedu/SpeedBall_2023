using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public class BallController : MonoBehaviour
{
    
    [Header("@@ BALL PROPS")]
    public float horizontalSpeed = 3000f;
    public float forwardSpeed = 50f;
    private float xLimit = 1.5f;

    [Header("@@ ENUM")]
    private GameStatus _status;
    
    [field: Header("@@ EVENTS")]
    public static event Action onJumpArea;
    public static event Action<GameStatus> onStatus;
    public static event Action onStart;
    public static event Action onWon;

    public ParticleSystem deadEffect;
    


    private void Update()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        
            // Move for Z axis
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

            // Move for X axis
            float horizontalMove = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
            float newPosition = Mathf.Clamp(transform.position.x + horizontalMove, -xLimit, xLimit);
            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);

        
    }

  
    public void StartBallMovement()
    {
        transform.position = new Vector3(1.5f, 13, -45);
        horizontalSpeed = 750f;
        forwardSpeed = 50f;
        onStart?.Invoke();
        //particleSystem.GetComponent<ParticleSystem>().Stop();
    }

    public void StopBallMovement()
    {
        horizontalSpeed = 0.0f;
        forwardSpeed = 0.0f;
    }

    
    
    
    public void Jump(Transform targetObject)
    {

        gameObject.GetComponent<TrailRenderer>().time = 0.05f;
        gameObject.transform.DOJump(
                targetObject.position + new Vector3(1.5f, 0, 0),
                50f,
                1,
                1f,
                false).SetEase(Ease.InSine)
            .OnUpdate(() => { }) // sağa sola hareket devam etsin.
            .OnComplete(() => { gameObject.GetComponent<TrailRenderer>().time = 0.3f; });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            StopBallMovement();
            onStatus?.Invoke(GameStatus.GameOver);
           
            deadEffect.gameObject.SetActive(true);
            deadEffect.Play();
            // Game over
            // bütün bileşenleri sıfırla
            // panel aç, tıkla ve reset poosition.
        }
    }
    
    [Obsolete("Obsolete")]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpArea"))
        {
            onJumpArea?.Invoke();
            forwardSpeed += 5f;
        }

        if (other.CompareTag("FinishLine"))
        {
            //You win Panel
            // click and reset position.
            onWon?.Invoke();
            deadEffect.gameObject.SetActive(true);
            deadEffect.Play();
            
        }

    }
}