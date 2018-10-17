using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactiveRotateSpeed = 100, activeRotateSpeed = 300;
    [SerializeField]
    private float inactiveScale = 1, activeScale = 1.5f;
    [SerializeField]

    private Color inactiveColor, activeColor;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioScource;

    //use for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioScource = GetComponent<AudioSource>();
        UpdateColor();
    }
    private void Update()
    {
        UpdateRotate();
    }


    private void UpdateColor()
    {
        Color color = inactiveColor;
        if (isActive)
            color = activeColor;
        spriteRenderer.color = color;
        
    }
    private void UpdateScale()
    {
        float scale = inactiveScale;
        if (isActive)
            scale = activeScale;
        transform.localScale = Vector3.one * scale;
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
        UpdateScale();
        UpdateColor();
        //scale and color changed here instead of in Update since this only needs to hapen once and not every frame
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            Debug.Log("Player entered checkpoint");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheck(this);
            audioScource.Play();
        }

    }


}
