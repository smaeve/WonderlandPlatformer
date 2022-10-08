using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //imported for reload scene after death

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private SizeManager sizeScript;
    private Rigidbody2D rb; //a reference to our player rigidbody, so we can change the body type from dynamic to static

    // Start is called before the first frame update
    private void Start()
    {
        sizeScript = GetComponent<SizeManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))  //"Trap" is the tag we assigned to the sprikes in the inspector
        {
            if(sizeScript.isBig)
            {
                sizeScript.gettingHit();
            }
            else {
                Die();
            }
        }                                             
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static; //disables movement of the player when we die
        anim.SetTrigger("death"); //the animator will switch to death animation
    }

    //RELOAD LEVEL AFTER DEATH
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
