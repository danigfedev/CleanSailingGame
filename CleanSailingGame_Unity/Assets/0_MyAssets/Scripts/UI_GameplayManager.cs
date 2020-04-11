using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameplayManager : MonoBehaviour
{
    public BoatController currentBoatController;
    public GameStatusData gameData;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI cargoText;
    public Image waterHealthImage;

    private int frameStep = 3;
    private int frameCount = 0;
    private BoatPropierties currentBoatProperties;

    private void Start()
    {
        SetBoatProperties();
    }

    private void Update()
    {
        frameCount++;
        if (frameCount == frameStep)
        {
            frameCount = 0;
            UpdateUI();
        }
    }

    public void SetBoatProperties()
    {
        currentBoatProperties = currentBoatController.boatPropierties;
    }

    private void UpdateUI()
    {
        levelText.text = "Lvl " + gameData.currentLevel;
        objectiveText.text = gameData.cargoObjective.ToString();
        waterHealthImage.fillAmount = gameData.waterHealth;
        cargoText.text = currentBoatProperties.currentCargo + " / " + currentBoatProperties.maxCargoCapacity;
        

    }

}
