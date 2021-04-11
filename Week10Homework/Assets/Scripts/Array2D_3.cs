using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Array2D_3 : MonoBehaviour
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
    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject tile4;
    public GameObject tile5;
    private GameObject thisTile;
    
    
    //spacing and positioning offsets for the tiles
    private float offsetX = -8.2f;
    private float offsetY = -4;
    private float padding = 0.5f;

    //the main scene camera
    public Camera cam;
    
    
    // 
    private int selectCount = 0;
    private GameObject selected1 = null;
    private GameObject selected2 = null;

    public GameObject level;
    
    void Start()
    {
        //Since we need random tile layouts in this game, this is the random seed
        //It is very unlikely you'd 
        Random.InitState(System.DateTime.Now.Second * System.DateTime.Now.Millisecond);
        MakeGrid();
        //
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
                selectCount += 1;
                if (selectCount % 2 == 1)
                {
                    selected1 = hit.collider.gameObject;
                    selected2 = null;
                }
                else
                {
                    selected2 = hit.collider.gameObject;
                }
                TileClicked(selected1,selected2);
            }
        }
        if (selected2 != null)
        {
            compareTiles(selected1,selected2);
        }
    }
    
   public void MakeGrid()
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
        tilesAtPos = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //we instantiate a new tile
                //TODO make this be a random tile from a list/dictionary. We need variety to match
                StartRandomTile();
                var newTile = Instantiate(thisTile);
                //we move the tile to the correct position
                newTile.transform.position = tilePlaces[x, y];
                newTile.transform.parent = level.transform;
                //then we assign it to our tiles 2D array
                tilesAtPos[x, y] = newTile;
                
            }
        }
    }

    void TileClicked(GameObject tile1, GameObject tile2)
    {
        
        var resourceType1 = tile1.GetComponent<TileInformation>().resourceType;
        var resourceType2 = tile2.GetComponent<TileInformation>().resourceType;
        Debug.Log("selected1"+resourceType1.ToString());
        Debug.Log("selected2"+resourceType2.ToString());
    }
    
    void StartRandomTile()
    {
        int r = Random.Range(1,6);
        //Debug.Log("random seed" + r);
        switch (r)
        {
            case 1:
                thisTile = tile1;
                break;
            case 2:
                thisTile = tile2;
                break;
            case 3:
                thisTile = tile3;
                break;
            case 4:
                thisTile = tile4;
                break;
            case 5:
                thisTile = tile5;
                break;
            default:
                break;
        }
    }

    void compareTiles(GameObject tile1, GameObject tile2)
    {
        var resourceType1 = tile1.GetComponent<TileInformation>().resourceType;
        var resourceType2 = tile2.GetComponent<TileInformation>().resourceType;
        if (resourceType1 == resourceType2)
        {
            Debug.Log("sametype=" + resourceType2.ToString());
            LocationIn2dArray(selected1,selected2);
        }
    }

    void LocationIn2dArray(GameObject tile1, GameObject tile2)
    {
        var location1x = tile1.GetComponent<TileInformation>().transform.position.x;
        var location1y = tile1.GetComponent<TileInformation>().transform.position.y;
        int xin2darray1 = Convert.ToInt32((location1x + 8.2) / 1.5);
        int yin2darray1 = Convert.ToInt32((location1y - 3.5)*(-1) / 1.5);
        var location2x = tile2.GetComponent<TileInformation>().transform.position.x;
        var location2y = tile2.GetComponent<TileInformation>().transform.position.y;
        int xin2darray2 = Convert.ToInt32((location2x + 8.2) / 1.5);
        int yin2darray2 = Convert.ToInt32((location2y - 3.5)*(-1) / 1.5);
        CheckPath(xin2darray1,yin2darray1,xin2darray2,yin2darray2);
    }

    void CheckPath(int x1, int y1, int x2, int y2)
    {
        bool pathworks = false;
        if (x1 == x2)
        {
            Debug.Log("samex");
            if (y1 - y2 == 1 || y1 - y2 == -1)
            {
                pathworks = true;
            }
            else if (y1 - y2 > 1)
            {
                {
                    for (var y = y2 + 1; y < y1; y++)
                    {
                        if (tilesAtPos[x1, y] = null)
                        {
                            pathworks = true;
                        }
                    }
                }
            }
            else if (y2 - y1 > 1)
            {
                {
                    for (var y = y1 + 1; y < y2; y++)
                    {
                        if (tilesAtPos[x1, y] = null)
                        {
                            pathworks = true;
                        }
                    }
                }
            }
        }
        else if (y1 == y2)
        { Debug.Log("samey");
            if (x1 - x2 == 1 || x1 - x2 == -1)
            {
                pathworks = true;
            }
            else if (x1 - x2 > 1)
            {
                {
                    for (var x = x2 + 1; x < y1; x++)
                    {
                        if (tilesAtPos[x, y1] = null)
                        {
                            pathworks = true;
                        }
                    }
                }
            }
            else if (x2 - x1 > 1)
            {
                {
                    for (var x = x1 + 1; x < x2; x++)
                    {
                        if (tilesAtPos[x, y1] = null)
                        {
                            pathworks = true;
                        }
                    }
                }
            }
        }
        
        if (pathworks == true)
        {
            tilesAtPos[x1, y1] = null;
            tilesAtPos[x2, y2] = null;
            PathWorking(selected1,selected2);
        }

        pathworks = false;
    }

    void PathWorking(GameObject tile1, GameObject tile2)
    {
        
        
        Destroy(selected1);
        Destroy(selected2);
    }
    
    
    
    
    }




//random
//确定哪两个tile是被选中的
//确定是否为同一种类
//是否可以连线 (x,y 任意相等，且中间都是null)
