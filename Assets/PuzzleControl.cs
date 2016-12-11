using UnityEngine;
using System.Collections;

public class PuzzleControl : MonoBehaviour {

    private const float VELOCITY = 0.05f;

    public GameObject[] Elements; //stores the elements that have to be activated to solve the puzzle

    //variables that specify what will happen when the puzzle is solved
    public bool destroy, move;
    public GameObject objective; //the object that will suffer the changes
    public float x, y; //to specify in which axis the object will move and how much


    private bool[] completed; //each one corresponds to one element
    private bool moving;
    private Vector3 finalObjectivePosition, actualPosition;
    private bool done; //to stop checking coordinates when the object has moved

	// Use this for initialization
	void Start () {
        completed = new bool[Elements.Length];

        if (move)
        {
            finalObjectivePosition = new Vector3(objective.transform.position.x + x, objective.transform.position.y + y, objective.transform.position.z);
            actualPosition = objective.transform.position;
        }
	}

    public void Update()
    {
        if (moving)
        {
            done = true;
            
            //X AXIS
            if (x < 0)
            {
                if (finalObjectivePosition.x > actualPosition.x)
                {
                    actualPosition.x += VELOCITY;
                    done = false;
                }
            }
            else if (x > 0)
            {
                if (finalObjectivePosition.x < actualPosition.x)
                {
                    actualPosition.x += VELOCITY;
                    done = false;
                }
            }

            //Y AXIS
            if (y < 0)
            {
                if (finalObjectivePosition.y > actualPosition.y)
                {
                    actualPosition.y += VELOCITY;
                    done = false;
                }
            }
            else if (y > 0)
            {
                if (finalObjectivePosition.y < actualPosition.y)
                {
                    actualPosition.y += VELOCITY;
                    done = false;
                }
            }

            objective.transform.position = actualPosition;
            if (done)
                moving = false;
        }

    }

    public void ElementCompleted(int id)
    {
        int count = 0;

        completed[id] = true;

        foreach (bool element in completed)
        {
            if (element)
                count++;
        }

        if (count == Elements.Length)
            SolvePuzzle();
    }

    private void SolvePuzzle()
    {
        if (destroy)
            Destroy(objective);

        else if(move)


        return;
    }
}
