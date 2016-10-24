using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour
{

    private string attachedFragment;
    private int date;
    private int power = -1;



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
}
