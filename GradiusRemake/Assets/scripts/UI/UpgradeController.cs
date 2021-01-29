using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType {speed,missile,_double,laser,option,shield };


public class UpgradeController : MonoBehaviour
{
    public GlobalStats stats;
    public ControlsObj controls;
    public UpgradeIndicator[] indicators;
    public int currentSelected;
    public int speedboost,option;
    public bool missile,_double,laser,shield;


    public delegate void GotPower();
    public GotPower optionAction,shieldAction;
    // Start is called before the first frame update
    void Start()
    {
        currentSelected = -1;
        ResetUpgrades();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (stats.paused) return;
        if(Input.GetKeyDown(controls.b))
        {
            if(currentSelected > -1)
            {
                switch ((UpgradeType)currentSelected)
                {
                    case UpgradeType.speed:
                        speedboost++;
                        indicators[(int)currentSelected].ChangeGraphic(0);
                        currentSelected = -1;
                        break;
                    case UpgradeType.missile:
                        if (!missile)
                        {
                            missile = true;
                            indicators[(int)currentSelected].ChangeGraphic(2);
                            currentSelected = -1;
                        }
                        break;
                    case UpgradeType._double:
                        if (!_double)
                        {
                            _double = true;
                            laser = false;
                            indicators[(int)currentSelected].ChangeGraphic(2);
                            indicators[(int)UpgradeType.laser].ChangeGraphic(0);
                            currentSelected = -1;
                        }
                        break;
                    case UpgradeType.laser:
                        if (!laser)
                        {
                            laser = true;
                            _double = false;
                            indicators[(int)currentSelected].ChangeGraphic(2);
                            indicators[(int)UpgradeType._double].ChangeGraphic(0);
                            currentSelected = -1;
                        }
                        break;
                    case UpgradeType.option:
                        if (option < 2)
                        {
                            optionAction();
                            option++;
                            if(option == 2)
                            {
                                indicators[(int)currentSelected].ChangeGraphic(2);
                                currentSelected = -1;
                            }
                            else
                            {
                                indicators[(int)currentSelected].ChangeGraphic(0);
                                currentSelected = -1;
                            }
                        }
                        break;
                    case UpgradeType.shield:
                        if (!shield)
                        {
                            shield = true;
                            shieldAction();
                            indicators[(int)currentSelected].ChangeGraphic(2);
                            currentSelected = -1;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void NextCurrent()
    {
        if (currentSelected == -1)
        {
            currentSelected += 1;
            indicators[(int)currentSelected].ChangeGraphic(indicators[(int)currentSelected].currentImageId + 1);
        }
        else
        {
            indicators[(int)currentSelected].ChangeGraphic(indicators[(int)currentSelected].currentImageId - 1);
            currentSelected += 1;
            currentSelected %= indicators.Length;
            indicators[(int)currentSelected].ChangeGraphic(indicators[(int)currentSelected].currentImageId + 1);
        }
    }

    public void DisableShieldIcon()
    {
        shield = false;
        if (currentSelected == (int)UpgradeType.shield)
        {
            indicators[(int)UpgradeType.shield].ChangeGraphic(1);
        }
        else
        {
            indicators[(int)UpgradeType.shield].ChangeGraphic(0);
        }
    }

    public void ResetUpgrades()
    {
        option = speedboost = 0;
        missile = _double = laser = shield = false;
        ResetGraphics();
    }

    public void DeathReset()
    {
        ResetUpgrades();
        if(currentSelected > -1)
        {
            currentSelected = 0;
        }
        ResetGraphics();
    }

    public void ResetGraphics()
    {        
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].ChangeGraphic(0);
        }
        if (currentSelected == 0)
        {
            indicators[0].ChangeGraphic(1);
        }
    }
    
}
