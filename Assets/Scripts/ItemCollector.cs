using System.Collections;
using System.Collections.Generic;
using UnityEngine; //this means we can use classes contained within this UnityEngine Package in our code
using UnityEngine.UI; //to access Text packages which aren't contained within the default Unity engine package

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectionSoundEffect;
    /* private int bananas = 0; //initialised the integer (whole numbers- can't have 0.1 cherries) as 0 so we don't start with any
    //Don't need our start or update method as we only want our script to be executed when we collide with the collectible

    [SerializeField] private Text bananasText;
    private void OnTriggerEnter2D(Collider2D collision) //OnTriggerEnter2D because we set the box collider of the banana collectible to tick IsTrigger
        //if we didn't tick it in ispector, we'd have to use OnCollisionEnter2D
    {
        if(collision.gameObject.CompareTag("Banana")) //note has to have exact same spelling as the tag name for the collectible in the inspector
        {
            Destroy(collision.gameObject); //here we are passing through the game object we want to destroy, which we have assigned as our banana, in the code above
            bananas++;
            bananasText.text = "Keys: " + bananas; //to update the text score in inspector, adding new banana to previous score
        }
    } */

    private int keys = 0; //initialised the integer (whole numbers- can't have 0.1 cherries) as 0 so we don't start with any
    //Don't need our start or update method as we only want our script to be executed when we collide with the collectible

    [SerializeField] private Text keysText;
    private void OnTriggerEnter2D(Collider2D collision) //OnTriggerEnter2D because we set the box collider of the banana collectible to tick IsTrigger
                                                        //if we didn't tick it in ispector, we'd have to use OnCollisionEnter2D
    {
        if (collision.gameObject.CompareTag("Banana")) //note has to have exact same spelling as the tag name for the collectible in the inspector
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject); //here we are passing through the game object we want to destroy, which we have assigned as our banana, in the code above
            keys++;
            keysText.text = "Keys: " + keys; //to update the text score in inspector, adding new banana to previous score
        }
    } 
}
