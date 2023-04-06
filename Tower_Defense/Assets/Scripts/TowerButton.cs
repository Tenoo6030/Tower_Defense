using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPref;
    [SerializeField] private Sprite towerIcon;
    [SerializeField] private int price;
    [SerializeField] private TMPro.TextMeshProUGUI priceText;

    public GameObject TowerPref => towerPref;
    
    public Sprite TowerIcon => towerIcon;

    public int Price => price;

    private void Start()
    {
        priceText.text = Price.ToString();
    }
}
