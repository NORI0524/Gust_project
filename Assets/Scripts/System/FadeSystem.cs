using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSystem : MonoBehaviour
{
    ParticleSystem particle = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        particle.Stop();
    }

    public void ChangeScene()
    {
        StartCoroutine("PushStart");
    }

    IEnumerator PushStart()
    {
        particle.Play();
        yield return new WaitForSeconds(5);

        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        yield return new WaitForSeconds(2);
        //ゲーム画面動作

        yield return null;
    }
}