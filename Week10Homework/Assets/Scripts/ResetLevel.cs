using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    private Array2D _array2D;
    
    private void Start()
    {
        //since we are additively loading this, we need to get the manager by tag
        _array2D = GameObject.FindWithTag("Game Manager").GetComponent<Array2D>();
    }

    public void ResetButton()
    {
        //we just want to remake the grid without refreshing the resources
        _array2D.MakeGrid();
    }
    
}
