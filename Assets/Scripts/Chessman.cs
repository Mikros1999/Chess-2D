using System.Collections;
using System.Collections.Generic;
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
            case "king_black": this.GetComponent<SpriteRenderer>().sprite = king_black; break;
            case "queen_black": this.GetComponent<SpriteRenderer>().sprite = queen_black; break;
            case "bishop_black": this.GetComponent<SpriteRenderer>().sprite = bishop_black; break;
            case "knight_black": this.GetComponent<SpriteRenderer>().sprite = knight_black; break;
            case "rook_black": this.GetComponent<SpriteRenderer>().sprite = rook_black; break;
            case "pawn_black": this.GetComponent<SpriteRenderer>().sprite = pawn_black; break;

            case "king_white": this.GetComponent<SpriteRenderer>().sprite = king_white; break;
            case "queen_white": this.GetComponent<SpriteRenderer>().sprite = queen_white; break;
            case "bishop_white": this.GetComponent<SpriteRenderer>().sprite = bishop_white; break;
            case "knight_white": this.GetComponent<SpriteRenderer>().sprite = knight_white; break;
            case "rook_white": this.GetComponent<SpriteRenderer>().sprite = rook_white; break;
            case "pawn_white": this.GetComponent<SpriteRenderer>().sprite = pawn_white; break;
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
}
