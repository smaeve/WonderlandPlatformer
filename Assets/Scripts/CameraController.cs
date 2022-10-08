using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; //to tag our camera onto the Player's position under "Transform" in the unity inspector
    //Creating a field under the camera script allows us to drag the player's position onto the camera inspector

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        //we want our camera to have the same x and y position as our player so it follows it.
        //we keep the camera z- position we have at -10, because the player's one is a 0 and we won't be able to see the bg
        //since we put this code in Update(), it will be executed in every frame, so we will constantly follow our player
    }
}
