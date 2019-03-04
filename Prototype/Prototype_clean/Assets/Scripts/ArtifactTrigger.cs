using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactTrigger : MonoBehaviour
{
    public string Trigger1;
    public AudioClip pickupSound;

    public GameObject hum;

    private AudioSource audioMan;
    private ParticleSystem particleSys;

    private void Start()
    {
        audioMan = GetComponent<AudioSource>();
        particleSys = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            PlayerPrefs.SetInt(Trigger1, 1);
            particleSys.Play();
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>()) sprite.enabled = false;

            GetComponentInChildren<Animator>().Play("artifact_text_fade");
            GetComponent<CircleCollider2D>().enabled = false;

            audioMan.PlayOneShot(pickupSound);

            hum.gameObject.SetActive(false);
        }


    }
}
