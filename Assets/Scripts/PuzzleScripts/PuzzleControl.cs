using UnityEngine;
using System.Collections;

public class PuzzleControl : MonoBehaviour {

    private const float VELOCITY = 0.05f;

    public GameObject[] Elements; //stores the elements that have to be activated to solve the puzzle

    //variables that specify what will happen when the puzzle is solved
    public bool Destroy, Move;
    public GameObject Objective; //the object that will suffer the changes
    public float X, Y; //to specify in which axis the object will move and how much

    private bool moving;
    private Vector3 finalObjectivePosition, actualPosition;
    private bool done; //to stop checking coordinates when the object has moved
    private int finalCount, currentCount; //number of elemnts that will have to be solved and number of the elements that have been solved
    public bool activ;

	// Use this for initialization
	void Start () {

        finalCount = Elements.Length;
        currentCount = 0;

        if (Move)
        {
            finalObjectivePosition = new Vector3(Objective.transform.position.x + X, Objective.transform.position.y + Y, Objective.transform.position.z);
            actualPosition = Objective.transform.position;
        }
	}

    public void Update()
    {
        if (moving)
        {
            done = true;
            
            //X AXIS
            if (X < 0)
            {
                if (finalObjectivePosition.x < actualPosition.x)
                {
                    actualPosition.x += VELOCITY;
                    done = false;
                }
            }
            else if (X > 0)
            {
                if (finalObjectivePosition.x > actualPosition.x)
                {
                    actualPosition.x += VELOCITY;
                    done = false;
                }
            }

            //Y AXIS
            if (Y < 0)
            {
                if (finalObjectivePosition.y < actualPosition.y)
                {
                    actualPosition.y += VELOCITY;
                    done = false;
                }
            }
            else if (Y > 0)
            {
                if (finalObjectivePosition.y > actualPosition.y)
                {
                    actualPosition.y += VELOCITY;
                    done = false;
                }
            }

            Objective.transform.position = actualPosition;
            if (done)
                moving = false;
        }

    }

    public void ActivateElement()
    {
        currentCount++;
        activ = !activ;
        if (currentCount == finalCount)
            SolvePuzzle();
    }

    private void SolvePuzzle()
    {
        if (Destroy)
            Object.Destroy(Objective);

        else if (Move)
            moving = true;
    }
}
