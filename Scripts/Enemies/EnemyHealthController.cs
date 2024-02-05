using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    // Start is called before the first frame update

    public static EnemyHealthController instance;

    //Public Animator

    public int maxHealth;
    int currentHealth;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public Rigidbody2D theRB;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) 
        {
            currentHealth = 0;
        }
    }


    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0) 
        {
            //Hurt Animation
            Die();
        }
    }

    void Die()
    {
        // Die Animation
        Debug.Log("Enemy Died.");
        // Disable enemy

        gameObject.SetActive(false);
    }

    public void KnockBack()
    {

        bool isFacingRight = PlayerController.instance.GetIsFacingRight();

        if (!isFacingRight)
        {
            theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
        }

    }
}
