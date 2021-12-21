using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantOnePage : MonoBehaviour
{
    public string plantName;
    public TMP_Text plantDescriptionLabel;
    public Button plantHintButton;
    public TMP_Text plantHintLabel;
    public Image plantImage;

    public void init()
    {
        bool showHint = PlantManager.Instance.isPlantHinted(plantName);
        if (PlantManager.Instance.isPlantUnlocked(plantName))
        {

            plantImage.color = Color.white;
            plantHintLabel.gameObject.SetActive(true);
            plantDescriptionLabel.gameObject.SetActive(true);
            plantHintButton.gameObject.SetActive(false);
        }
        else
        {
            plantImage.color = Color.black;
            plantHintLabel.gameObject.SetActive(showHint);
            plantDescriptionLabel.gameObject.SetActive(false);
            plantHintButton.gameObject.SetActive(!showHint);
        }
    }

    public void clickHintButton()
    {
        PlantManager.Instance.showPlantHint(plantName);
        init();
    }
    // Start is called before the first frame update
    void Start()
    {
        plantHintButton.onClick.AddListener(delegate { clickHintButton(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {

        if (plantName == "")
        {
            plantName = name;
        }
    }
}
