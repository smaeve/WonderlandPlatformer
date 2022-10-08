using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null); //null effectively removes the player from the parent
            //Ensure to set layer to "ground" in Moving Platform inspector, otherwise we won't be able to jump while standing
            //on the platform, our isGrounded check, checks for the ground layer mask.
        }
    }
}
