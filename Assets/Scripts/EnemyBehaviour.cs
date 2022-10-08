using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //Reference to waypoints
    public List<Transform> points;
    //THe int value for next point index
    public int nextID = 0;
    //THe value that applies to ID for changing
    int idChangeValue = 1;
    //Speed of movement
    private float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //Get the next Point transform
        Transform goalPoint = points[nextID];
        //Flip the enemy transform to look into the point's direction
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        //Check the distance between enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position)<1f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //2 points (0,1), therefore the nextID points == points.count(2)-1
            //Check if we are at the start of the line (make the change +1)
            if (nextID == 0) //0 is always a starting point
                idChangeValue = 1;
            //Apply the change to nextID
            nextID += idChangeValue;
        }
    }
}