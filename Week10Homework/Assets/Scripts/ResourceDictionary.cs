using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//Yanxi
using UnityEngine.UI;
using TMPro;
    

public class ResourceDictionary : MonoBehaviour
{

    //these are the two 2D arrays
    private Vector2[,] tilePlaces;
    private GameObject[,] tilesAtPos;

    //the main scene camera
    public Camera cam;
    
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


    private int metalOwned = 0;
    public Text metalText;
    
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        
        DisplayIcons();

        displayClay.text = "0";
        displayLava.text = "0";
        displayObsidian.text = "0";
        displaySand.text = "0";
        displayWater.text = "0";

        GameObject.FindWithTag("Game Manager").GetComponent<Array2D>()._ResourceDictionary = this;
    }

    //Yanxi: call this to check the tile's resourceType
    public TileInformation.TileResource CheckType(GameObject tile)
    {
        var resourceType = tile.GetComponent<TileInformation>().resourceType;

        return resourceType;
    }
    
    
    //Yanxi: Call this to spawn the resource objects
    //Alessia: I don't think we need this?
    /*
    private IEnumerator SpawnResource(GameObject resource, Vector2 position) {
        Instantiate(resource);
            resource.transform.position = position;
            yield return new WaitForSeconds(1);
            Destroy(resource);
    }
    */
    
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
        //each of these puts the right icons in the right spots. 
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
        //we show all the resources 
        //this should probably be a switch statement
        //this is updating the UI
        if (resourcesOwned.ContainsKey("clay"))
        {
            displayClay.text = resourcesOwned["clay"].ToString();
        }
        if (resourcesOwned.ContainsKey("water"))
        {
            displayWater.text = resourcesOwned["water"].ToString();
        }
        if (resourcesOwned.ContainsKey("obsidian"))
        {
            displayObsidian.text = resourcesOwned["obsidian"].ToString();
        }
        if (resourcesOwned.ContainsKey("sand"))
        {
            displaySand.text = resourcesOwned["sand"].ToString();
        }        
        if (resourcesOwned.ContainsKey("lava"))
        {
            displayLava.text = resourcesOwned["lava"].ToString();
        }
    }

    //Yanxi: call this to get resource when a tile disappears
    public void GetResource(GameObject tile)
    {
        var type = CheckType(tile);
        //we check what type of resource we're adding 
        
        if (type == TileInformation.TileResource.clay)
        {
            AddResource("clay", 1);
        }
        
        if (type == TileInformation.TileResource.lava)
        {
            AddResource("lava", 1);
        }
        
        if (type == TileInformation.TileResource.obsidian)
        {
            AddResource("obsidian", 1);
        }
        
        if (type == TileInformation.TileResource.sand)
        {
            AddResource("sand", 1);
        }
        
        if (type == TileInformation.TileResource.water)
        {
            AddResource("water", 1);
        }
        
        Debug.Log("Added a resource of type: " + type);
        //Yanxi: Display resources
        //Alessia: this only needs to be called if you're updating the number of resources you have
        //We don't need to update the Ui every frame
        DisplayResources();
    }

    public void RefineResources()
    {
        if (resourcesOwned["lava"] > 0 &&
            resourcesOwned["clay"] > 0 &&
            resourcesOwned["water"] > 0 &&
            resourcesOwned["obsidian"] > 0 &&
            resourcesOwned["clay"] > 0)
        {
            AddResource("water", -1);
            AddResource("clay", -1);
            AddResource("obsidian", -1);
            AddResource("lava", -1);
            AddResource("sand", -1);

            metalOwned++;
            metalText.text = metalOwned + " piece of metal owned";
            
            DisplayResources();
        }
    }
    
}
