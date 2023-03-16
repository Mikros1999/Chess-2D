using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject referece = null;

    // Board positions, not world positions
    int matrixX;
    int matrixY;

    // false -> movement | true -> attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f );
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("ameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(referece.GetComponent<Chessman>().GetXBoard(),
            referece.GetComponent<Chessman>().GetYBoard());

        referece.GetComponent<Chessman>().SetXBoard(matrixX);
        referece.GetComponent<Chessman>().SetYBoard(matrixY);
        referece.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(referece);

        referece.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x; 
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        referece = obj;
    }

    public GameObject GetReference()
    {
        return referece;
    }
}
