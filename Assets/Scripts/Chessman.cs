using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string playerColor;

    public Sprite king_black, queen_black, bishop_black, knight_black, rook_black, pawn_black;
    public Sprite king_white, queen_white, bishop_white, knight_white, rook_white, pawn_white;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "king_white": this.GetComponent<SpriteRenderer>().sprite = king_white; playerColor = "white"; break;
            case "queen_white": this.GetComponent<SpriteRenderer>().sprite = queen_white; playerColor = "white"; break;
            case "bishop_white": this.GetComponent<SpriteRenderer>().sprite = bishop_white; playerColor = "white"; break;
            case "knight_white": this.GetComponent<SpriteRenderer>().sprite = knight_white; playerColor = "white"; break;
            case "rook_white": this.GetComponent<SpriteRenderer>().sprite = rook_white; playerColor = "white"; break;
            case "pawn_white": this.GetComponent<SpriteRenderer>().sprite = pawn_white; playerColor = "white"; break;

            case "king_black": this.GetComponent<SpriteRenderer>().sprite = king_black; playerColor = "black"; break;
            case "queen_black": this.GetComponent<SpriteRenderer>().sprite = queen_black; playerColor = "black"; break;
            case "bishop_black": this.GetComponent<SpriteRenderer>().sprite = bishop_black; playerColor = "black"; break;
            case "knight_black": this.GetComponent<SpriteRenderer>().sprite = knight_black; playerColor = "black"; break;
            case "rook_black": this.GetComponent<SpriteRenderer>().sprite = rook_black; playerColor = "black"; break;
            case "pawn_black": this.GetComponent<SpriteRenderer>().sprite = pawn_black; playerColor = "black"; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f); 
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard() 
    {  
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
       switch (this.name)
        {
            case "queen_white":
            case "queen_black":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "knight_white":
            case "knight_black":
                LMovePlate();
                break;
            case "bishop_white":
            case "bishop_black":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case "king_white":
            case "king_black":
                SurroundMovePlate();
                break;
            case "rook_white":
            case "rook_black":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "pawn_white":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "pawn_black":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x,y);
            x += xIncrement;
            y += yIncrement;
        }

        if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().playerColor != playerColor)
        {
            MovePlateSpawn(x,y, true);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            } else if (cp.GetComponent<Chessman>().playerColor != playerColor)
            {
                MovePlateSpawn(x, y, true);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && 
                sc.GetPosition(x + 1, y) != null && 
                sc.GetPosition(x + 1, y).GetComponent<Chessman>().playerColor != playerColor)
            {
                MovePlateSpawn(x + 1, y, true);
            }

            if (sc.PositionOnBoard(x - 1, y) &&
                sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<Chessman>().playerColor != playerColor)
            {
                MovePlateSpawn(x - 1, y, true);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY, bool isAttack = false)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = isAttack;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
