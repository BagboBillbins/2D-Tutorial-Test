using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    
    
    private void OnTriggerEnter2D(Collider2D collision)
      
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered hazard");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Respawn(); //respawn player at checkpoint
            
        }
        else
            Debug.Log("Something has hit a hazard");
        
        
    }

    //hard collision so you don't check the trigger in the collider
    //private void OnCollisionEnter2D(Collision2D collision)
    
    
}
