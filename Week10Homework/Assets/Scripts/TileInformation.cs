using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TileInformation : MonoBehaviour
{

    //tiles should hold information about themselves to make it easier to learn things about it
    
    public enum TileResource
    {
        water,
        sand,
        clay,
        obsidian,
        lava
    }

    //each tile keeps track of its own resource type
    public TileResource resourceType;
    
    //each tile also keeps track of its own index (since position is not a reliable way to get it)
    public int indexX;
    public int indexY;

}
