using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    static public UIManager Instance;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }


    public GameObject txtScore;
    public GameObject txtScoreBest;

    // In Panel GameOver
    //public GameObject pnlGameOver; //
    //public GameObject txtPnlScore;
    //public GameObject txtPnlScoreBest;

}
