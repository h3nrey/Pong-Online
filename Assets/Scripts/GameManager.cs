using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager game;

    [Header("Create Prefabs")]
    [SerializeField] GameObject[] prefabs;
    [SerializeField] Transform[] prefabsPoints;

    [Header("UI")]
    [SerializeField] TMP_Text scoreLText, scoreRText;
    float scoreL, scoreR;
    [SerializeField] GameObject pauseCanvas;
    public bool paused = false;

    // Start is called before the first frame update
    void Start() {
        game = this;
        CreateElements();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            TogglePauseCanvas();
        }
        print(Time.timeScale);
    }

    public void CreateElements() {
        for(int i = 0; i < prefabs.Length; i++) {
            Instantiate(prefabs[i], prefabsPoints[i].position, Quaternion.identity);
        }
    }

    public void restartElements() {
        StartCoroutine(restart());

        IEnumerator restart() {
            yield return new WaitForSeconds(0.3f);
            GameObject.FindGameObjectsWithTag("paddle")[0].transform.position = prefabsPoints[0].position;
            GameObject.FindGameObjectsWithTag("paddle")[1].transform.position = prefabsPoints[2].position;
            GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameObject.FindGameObjectWithTag("ball").transform.position = prefabsPoints[1].position;
            yield return new WaitForSeconds(0.4f);
            GameObject.FindGameObjectWithTag("ball").GetComponent<BallMovement>().LauchBall();
        }
    }

    public void ScoreUpdate(string tag) {
        if(tag == "goalL") {
            scoreL++;
            scoreLText.text = scoreL.ToString();
        } else if(tag == "goalR") {
            scoreR++;
            scoreRText.text = scoreR.ToString();
        }
    }

    public void TogglePauseCanvas() {
        if(paused){
            pauseCanvas.SetActive(false);
            paused = false;
            RunTime(1);
        } else if(!paused) {
            pauseCanvas.SetActive(true);
            paused = true;
            RunTime(0);
        }
    }


    private void RunTime(int time) {
        Time.timeScale = time;
    }
}
