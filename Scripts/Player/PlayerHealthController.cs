using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;


    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;
     

    // Start is called before the first frame update
    // Line of code only starts when initialized.
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame or over and over again
    void Update()
    {
        if (invincibleCounter > 0)
        {
           
            // invincibleCounter--; perpatual 0 if that is the case.
            invincibleCounter -= Time.deltaTime; // It'll always take one second to take one away. THANK YOU UNITY!

            if (invincibleCounter <= 0 ) 
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }

        
    }

    // Referenced by damagePlayer.cs. Upon hitting collision, it will deduct player health by 1
    public void DamagePlayer(int damage)
    {
        if (invincibleCounter <= 0)
        {

        currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false); // Comment out to debug respawn

                LevelManager.instance.RespawnPlayer();

            } else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);

                    PlayerController.instance.KnockBack();
            }

        // Everytime player is damaged, it willl call the instance in UIcontroller.cs
        UIcontroller.instance.UpdateHealthDisplay();
        }
    }
}
