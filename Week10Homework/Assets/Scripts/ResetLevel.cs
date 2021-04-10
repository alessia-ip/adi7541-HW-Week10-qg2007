using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    private Array2D _array2D;
    
    private void Start()
    {
        _array2D = this.gameObject.GetComponent<Array2D>();
    }


    void ResetButton()
    {
        _array2D.MakeGrid();
    }
    
}
