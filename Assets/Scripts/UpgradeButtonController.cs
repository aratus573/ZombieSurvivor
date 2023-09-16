using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class UpgradeButtonController : MonoBehaviour
{

    private StatsModifier statsModifier;
    [SerializeField]private UpgradesManager upgradesManager;
    public Text _name;
    public Text _description;
    public Image _icon;
    public int pressedButtonID;
    void Start()
    {

    }

    public void SetData()
    {
        statsModifier = upgradesManager.listOfOptionalStatsModifier[pressedButtonID];
        if (statsModifier != null)
        {
            _name.text = statsModifier.Name;
            _description.text = statsModifier.description;
            _icon.sprite = statsModifier.icon;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
