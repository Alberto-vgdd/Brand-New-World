using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        int aux;
        GameObject gaObj = col.gameObject;

        if (gaObj.tag == "Object")
        {
            //to handle object collisions
            Object obj = gaObj.GetComponent<Object>();

            //OBJECT COLLISION HANDLING
            if (obj != null)
            {
                aux = obj.GetPower();

                switch (aux)
                {
                    case GlobalDataScript.NO_POWER:
                        break;


                    //here will bw the code necessary to unlock the different powers
                    case GlobalDataScript.ICE_POWER:
                        break;

                    case GlobalDataScript.FIRE_POWER:
                        break;

                    case GlobalDataScript.STICKY_POWER:
                        break;

                    case GlobalDataScript.POWER_4:
                        break;
                }

                Destroy(gaObj);
                return;
            }
        }
    }
}
