using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour {

    private GameObject canv;
    private GameObject menuContextual;
    private RectTransform transformMenuContextual;

	void Start () {

        canv = GameObject.Find("Canvas");
        menuContextual = GameObject.Find("ContextMenu");
        transformMenuContextual = menuContextual.GetComponent<RectTransform>();

        menuContextual.SetActive(false);
    }


    public void MuestraContextual(float posX, float posY)
    {
        //ajustando posiciones para que no se salga de la pantalla
        if (posX < transformMenuContextual.rect.width)
            posX = transformMenuContextual.rect.width;

        else if (posX > Screen.currentResolution.width - transformMenuContextual.rect.width)
            posX = Screen.currentResolution.width - transformMenuContextual.rect.width;

        if (posY < transformMenuContextual.rect.height)
            posY = transformMenuContextual.rect.height;

        else if (posY > Screen.currentResolution.height - transformMenuContextual.rect.height)
            posY = Screen.currentResolution.height - transformMenuContextual.rect.height;



        menuContextual.SetActive(true);
        transformMenuContextual.position = new Vector3(posX, posY, 0);
    }

    public void EscondeContextual()
    {
        menuContextual.SetActive(false);
    }
}
