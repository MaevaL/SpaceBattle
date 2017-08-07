using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;
    public int lifeValue;

    private GameController gameController;
    private LifeController lifeController;
    public GameObject bonusLife;
    public GameObject bonusFireRate;
    public GameObject bonusInvincible;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
            if (gameController == null) {
                Debug.Log("Cannot Find 'GameController' script");
            }
        }

        GameObject lifeControllerObject = GameObject.FindWithTag("LifeController");
        if (lifeControllerObject != null) {
            lifeController = lifeControllerObject.GetComponent<LifeController>();
            if (lifeController == null) {
                Debug.Log("Cannot Find 'LifeController' script");
            }
        }

    }

    void OnTriggerEnter(Collider collider) {
        /*
         * without that asteroid is destroyed at the very 1st frame
         * by boundary's which declench our triggerEnter
         */
        if (collider.CompareTag ("Boundary") || collider.CompareTag("Enemy") || collider.CompareTag("BonusLife")) {       return;     }

        /*
         * Explosion is instantiated at the position and the rotation
         * defined for the asteroïd
         */
        if (explosion != null) { 	Instantiate(explosion, transform.position, transform.rotation); 	}

        if (collider.CompareTag("PlayerBolt")) {
            float random = Random.Range(1, 12);
            if(random == 1)
                Instantiate(bonusLife, transform.position, Quaternion.identity);
            else if (random == 2)
                Instantiate(bonusFireRate, transform.position, Quaternion.identity);
            else if (random == 3)
                Instantiate(bonusInvincible, transform.position, Quaternion.identity);
            Destroy(collider.gameObject);

            /*
             * Destroy gameobject's script attach and his children
             */
            Destroy(gameObject);

			gameController.addScore(scoreValue);
        }

        if (collider.CompareTag("Player")) {
            if(!lifeController.getInvincible()) {
                gameController.setLife(lifeValue);
                if (lifeController.getLife() <= 0) {
                    Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
                    gameController.GameOver();

                    Destroy(collider.gameObject);
                    Destroy(gameObject);
                }
            }
        }

    }
}
