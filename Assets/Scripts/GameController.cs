using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;

    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText lifeText;

    private LifeController lifeController;

    private bool gameOver;
    private bool restart;

    private int score;

    void Start() {

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";

        score = 0;
        UpdateScore();

        GameObject lifeControllerObject = GameObject.FindWithTag("LifeController");
        if (lifeControllerObject != null) {
            lifeController = lifeControllerObject.GetComponent<LifeController>();
            UpdateLife();
            if (lifeController == null) {
                Debug.Log("Cannot Find 'LifeController' script");
            }
        }
        
        StartCoroutine (SpawnWaves());
    }

    void Update() {
        if(restart) {
            if(Input.GetKeyDown(KeyCode.R)) {
                /*
                * Load the level (scene files) specified between parenthesis
                * Application.loadedLevel load the current scene
                * Application.LoadLevel(Application.loadedLevel);
                * Obsolete
                */

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

	IEnumerator SpawnWaves() {

        yield return new WaitForSeconds(startWait);
        while (true) {
            
            for (int i = 0; i < hazardCount; i++) {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity; // no rotation
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }


        }
          
    }


    void UpdateScore() {
        scoreText.text = "Score : " + score;
    }

    void UpdateLife() {
        lifeText.text = "Santé : " + lifeController.getLife() + "%";
    }

    public void setLife(int newValue) {
        lifeController.setLife(newValue);
        UpdateLife();
    }

    public void setInvisibility(bool value) {
        lifeController.setInvicibility(value);
    }
    public void addScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }
 
    public void GameOver() {
        gameOverText.text = "GAME OVER";
        gameOver = true;
    }
}
