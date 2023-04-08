using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("@@ CONTROLLERS")]
    public BallController BallController; 
    
    [Header("@@ PLANES")]
    public List<PlaneIdentity> planeIdentities = new List<PlaneIdentity>();
    private int _activePlaneIndex;
    
    [Header("@@ ENUM")]
    private GameStatus _status;

    [field: Header("@@ EVENTS")]
    public static event Action<GameStatus> onStatus;  
    
    
    private void Start()
    {
        _status = GameStatus.InMainMenu;
        onStatus?.Invoke(_status);
    }

    private void OnEnable()
    {
        BallController.onJumpArea += OnJumpArea; 
        BallController.onStart += OnStart;

    }
    private void OnDisable()
    {
        BallController.onJumpArea -= OnJumpArea;
        BallController.onStart -= OnStart;

    }
    

    private void OnStart()
    {
        _activePlaneIndex = 0;
        //başka bir çözüm bulacağım.
            planeIdentities[1].gameObject.SetActive(false);
            planeIdentities[2].gameObject.SetActive(false);
        
    }
    private void OnJumpArea()
    {
     
        
        _activePlaneIndex++;
        if (_activePlaneIndex >= planeIdentities.Count)
            return;
        
        planeIdentities[_activePlaneIndex].gameObject.SetActive(true);
        planeIdentities[_activePlaneIndex].transform.DOMove(new Vector3(0,-100f,0), 0.5f).From();
        BallController.Jump(planeIdentities[_activePlaneIndex].targetArea);
        
    }
    
    
    
 
    

   
}
