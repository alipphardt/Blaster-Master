using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] int sceneIndex = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(sceneIndex);
    }

}
