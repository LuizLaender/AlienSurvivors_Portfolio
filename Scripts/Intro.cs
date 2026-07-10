using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float volume;
    [SerializeField] GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
        StartCoroutine(ShowCanvas());
    }

    IEnumerator ShowCanvas()
    {
        yield return new WaitForSeconds(0.64f);
        canvas.SetActive(true);
        audioSource.PlayOneShot(audioClip, volume);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}