using UnityEngine;
using System.Collections;
using System.IO;


public class ObjectGenerator : MonoBehaviour
{

    private string path;
    private int actual, auxDate;
    private string[] auxiliar, aux;


    void Start()
    {
        System.Collections.Generic.List<string> aux;
        //initializing variables
        auxiliar = new string[500];
        GlobalDataScript.Fragments = new System.Collections.Generic.List<string>[20];
        //fragments = new string[20][];

        for (int i = 0; i < 20; i++)
        {
            aux = new System.Collections.Generic.List<string>();
            GlobalDataScript.Fragments[i] = aux;
        }
        GlobalDataScript.SetFragmentTags(Random.Range(1,6));
        GlobalDataScript.setDates();
        loadFragments();
        InitializeObjects();
    }



    private void loadFragments()
    {
        //string[] aux;

        path = Directory.GetCurrentDirectory();
        path += "\\fragmentos.txt";

        if (File.Exists(path))
        {
            int actual = -1;
            StreamReader lector = new StreamReader(path);//, Encoding.Default);

            do
            {
                actual++;
                auxiliar[actual] = lector.ReadLine();

            } while (auxiliar[actual] != null);

            lector.Close();


            //DIVIDE LAS CADENAS Y SE QUEDA CON LOS DATOS DE INTERES

            //must start in 1 to avoid reading the first line
            actual = 1;
            while (auxiliar[actual] != null)
            {

                aux = auxiliar[actual].Split('-');
                if (aux.Length > 1)
                {

                    for (int i = 1; i < aux.Length; i++)
                    {
                        GlobalDataScript.Fragments[System.Convert.ToInt32(aux[i])].Add(auxiliar[actual + 1]);
                    }

                    actual += 2;
                    continue;
                }
                actual++;
            }
        }

        else
        {
            print("El archivo 'fragmentos.txt' no existe, comprobar que su subicación es correcta en la carpeta raiz del proyecto");
        }

    }


    public void InitializeObjects()
    {
        GameObject auxObj;
        Object[] objectArray;
        int[] aux = GlobalDataScript.GetDates();
        int iteration = 0, random;
        string fragment;
        
        //to store the minimmum and maximmum dates
        /*we ASSUME that every gameobject has an "object" script in it and a sprite renderer whoose image can be changed
         * THE FOUR FIRST OBJECTS WILL CONTAIN THE POWERS IN THE SAME ORDER THEY ARE DECLARED IN GLOBAL DATA
         * THE OTHERS WILL HAVE NO POWERs*/

        objectArray = GetComponentsInChildren<Object>();    

        foreach(Object obj in objectArray)
        {
                //code to change the objects sprite


               // code to give it a fragment, a date and a power if it unlocks one
            
            random = Random.Range(0, GlobalDataScript.Fragments[GlobalDataScript.GetFragmentsTag()].Count); //calculates a random number
            obj.SetFragment(GlobalDataScript.Fragments[GlobalDataScript.GetFragmentsTag()][0]); //sets the random fragment
            GlobalDataScript.Fragments[GlobalDataScript.GetFragmentsTag()].Remove(obj.GetFragment()); //deletes that fragment from the list
            auxDate = Random.Range(aux[0], aux[1]);
            obj.SetDate(auxDate);

                //DEPENDING ON THE ORDER WE GIVE THE OBJECT A POWER OR NONE
                if (iteration < 4)
                {
                    switch (iteration)
                    {
                        case 0:
                            obj.SetPower(GlobalDataScript.FIRE_POWER);
                            break;

                        case 1:
                            obj.SetPower(GlobalDataScript.ICE_POWER);
                            break;

                        case 2:
                            obj.SetPower(GlobalDataScript.STICKY_POWER);
                            break;

                        case 3:
                            obj.SetPower(GlobalDataScript.POWER_4);
                            break;
                    }
                }

                else
                    obj.SetPower(GlobalDataScript.NO_POWER);

            /*
                //we obtain the random fragment's number
                
                //setting the objects fragment and date
                fragment = GlobalDataScript.Fragments[GlobalDataScript.GetFragmentsTag()] //we get the list
                    [random];

                obj.SetFragment(fragment); //we extract a random fragment

                //we delete the fragment, to avoid repetition
                GlobalDataScript.Fragments[System.Convert.ToInt32(aux[i])].Add(auxiliar[actual + 1]);*/


                iteration++;

        }
    }
}
