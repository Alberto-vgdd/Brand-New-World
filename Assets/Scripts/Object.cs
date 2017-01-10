using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour
{
    private AudioSource audio;
    public string attachedFragment;
    public int date;
    private int power = -1;
    private CanvasControllerScript canvasController;
    public bool prueba;


    void Start()
    {
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasControllerScript>();
        audio = this.gameObject.GetComponent<AudioSource>();
    }



    void OnDestroy()
    {
        canvasController.NewFragment(attachedFragment, date);

        //we save the fragment, so it can be reloaded when the scene changes
        GlobalDataScript.AddFragment(System.Convert.ToString(date), attachedFragment);
    }

    public bool SetPower(int pow)
    {
        if (power != -1)
        {
            power = pow;
            return true;
        }
        else
            return false;
    }

    public int GetPower()
    {
        return power;
    }

    //sets a random fragment it there wasn't one setted yet
    public bool SetFragment(string frag)
    {
        prueba = true;
        if (attachedFragment != null)
        {
            attachedFragment = frag;
            return true;
        }
        else
            return false;
    }

    public string GetFragment()
    {
        return attachedFragment;
    }

    public void SetDate(int date)
    {
        this.date = date;
    }

    public int GetDate()
    {
        return date;
    }

    public void Play()
    {
        audio.PlayOneShot(audio.clip);
    }
    

}
