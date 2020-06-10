using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SetParticle = RankController;
public class Effect_test : BaseCompornent
{
    ParticleSystem particle = null;
    

    private SetParticle setparticle; 

    private void Start()
    {
        GameObject Rank = GameObject.Find("Rank");
        setparticle = Rank.GetComponent<RankController>();
        particle = GetComponent<ParticleSystem>();
        particle.Pause();
    }

    public void Update()
    {
        if (setparticle.ParticleFlg)
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
        Debug.Log("3");
        yield return null;
    }

    public void ParticleFlgOn()
    {

        Debug.Log("0");
    }

}