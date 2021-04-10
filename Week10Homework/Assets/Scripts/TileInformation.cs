using System.Collections;
using System.Collections.Generic;
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

}
