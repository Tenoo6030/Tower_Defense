using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private TMPro.TextMeshProUGUI cashText;
    private int cash = 5;
    private int cost;


    public TowerButton ClickedButton { get; set; }
    public int Cash
    {
        get { return cash; }
        set
        {
            cash = value;
            cashText.text = value.ToString();
        }
    }

    void Start()
    {
        Cash = 45;
    }

    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerButton)
    {
        if (Cash >= towerButton.Price)
        {
            ClickedButton = towerButton;
            Hover.Instance.Activate(towerButton.TowerIcon);
            cost = towerButton.Price;
        }
    }
    public void BayTower()
    {
        Cash -= cost;
        Hover.Instance.Disactivate();
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Disactivate();
        }
    }
}
