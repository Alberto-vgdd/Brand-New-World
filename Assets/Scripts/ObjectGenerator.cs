using UnityEngine;
using System.Collections;
using System.IO;


public class ObjectGenerator : MonoBehaviour
{

    public System.Collections.Generic.List<string>[] fragments;
    public string path;
    public int actual;
    public string[] auxiliar, aux;
    public string prueba;
    public int asf;



    void Start()
    {
        System.Collections.Generic.List<string> aux;
        //initializing variables
        auxiliar = new string[500];
        fragments = new System.Collections.Generic.List<string>[20];
        //fragments = new string[20][];

        for (int i = 0; i < 20; i++)
        {
            aux = new System.Collections.Generic.List<string>();
            fragments[i] = aux;
        }

        loadFragments();
        prueba = fragments[1][0];
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
                        fragments[System.Convert.ToInt32(aux[i])].Add(auxiliar[actual + 1]);
                    }

                    actual += 2;
                    continue;
                }
                actual++;
            }
        }

        else
        {
            //ERRORACO GORDO
        }

    }


    public void InitializeObjects()
    {
        GameObject auxObj;
        Object[] objectArray;
        int[] aux = GlobalDataScript.GetDates();
        int iteration = 0, random;
        asf = 0;
        
        //to store the minimmum and maximmum dates
        /*we ASSUME that every gameobject has an "object" script in it and a sprite renderer whoose image can be changed
         * THE FOUR FIRST OBJECTS WILL CONTAIN THE POWERS IN THE SAME ORDER THEY ARE DECLARED IN GLOBAL DATA
         * THE OTHERS WILL HAVE NO POWERs*/

        objectArray = GetComponentsInChildren<Object>();    

        foreach(Object obj in objectArray)
        {
                //code to change the objects sprite


               // code to give it a fragment, a date and a power if it unlocks one
            
            random = Random.Range(0, fragments[GlobalDataScript.GetFragmentsTag()].Count); //calculates a random number
            obj.SetFragment(fragments[GlobalDataScript.GetFragmentsTag()][0]); //sets the random fragment
            fragments[GlobalDataScript.GetFragmentsTag()].Remove(obj.GetFragment()); //deletes that fragment from the list
            obj.SetFragment(prueba);


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

                //setting the objects fragment and date
                obj.SetFragment(fragments[GlobalDataScript.GetFragmentsTag()] //we get the list
                    [Random.Range(0, fragments[GlobalDataScript.GetFragmentsTag()].Count)]); //we extract a random fragment

                obj.SetDate(Random.Range(aux[0], aux[1]));

                iteration++;
        }
    }
}
