using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        // When using an IEnumarot use this to start.
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {   
        // Set player False
        PlayerController.instance.gameObject.SetActive(false);

        // Wait for a value to be true, and wait for this amount to end. And when this amount of time
        // Ended, continue!
        yield return new WaitForSeconds(waitToRespawn);

        // Reactivate player
        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        //on the UI
        UIcontroller.instance.UpdateHealthDisplay();
    }
}
