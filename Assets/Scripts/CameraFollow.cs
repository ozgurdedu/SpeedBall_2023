using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // takip edilecek obje
    public float smoothSpeed = 0.125f; // takip işleminin smoothluğu
    public Vector3 offset; // kameranın hedef objeden olan konumu


    private void Start()
    {
        offset = transform.position - target.position; 
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z); // kameranın gitmek istediği konum
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // smooth hareket
        transform.position = smoothedPosition;

        transform.LookAt(target); // kameranın hedef objeyi izlemesi
    }
}
