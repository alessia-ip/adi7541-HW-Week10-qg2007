using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Array2D : MonoBehaviour
{
    //This script is to generate the grid
    //top left corner is -8.88, 4.92, for offset

    //these are the two 2D arrays
    private Vector2[,] tilePlaces;
    private GameObject[,] tilesAtPos;

    //the height and width of the tile grid
    public int height;
    public int width;

    //placeholder object until we have real tiles to use
    public GameObject greenTile;

    //spacing and positioning offsets for the tiles
    private float offsetX = -8.2f;
    private float offsetY = -4;
    private float padding = 0.5f;

    //the main scene camera
    public Camera cam;

    
    public GameObject level;
    
    void Start()
    {
        //Since we need random tile layouts in this game, this is the random seed
        //It is very unlikely you'd 
        Random.InitState(System.DateTime.Now.Second * System.DateTime.Now.Millisecond);
        MakeGrid();
    }

    private void Update()
    {
        //when you click the mouse, we want to see what we've hit
        if (Input.GetMouseButtonDown(0))
        {
            //we get the mouse position in worldspace so we know the point at which we clicked
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            //then we raycast at that point
            RaycastHit2D hit = Physics2D.Raycast(
                mousePos, 
                Vector2.zero);

            //if we hit a collider, AND that collider was a tile object, we want to execute the 'tile clicked' function
            if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
            {
                var selected = hit.collider.gameObject;
                TileClicked(selected);
            }
        }
    }

    void MakeGrid()
    {
        if (level != null)
        {
            Destroy(level);
        }

        level = new GameObject("Level");
        
        //set grid positions
        tilePlaces = new Vector2[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //at x,y in our array, we set the x,y of the vector 2
                //This is to make it cleaner later to set positioning
                //The padding is the spacing around each tile
                //the offset is the position in the world
                var newPos = new Vector2(
                    x + (padding * x) + offsetX, 
                    y + (padding * y) + offsetY);
                //only when we've figured out the position to we put it in the correct spot in the array
                tilePlaces[x, y] = newPos;
            }
        }
        
        //put tiles at positions defined above
        //TODO add random tile types
        tilesAtPos = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //we instantiate a new tile
                //TODO make this be a random tile from a list/dictionary. We need variety to match
                var newTile = Instantiate(greenTile);
                //we move the tile to the correct position
                newTile.transform.position = tilePlaces[x, y];
                newTile.transform.parent = level.transform;
                //then we assign it to our tiles 2D array
                tilesAtPos[x, y] = newTile;
            }
        }
    }

    void TileClicked(GameObject tile)
    {
        
        var resourceType = tile.GetComponent<TileInformation>().resourceType;
        
        Debug.Log(resourceType.ToString());
        
    }
    
}
