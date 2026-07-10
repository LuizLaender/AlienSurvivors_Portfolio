using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] Vector3 moveSpeed;
    bool isRollingCredits = false;

    void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (isRollingCredits)
        {
            gameObject.transform.position = gameObject.transform.position + moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.8f);
        isRollingCredits = true;
        yield return new WaitForSeconds(27);
        SceneManager.LoadScene("MainMenu");
    }
}
