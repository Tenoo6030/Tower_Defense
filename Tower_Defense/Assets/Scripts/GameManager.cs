using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int cash = 5, wawe = 0;
    private int cost;
    [SerializeField] private GameObject waweButon;
    [SerializeField] private TMPro.TextMeshProUGUI cashText, waweText;
    private List<Enemy> activeEnemy = new List<Enemy>();

    public bool WaweActive => activeEnemy.Count > 0;
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
        if (Cash >= towerButton.Price && !WaweActive)
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
        wawe++;
        waweText.text =string.Format( "Wawe {0}", wawe);
        
        StartCoroutine(SpawnWawe());

        waweButon.SetActive(false);
    }

    private IEnumerator SpawnWawe()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wawe + 5; i++)
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

            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            enemy.Spawn();
            activeEnemy.Add(enemy);
            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemy.Remove(enemy);
        if (!WaweActive)
        {
            waweButon.SetActive(true);
        }
    }
}
