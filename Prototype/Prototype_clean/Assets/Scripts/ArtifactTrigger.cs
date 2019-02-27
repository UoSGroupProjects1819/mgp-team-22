using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactTrigger : MonoBehaviour
{
    public string Trigger1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player")) PlayerPrefs.SetInt(Trigger1, 1);
    }
}
