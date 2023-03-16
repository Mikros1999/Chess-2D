using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("rook_white",0,0), Create("knight_white",1,0), Create("bishop_white",2,0), Create("queen_white",3,0),
            Create("king_white",4,0), Create("bishop_white",5,0), Create("knight_white",6,0), Create("rook_white",7,0),
            Create("pawn_white",0,1), Create("pawn_white",1,1), Create("pawn_white",2,1), Create("pawn_white",3,1),
            Create("pawn_white",4,1), Create("pawn_white",5,1), Create("pawn_white",6,1), Create("pawn_white",7,1)
        };

        playerBlack = new GameObject[]
        {
            Create("rook_black",0,7), Create("knight_black",1,7), Create("bishop_black",2,7), Create("queen_black",3,7),
            Create("king_black",4,7), Create("bishop_black",5,7), Create("knight_black",6,7), Create("rook_black",7,7),
            Create("pawn_black",0,6), Create("pawn_black",1,6), Create("pawn_black",2,6), Create("pawn_black",3,6),
            Create("pawn_black",4,6), Create("pawn_black",5,6), Create("pawn_black",6,6), Create("pawn_black",7,6)
        };

        for(int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }
    } 

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0,0,-1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x,y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }
}
