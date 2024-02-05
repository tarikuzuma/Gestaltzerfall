using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //public Animator animator;
    public Transform attackPointHorizontal;
    public float attackRangeHorizontalX;
    public float attackRangeHorizontalY;

    public Transform attackPointVertical;
    public float attackRangeVerticalX;
    public float attackRangeVerticalY;

    public LayerMask enemyMask;
    public LayerMask objectTor;

    public int AttackDamage;

    public float AttackRate;
    private float AttackTime = 0f;

    public Rigidbody2D theRB;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bool isFacingRight = PlayerController.instance.GetIsFacingRight();
        bool isGrounded = PlayerController.instance.GetIsGrounded(); // If player is grounded, you cannot do a mid-air slice jump downwards kaboom
        
        if (isFacingRight)
        {
            
            attackPointHorizontal.localPosition = new Vector3(+0.7f, attackPointHorizontal.localPosition.y, attackPointHorizontal.localPosition.z);
        } else
        {
           
            // Update the x position of attackPointHorizontal to -0.7
            attackPointHorizontal.localPosition = new Vector3(-0.7f, attackPointHorizontal.localPosition.y, attackPointHorizontal.localPosition.z);
        }

        if (Time.time >= AttackTime) { 

            // Normal Attack
            if (Input.GetMouseButtonDown(0)) 
            { 
                Debug.Log("Pressed left-click.");
                AttackHorizontal();
                AttackTime = Time.time + 1f / AttackRate;
            }

            // Downward attack
            if (Input.GetKey(KeyCode.S) && Input.GetMouseButton(0)) 
            {
                attackPointVertical.localPosition = new Vector3(attackPointVertical.localPosition.x, -1.4f, attackPointVertical.localPosition.z);
                if (!isGrounded)
                {
                    AttackVertical();
                    AttackTime = Time.time + 1f / AttackRate;
                } else
                {
                    Debug.Log("Not mid-air, sorry.");
                }
            }

            // Upward Attack
            if (Input.GetKey(KeyCode.W) && Input.GetMouseButton(0))
            {
                Debug.Log("Looking vertically and attack.");
                attackPointVertical.localPosition = new Vector3(attackPointVertical.localPosition.x, +0.7f, attackPointVertical.localPosition.z);
                AttackHorizontal();
            }

        }
    }

    private void AttackHorizontal()
    {
        // Play attack detention
        // Apply damage to enemy

        //animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPointHorizontal.position, new Vector2(attackRangeHorizontalX, attackRangeHorizontalY), 0, enemyMask);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealthController>().KnockBack();
            enemy.GetComponent<EnemyHealthController>().TakeDamage(AttackDamage);
        }

        // call in knockback for enemy. ENEMY KNOCKBACK is dependent on where the player is facing.
    }

    private void AttackVertical()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPointVertical.position, new Vector2(attackRangeVerticalX, attackRangeVerticalY), 0, enemyMask);
        Collider2D[] hitObject = Physics2D.OverlapBoxAll(attackPointVertical.position, new Vector2(attackRangeVerticalX, attackRangeVerticalY), 0, objectTor);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We Hit" + enemy.name + " while mid-air?!");
            enemy.GetComponent<EnemyHealthController>().TakeDamage(AttackDamage);

            PlayerController.instance.KnockBackVertical();

        }

        foreach (Collider2D interacted in hitObject)
        {
            Debug.Log("We Hit" + interacted.name + " while mid-air?!");

            // Add sound
            PlayerController.instance.KnockBackVertical();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a cube to visually see the editor.

        if ( attackPointHorizontal == null)
        {
            return;
        }

        if ( attackPointVertical == null)
        {
            return;
        }

        Gizmos.DrawWireCube(attackPointHorizontal.position, new Vector3(attackRangeHorizontalX, attackRangeHorizontalY, 1));

        Gizmos.DrawWireCube(attackPointVertical.position, new Vector3(attackRangeVerticalX, attackRangeVerticalY, 1));
    }

}