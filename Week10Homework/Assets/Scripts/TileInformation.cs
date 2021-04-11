using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TileInformation : MonoBehaviour
{

    public enum TileResource
    {
        water,
        sand,
        clay,
        obsidian,
        lava
    }

    public TileResource resourceType;

    public int indexX;
    public int indexY;

}
