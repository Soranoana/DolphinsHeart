using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogcolorGet : MonoBehaviour {

    private GameObject UnderWaterCameraEffects;
    void Start () {

        UnderWaterCameraEffects = GameObject.Find("UnderWaterCameraEffects");
        transform.GetComponent<Renderer>().material.color = UnderWaterCameraEffects.GetComponent<AQUAS_LensEffects>().underWaterParameters.fogColor;
    }

    private void Awake()
    {
    }

    void Update () {

    }
}
