using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class deathOnTrigger : MonoBehaviour {

    public GameObject player;
    public ParticleSystem ps;

    void Start()
    {
        ps.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "obstcl")
        { 
            AudioSource audios = player.gameObject.GetComponent<AudioSource>();
            audios.Play();
            StartCoroutine(onOffPS());
        }
    }

    IEnumerator onOffPS()
    {
        ps.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
        ps.Play();
        yield return new WaitForSeconds(0.7f);
        ps.gameObject.GetComponent<ParticleSystem>().enableEmission = false;
    }
}
