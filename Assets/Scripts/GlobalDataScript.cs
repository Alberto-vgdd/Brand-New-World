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
    public static bool OBJECT_PICKED_PANEL_ACTIVE = false;
    public static int ROOM_ENTRANCE = 0;
    public static string LAST_SCENE = "S01";

    //data that has special conditions to be changed
    private static bool tags_set = false;
    private static int fragmentTags;

    private static int minimumDate = 0;
    private static int maximumDate = 1600;
    private static bool datesSet;

    //stats data
    public static int EnemiesKilled = 0;
    public static int TotalJumps = 0;
    public static int ObstaclesDestroyed = 0;
    public static int TotalDeaths = 0;
    public static int ObjectsPicked = 0;

    //punctuation
    private static int[] maxPuntuations = {500, 1000, 1500, 2000, 2500};
    public static int currentPuntuation = 0;

    public const int OBJECT_POINTS = 300;
    public const int ENEMY_DESTRUCTION_POINTS = 150;
    public const int DEATH_POINTS = -1000;

    //fragments corresponding to the fragment's tag extracted
    //they are stored here to avoid having to load them every time the scene changes
    public static System.Collections.Generic.List<string>[] Fragments;


    //this will store the fragments that have already been picked,
    //so they can be loaded easily when the new scene loads
    //this will be used until a proper room-change system is coded
    public static string[][] PickedFragments = new string[20][];
    public static int last = 0;
    //it has 20 elements because that is the maxximum number of objects at the moment
    //of 2 sub elements (date and fragment)



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
            minimumDate = Random.Range(0, 9000);
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

    public static void AddFragment(string date, string fragment)
    {
        PickedFragments[last] = new string[2];
        PickedFragments[last][0] = date;
        PickedFragments[last][1] = fragment;
        last = last + 1;
    }

    public static void IncreasePuntuation(int quantity)
    {
        currentPuntuation += quantity;
    }

    public static int[] CheckAndGetPuntuations()
    {
        //this method checks if the current puntuation is higher tan any of the max puntuations already registered and returns the
        //new array of maximum puntuations with the new puntuation in its corresponding place
        //+ the current puntuation
        //[1-5] maximum puntuations updated [6] current puntuation

        int[] aux = new int[6];
        int lastPlace = -1;

        for(int i = 0; i < 5; i++)
        {
            aux[i] = maxPuntuations[i];
            if (currentPuntuation > maxPuntuations[i])
                lastPlace = i;
        }

        aux[5] = currentPuntuation;

        if (lastPlace != -1)
            aux[lastPlace] = currentPuntuation;

        maxPuntuations = aux;

        return aux;

    }

    }
