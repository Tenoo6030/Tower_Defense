using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private TMPro.TextMeshProUGUI cashText;
    private int cash = 5;
    private int cost;


    public TowerButton ClickedButton { get; set; }
    public ObjectPool Pool { get; set; }
    public int Cash
    {
        get { return cash; }
        set
        {
            cash = value;
            cashText.text = value.ToString();
        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
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

    public void StartWawe()
    {
        StartCoroutine(SpawnWawe());
    }

    private IEnumerator SpawnWawe()
    {

        int monsterIndex = Random.Range(0, 4);
        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "EnemyGreen";
                break;
            case 1:
                type = "EnemyPurple";
                break;
            case 2:
                type = "EnemyYellow";
                break;
            case 3:
                type = "EnemyRed";
                break;
            default:
                break;
        }

        Pool.GetObject(type);

        yield return new WaitForSeconds(2.5f);
    }
}
