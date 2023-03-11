﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedButton { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PickTower(TowerButton towerButton)
    {
       ClickedButton = towerButton;
    }
    public void BayTower()
    {
        ClickedButton = null;
    }
}
