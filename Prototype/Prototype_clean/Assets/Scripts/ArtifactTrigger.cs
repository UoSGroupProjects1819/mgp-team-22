using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactTrigger : MonoBehaviour
{
    public string Trigger1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Play();
        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) sprite.enabled = false;
        if (collision.gameObject.tag == ("Player")) PlayerPrefs.SetInt(Trigger1, 1);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
