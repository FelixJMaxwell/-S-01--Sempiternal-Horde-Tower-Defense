using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    
    public float startSpeed = 10f;
    public float startHealth = 100;
    public float startValue = 50;
    public float startScore = 50;
    public float startExperiencia = 5;
    public static float extraValue;
    //[HideInInspector]
    public float speed, health , experiencia;
    public float value, score;
    public GameObject deathEffect;
    public GameObject enemyHealthBarHolder;
    public string experienciaParaElBeacon;
    private bool enemyIsDead = false;
    public bool increasePower = false;
    public PlayerStats playerStatsComp;
    public static int increaseMultiplier = 0;

    private void Start() {
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        float _multiplicadorDificultad = playerStatsComp.multiplicadorDeDificultad;

        if (playerStatsComp.multiplicadorDeDificultad > 1) {
            startHealth = startHealth * _multiplicadorDificultad;
            startScore = startScore * _multiplicadorDificultad;
            startValue = startValue * _multiplicadorDificultad;
        }

        extraValue = playerStatsComp.valoresUsuario[1];

        speed = startSpeed;
        health = startHealth;
        score = startScore;
        value = startValue + ((startValue * extraValue) / 100);

        if (increasePower) {
            //temer cuidado al modificar esto #LOLOLOLOLOLOLOLOL
            //hacer tabla en excel :V

            mejorarEnemigo(
                //masVida
                increaseMultiplier * (Random.Range(3f, 5f)),
                //masScore
                increaseMultiplier * Mathf.FloorToInt(Random.Range(0.25f, 0.96f)),
                //masDinero
                increaseMultiplier * Mathf.FloorToInt(Random.Range(0.07f, 0.1f)),
                //masExperiencia
                increaseMultiplier * Random.Range(0.75f, 1f));
        }
    }

    public void mejorarEnemigo(float masVida, int masScore, int masValor, float masExperiencia) {
        health += masVida;
        score += masScore;
        value += masValor;
        experiencia += masExperiencia;
    }


    public void takeDamage(float amount) {
        health -= amount;
        enemyHealthBarHolder.transform.localScale = new Vector3(1, 1, (health/startHealth)*2);

        if (health <= 0 && !enemyIsDead) {
            enemyDie();
        }
    }

    public void ralentizacion(float percentage) {
        speed = startSpeed * (1f - percentage);
    }

    void enemyDie() {
        enemyIsDead = true;
        playerStatsComp.Money += value;
        playerStatsComp.playerScore += score;
        GameObject dEffect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(dEffect, 2f);
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }

}