﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactiveRotateSpeed = 100, activeRotateSpeed = 300;
    private bool isActive = false;

    private void Update()
    {
        UpdateRotate();
    }
    private void UpdateRotate()
    {
        float rotationSpeed = inactiveRotateSpeed;
        if (isActive)
            rotationSpeed = activeRotateSpeed;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); 
        //Time.deltaTime will make it so the spin runs smoothly across all platforms instead of relying on your device's framerate
    }
    public void setActive(bool value)
    {
        isActive = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.setCurrentCheck(this);
        }

    }


}
