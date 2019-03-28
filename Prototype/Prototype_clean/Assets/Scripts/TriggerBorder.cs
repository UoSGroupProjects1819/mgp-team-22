using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBorder : MonoBehaviour
{

    GameObject player;
    SpriteRenderer sprite;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        float distance = Vector3.Distance(transform.position, playerPos);
        sprite.color = new Color(255, 255, 255, (1f - (distance/6)));

    }
}
