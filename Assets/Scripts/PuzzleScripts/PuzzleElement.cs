using UnityEngine;
using System.Collections;

public class PuzzleElement : MonoBehaviour {


    protected bool activated; //if the element has been activated or not
    protected PuzzleControl mainPuzzle; //the main puzzle PuzzleControl script

    public void Start()
    {
        mainPuzzle = this.gameObject.GetComponentInParent<PuzzleControl>();
    }


    protected void activate()
    {
        if (!activated)
        {
            mainPuzzle.ActivateElement();
            activated = true;
        }
    }
}
