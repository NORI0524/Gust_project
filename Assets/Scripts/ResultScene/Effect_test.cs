using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_test : BaseCompornent
{
    ParticleSystem particle = null;
    bool Flg = false;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Pause();
    }

    public void Update()
    {
        if (Flg == true)
        {
            StartCoroutine("Particle");
            Debug.Log("1");
        }
    }

    IEnumerator Particle()
    {
        particle.Play();
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        particle.Stop();
        Debug.Log("2");
        Flg = false;
        Debug.Log("3");
        yield return null;
    }

    public void ParticleFlgOn()
    {
        Flg = true;
        Debug.Log("0");
    }

}