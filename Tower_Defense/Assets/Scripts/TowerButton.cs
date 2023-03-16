using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPref;
    [SerializeField] private Sprite towerIcon;

    public GameObject TowerPref => towerPref;
    
    public Sprite TowerIcon => towerIcon;
}
