using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;

    public Sprite checkPointOnSprite, checkPointOffSprite;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When player enters the 2D area, make sprite ON.
    // Before it turns on, it resets all checkpoints.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();

            theSR.sprite = checkPointOnSprite;

            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = checkPointOffSprite;
    }
}
