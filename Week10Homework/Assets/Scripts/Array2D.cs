using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array2D : MonoBehaviour
{
    private Vector2[,] tilePlaces;
    private GameObject[,] tilesAtPos;

    public int height;
    public int width;
    
    // Start is called before the first frame update
    void Start()
    {
        MakeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeGrid()
    {
        tilePlaces = new Vector2[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var newPos = new Vector2(x, y);
                Debug.Log("Position at " + x + "," + y + " is" + newPos);
                tilePlaces[x, y] = newPos;
            }
        }
    }
    
}
