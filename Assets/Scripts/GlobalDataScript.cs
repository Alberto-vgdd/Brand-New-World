using UnityEngine;
using System.Collections;

public class GlobalDataScript : MonoBehaviour {


    //POWER-RELATED CONSTANTS
    public const int NO_POWER = 0;
    public const int FIRE_POWER = 1;
    public const int ICE_POWER = 2;
    public const int STICKY_POWER = 3;
    public const int POWER_4 = 4;

    //TAG CONSTANTS
    public const string PLAYER_TAG = "Player";
    public const string FIREBALL_TAG = "Fireball";
    public const string ICEBALL_TAG = "Iceball";
    public const string STICKYBALL_TAG = "Stickyball";

    public const float NORMAL_TIME_SPEED = 1f;
    public const float NORMAL_FIXED_DELTA_TIME = 0.02f;
    public const float SLOW_TIME_SPEED = 0.025f;
    public const float SLOW_FIXED_DELTA_TIME = NORMAL_FIXED_DELTA_TIME * SLOW_TIME_SPEED;
    public const float PAUSED_TIME_SPEED = 0f;
    public const float PAUSED_FIXED_DELTA_TIME = 0f;
    public static bool CONTEXT_MENU;
    public static bool PAUSE_MENU;
    public static bool CROUCHING;
    public static bool INPUT_ENABLED = true;
    public static bool CRONOLINE;
    public static int ROOM_ENTRANCE = 1;


    //data that has special conditions to be changed
    private static bool tags_set = false;
    private static int fragmentTags;

    private static int minimumDate = 0;
    private static int maximumDate = 1600;
    private static bool datesSet;



    //sets the tag that will be used for this game if it has'nt been set
    public static bool SetFragmentTags(int tag)
    {
        if (tags_set)
            return false;
        else
        {
            fragmentTags = tag;
            tags_set = true;
            return tags_set;
        }
    }

   // returns the tag if setted
    public static int GetFragmentsTag()
    {
        if (tags_set)
            return fragmentTags;
        else
            return -1;
    }


    //sets the range of dates, but only once per game
    public static bool setDates()
    {
        if (!datesSet)
        {
            minimumDate = Random.Range(100, 9000);
            maximumDate = minimumDate + Random.Range(10, 200);
            return true;
        }
        else
            return false;
    }


    //returns an array with the minnimum and maximum dates
    public static int[] GetDates()
    {
            int[] aux = new int[2];
            aux[0] = minimumDate;
            aux[1] = maximumDate;
            return aux;
        }




    }
