using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactListener : MonoBehaviour


{
    public GameObject ToggleElements1, ToggleElements2;
    private int worldValue1, worldValue2;

    private Vector3 SpawnLocation;

    public string Toggle1, Toggle2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        worldValue1 = PlayerPrefs.GetInt(Toggle1);
        worldValue2 = PlayerPrefs.GetInt(Toggle2);

        if (worldValue1 == 1) ToggleElements1.SetActive(true);
        if (worldValue1 == 0) ToggleElements1.SetActive(false);

        if (worldValue2 == 1) ToggleElements2.SetActive(true);
        if (worldValue2 == 0) ToggleElements2.SetActive(false);
    }
}
