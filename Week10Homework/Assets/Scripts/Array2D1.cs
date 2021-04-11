using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//Yanxi
using UnityEngine.UI;
using TMPro;
    

public class Array2D1 : MonoBehaviour
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
    
    //Yanxi: placeholders for Resource Prefabs
    public GameObject clayResource;
    public GameObject lavaResource;
    public GameObject obsidianResource;
    public GameObject sandResource;
    public GameObject waterResource;
    
    
    // Yanxi: A dictionary to represent what resources they have.
    private Dictionary<string, int> resourcesOwned = new Dictionary<string, int>();
    
    //Yanxi: display the resource nums
    public Text displayClay;
    public Text displayLava;
    public Text displayObsidian;
    public Text displaySand;
    public Text displayWater;
    
    //Yanxi: display the recource icons
    public GameObject clayIcon;
    public GameObject lavaIcon;
    public GameObject obsidianIcon;
    public GameObject sandIcon;
    public GameObject waterIcon;
    
    //Yanxi: About Resource Display positions
    private Vector2 ResourceDisplayPostion;
    public float space = 2; //space between each resource display
    public float vertical = 4.6f; // height of the resource displays
    public float horizontal = -3f;
    void Start()
    {
        //Since we need random tile layouts in this game, this is the random seed
        //It is very unlikely you'd 
        Random.InitState(System.DateTime.Now.Second * System.DateTime.Now.Millisecond);
        MakeGrid();
        DisplayIcons();
        ResourceDisplayPostion = tilePlaces[0,0];

        displayClay.text = "0";
        displayLava.text = "0";
        displayObsidian.text = "0";
        displaySand.text = "0";
        displayWater.text = "0";
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
        
        //Yanxi: Display resources
        DisplayResources();
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
    
    //Yanxi: call this to check the tile's resourceType
    public TileInformation.TileResource CheckType(GameObject tile)
    {
        var resourceType = tile.GetComponent<TileInformation>().resourceType;

        return resourceType;
    }
    
    //Yanxi: Call this to destroy a tile
    void DestroyTile(GameObject tile)
    {
        
    }
    
    //Yanxi: Call this to spawn the resource objects
    private IEnumerator SpawnResource(GameObject resource, Vector2 position) {
        Instantiate(resource);
            resource.transform.position = position;
            yield return new WaitForSeconds(1);
            Destroy(resource);
    }
    
    //Yanxi: Add a resource
    public void AddResource(string resourceType, int amountToAdd)
    {
        if (resourcesOwned.ContainsKey(resourceType))
        {
            resourcesOwned[resourceType] = resourcesOwned[resourceType] + amountToAdd;
            
            Debug.Log("You own " + resourcesOwned[resourceType] + " of " + resourceType);
        }
        else
        {
            resourcesOwned.Add(resourceType, amountToAdd);
        }
    }
    //Yanxi: Display resources Icons
    void DisplayIcons()
    {
        var cIcon = Instantiate(clayIcon);
        cIcon.transform.position = new Vector2 (ResourceDisplayPostion.x + horizontal, ResourceDisplayPostion.y + vertical);
        
        var lIcon = Instantiate(lavaIcon);
        lIcon.transform.position = new Vector2(ResourceDisplayPostion.x+space + horizontal, ResourceDisplayPostion.y + vertical);
        
        var oIcon = Instantiate(obsidianIcon);
        oIcon.transform.position = new Vector2(ResourceDisplayPostion.x+space*2 + horizontal, ResourceDisplayPostion.y + vertical);
        
        var sIcon = Instantiate(sandIcon);
        sIcon.transform.position = new Vector2(ResourceDisplayPostion.x+space*3 + horizontal, ResourceDisplayPostion.y + vertical);
        
        var wIcon = Instantiate(waterIcon);
        wIcon.transform.position = new Vector2(ResourceDisplayPostion.x+space*4 + horizontal, ResourceDisplayPostion.y + vertical);

    }
    
    //Yanxi: Display resources numbers that has gained
    public void DisplayResources()
    {

        foreach (KeyValuePair<string, int> keyValuePair in resourcesOwned)
        {
            if (keyValuePair.Key == "clay")
            {
                displayClay.text += resourcesOwned[keyValuePair.Key];
                displayClay.transform.position = clayIcon.transform.position;

            }
            
            if (keyValuePair.Key == "lava")
            {
                displayLava.text += resourcesOwned[keyValuePair.Key];
                displayLava.transform.position = lavaIcon.transform.position;
            }
            
            if (keyValuePair.Key == "obsidian")
            {
                displayObsidian.text += resourcesOwned[keyValuePair.Key];
                displayObsidian.transform.position = obsidianIcon.transform.position;
            }
            
            if (keyValuePair.Key == "Sand")
            {
                displaySand.text += resourcesOwned[keyValuePair.Key];
                displaySand.transform.position = sandIcon.transform.position;
            }
            
            if (keyValuePair.Key == "water")
            {
                displayWater.text += resourcesOwned[keyValuePair.Key];
                displayWater.transform.position = waterIcon.transform.position;
            }
        }
    }

    //Yasnxi: call this to get resource when a tile disappears
    void GetResource(GameObject tile)
    {
        var type = CheckType(tile);
        
        if (type == TileInformation.TileResource.clay)
        {
            StartCoroutine (SpawnResource(clayResource, tile.transform.position));
            AddResource("clay", 1);
        }
        
        if (type == TileInformation.TileResource.lava)
        {
            StartCoroutine (SpawnResource(lavaResource, tile.transform.position));
            AddResource("lava", 1);
        }
        
        if (type == TileInformation.TileResource.obsidian)
        {
            StartCoroutine (SpawnResource(obsidianResource, tile.transform.position));
            AddResource("obsidian", 1);
        }
        
        if (type == TileInformation.TileResource.sand)
        {
            StartCoroutine (SpawnResource(sandResource, tile.transform.position));
            AddResource("sand", 1);
        }
        
        if (type == TileInformation.TileResource.water)
        {
            StartCoroutine (SpawnResource(waterResource, tile.transform.position));
            AddResource("water", 1);
        }
    }
}
