using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodLettingEvent : MonoBehaviour
{
    public bool on = true;
    public bool leech = false;

    public GameObject Shadow;
    public Canvas UI;
    public ParticleSystem PlayerBlood;
    private List<GameObject> Shadows;
    private float ShadowTimer = 0;

    void Start()
    {
        Shadows = new List<GameObject>();
    }

    void Update()
    {
        if (on)
        {
            if (leech)
            {
                blood();
                on = false;
                leech = false;
            }
            else scary();
        }
    }

    void scary()
    {
        if(ShadowTimer <= 0)
        {
            GameObject newShadow = Instantiate(Shadow);
            newShadow.transform.SetParent(UI.transform);
            
            Shadows.Add(newShadow);

            ShadowTimer = Random.Range(1, 5);
        }
        else ShadowTimer -= Time.deltaTime;
    }

    void blood()
    {
        foreach(GameObject shadow in Shadows) { Destroy(shadow); }
        PlayerBlood.Play();
    }
}
