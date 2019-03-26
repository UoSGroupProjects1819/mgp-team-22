using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactListener : MonoBehaviour


{
    public GameObject[] ToggleElementsOn1, ToggleElementsOff1, ToggleElementsOn2, ToggleElementsOff2;
    private int worldValue1, worldValue2;

    private Vector3 SpawnLocation;

    public string Toggle1, Toggle2;

    // Start is called before the first frame update
    void Start()
    {
        worldValue1 = PlayerPrefs.GetInt(Toggle1);
        worldValue2 = PlayerPrefs.GetInt(Toggle2);

        foreach (GameObject element in ToggleElementsOn1)
        {
            if(worldValue1 == 1) element.SetActive(true);
            if (worldValue1 == 0) element.SetActive(false);
        }

        foreach (GameObject element in ToggleElementsOff1)
        {
            if (worldValue1 == 1) element.SetActive(false);
            if (worldValue1 == 0) element.SetActive(true);
        }

        foreach (GameObject element in ToggleElementsOn2)
        {
            if (worldValue1 == 1) element.SetActive(true);
            if (worldValue1 == 0) element.SetActive(false);
        }

        foreach (GameObject element in ToggleElementsOff2)
        {
            if (worldValue1 == 1) element.SetActive(false);
            if (worldValue1 == 0) element.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //worldValue1 = PlayerPrefs.GetInt(Toggle1);
        //worldValue2 = PlayerPrefs.GetInt(Toggle2);

        //if (worldValue1 == 1 && ToggleElements1 != null) ToggleElements1.SetActive(true);
        //if (worldValue1 == 0 && ToggleElements1 != null) ToggleElements1.SetActive(false);

        //if (worldValue2 == 1 && ToggleElements2 != null) ToggleElements2.SetActive(true);
        //if (worldValue2 == 0 && ToggleElements2 != null) ToggleElements2.SetActive(false);
    }
}
