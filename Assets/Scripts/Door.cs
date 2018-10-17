using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //this method doesn't work well since the playerChar has two colliders, so it triggers the event twice
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Player"))
    //    {
    //        if(Input.GetButtonDown("Activate"))
    //        {
    //            Debug.Log("Player activated door");
    //        }
    //    }
    //}
    [SerializeField]
    private string sceneToLoad;

    private bool playerInTrig;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInTrig = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrig = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate")&&playerInTrig)
        {
            Debug.Log("Player activated door");
            SceneManager.LoadScene(sceneToLoad);
        }
    }



}
