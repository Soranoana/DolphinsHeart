using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeParticle : MonoBehaviour {

    private float fadePoint;
    private GameObject player;
    private int fade;
    private int paintArea;//描画を始める範囲
    private ParticleSystem myParticle;
    private Vector3 target; //playerとtransform距離ベクトル

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myParticle = GetComponent<ParticleSystem>();
        fade = 0;
        paintArea = 100;
    }

    void Update()
    {
        if (transform.name == "particleSouth" || transform.name == "particleNorth")
        {
            //fadePoint = paintArea - Mathf.Abs(player.transform.position.z - transform.position.z) + 5;
            target = player.transform.position - transform.position;
            target.y = 0;
            target.x = 0;
            fadePoint = (float)paintArea - target.magnitude+5;

            if (fadePoint >= 0)
            {
                fade = (int)(100 * (fadePoint / paintArea));
                target = player.transform.position - transform.position;
                target.y = 0;
                target.x = 0;
                fadePoint = (float)paintArea - target.magnitude + 5;
                myParticle.Play();
            }
            else
            {
                fade = 0;//透明
                myParticle.Clear();
                myParticle.Stop();
            }
        }
        if (transform.name == "particleWest" || transform.name == "particleEast")
        {
            //fadePoint = paintArea - Mathf.Abs(player.transform.position.x - transform.position.x) + 5;
            target = player.transform.position - transform.position;
            target.y = 0;
            target.z = 0;
            fadePoint = (float)paintArea - target.magnitude + 5;

            if (fadePoint >= 0)
            {
                fade = (int)(100*(fadePoint / paintArea));
                myParticle.Play();
            }
            else
            {
                fade = 0;//透明
                myParticle.Clear();
                myParticle.Stop();
            }
        }
        //        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1.0f * fade / 100);
        GetComponent<ParticleSystem>().startLifetime = (float)(100 * fade/100);
        
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.name == "wallSouth")
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, 245);
            }
            else if (transform.name == "wallNorth")
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -245);
            }
            else if (transform.name == "wallWest")
            {
                other.transform.position = new Vector3(245, other.transform.position.y, other.transform.position.z);
            }
            else if (transform.name == "wallEast")
            {
                other.transform.position = new Vector3(-245, other.transform.position.y, other.transform.position.z);
            }
        }
    }*/
}
