using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCaller : MonoBehaviour{

    float sceneCooldown;

    public void CallScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(call());
        IEnumerator call() {
            yield return new WaitForSeconds(sceneCooldown);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void getCoolDown(float cooldown) {
        sceneCooldown = cooldown;
    }
}
