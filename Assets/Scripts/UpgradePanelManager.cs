using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{

    public UpgradeButtonController button0;
    public UpgradeButtonController button1;
    public UpgradeButtonController button2;


    public void DisplayButton()
    {
        button0.SetData();
        button1.SetData();
        button2.SetData();
    }

}
