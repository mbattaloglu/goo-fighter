using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;

    private static GameManager instance;

    private GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static GameManager GetInstance()
    {
        return instance;
    }
    
}
