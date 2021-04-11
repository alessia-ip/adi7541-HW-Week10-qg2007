using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    private Array2D_3 _array2D;
    
    private void Start()
    {
        _array2D = GameObject.FindWithTag("Game Manager").GetComponent<Array2D_3>();
    }

    public void ResetButton()
    {
        _array2D.MakeGrid();
    }
    
}
