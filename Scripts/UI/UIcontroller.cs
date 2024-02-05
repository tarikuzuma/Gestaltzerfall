using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public static UIcontroller instance;

    // Subjected to change since player health may increase.
    public Image heart1, heart2, heart3;


    public Sprite heartFull, heartEmpty, heartHalf;

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

    // References PlayerHealthController by checking how much health they have as of the moment.
    public void UpdateHealthDisplay()
    {
        //Check current health of player and does a switch case. This funtion is always constant.
        switch (PlayerHealthController.instance.currentHealth) 
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;

                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;

                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;

                break;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;

                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

                /*
                 
                 int playerHealth = PlayerHealthController.instance.currentHealth;

                    for (int i = 0; i < 3; i++) 
                    {
                        Image heart = i == 0 ? heart1 : i == 1 ? heart2 : heart3;

                        if (playerHealth >= (i + 1) * 2)
                            heart.sprite = heartFull;
                        else if (playerHealth == (i + 1) * 2 - 1)
                            heart.sprite = heartHalf;
                        else
                            heart.sprite = heartEmpty;
                    }

                 */
        }
    }
}
