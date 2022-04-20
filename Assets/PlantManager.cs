using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : Singleton<PlantManager>
{

    int progress;
    public Text progressText;
    Dictionary<string, bool> plantUnlocked = new Dictionary<string, bool>();
    Dictionary<string, bool> plantHinted = new Dictionary<string, bool>();

    public override void Save(SerializedGame save)
    {
        base.Save(save);
        save.progress = progress;
        save.plantUnlocked = plantUnlocked;
        save.plantHinted = plantHinted;
    }
    public override void Load(SerializedGame save)
    {
        base.Load(save);
        progress = save.progress;

        progressText.text = progress + " %";
        plantUnlocked = save.plantUnlocked;
        plantHinted = save.plantHinted;
    }

    public void unlockPlant(string name)
    {
        plantUnlocked[name] = true;
    }

    public void showPlantHint(string name)
    {
        plantHinted[name] = true;

    }

    public bool isPlantHinted(string name)
    {
        return plantHinted.ContainsKey(name) && plantHinted[name];
    }

    public bool isPlantUnlocked(string name)
    {
        return plantUnlocked.ContainsKey(name) && plantUnlocked[name];
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void addProgress(int p)
    {
        progress += p;
        progressText.text = progress + " %";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
