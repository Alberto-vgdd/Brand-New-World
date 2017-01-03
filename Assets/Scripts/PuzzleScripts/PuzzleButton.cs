using UnityEngine;
using System.Collections;

public class PuzzleButton : PuzzleElement {


    //THIS VARIABLES WILL BE DEFINED BY THE LEVEL DESIGNER IN THE UNITY EDITOR
    public bool player, balls; //to indicate if the button will react to a player or/and a ball colision
    public bool ice, fire, sticky; //to indicate to which balls has to react


    void OnCollisionEnter2D(Collision2D col)
    {
        //if it can be activated by the player
        if (player)
        {
            if (col.gameObject.tag == GlobalDataScript.PLAYER_TAG)
                activate();
        }

        //if it can be activated by a ball
        if (balls)
        {
            //we check every type of ball, because it can be activated by more of one type
            if (ice)
            {
                if (col.gameObject.tag == GlobalDataScript.ICEBALL_TAG)
                {
                    activate();
                    return;
                }
            }

            if (fire)
            {
                if (col.gameObject.tag == GlobalDataScript.FIREBALL_TAG)
                {
                    activate();
                    return;
                }
            }


            if (sticky)
            {
                if (col.gameObject.tag == GlobalDataScript.STICKYBALL_TAG)
                {
                    activate();
                    return;
                }
            }

        }
    }
}
