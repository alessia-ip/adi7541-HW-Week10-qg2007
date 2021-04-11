using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveLoader : MonoBehaviour
{
    //This script is added to make this group project a little easier to work on
    //Additively loading this means I don't have to make all of the UI in one scene
    //I wanted to leave the scene free for my other teammates to work on
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive); //load the UI reset scene
        SceneManager.LoadScene(2, LoadSceneMode.Additive); //load the UI reset scene
    }
}
