using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI f, g, h;

    public TextMeshProUGUI F { get { f.gameObject.SetActive(true); return f; } set => f = value; }
    public TextMeshProUGUI G { get { g.gameObject.SetActive(true); return g; } set => g = value; }
    public TextMeshProUGUI H { get { h.gameObject.SetActive(true); return h; } set => h = value; }
}
