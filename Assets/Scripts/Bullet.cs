using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    //objetivo que sera perseguido por la bala
    private Transform target;
    //referencia al sistema de particulas para cuando la bala golpee algo
    public GameObject impactEffect;
    //velocidad de la bala
    public float bulletSpeed = 50f;
    //radio de explosion, poner en cero en codigo
    //agregar valor en el inspector por cada bala
    public float explosionRadius = 0f;
    //daño que realiza la bala al enemigo
    //publico para poder dar a cada bala un valor diferente de daño
    public float bulletDamage;
    private Turret turretComponent;
    public bool kinematicBullet = false;
    public GameObject missileSound;

    //funcion para segeuir al objetivo
    public void seek(Transform _target) {
        target = _target;
    }

    private void Start() {

        if (missileSound != null) {
            AudioSource rango = missileSound.GetComponent<AudioSource>();
            rango.maxDistance = Random.Range(8, 20);
        }

        turretComponent = transform.parent.GetComponent<Turret>();
        bulletDamage = turretComponent.bulletDamage;
        explosionRadius = turretComponent.startExplosionRadiusBullet + turretComponent.finalXplosionRST;
    }

    // Update is called once per frame
    void Update () {

        if (kinematicBullet) {
            return;
        }

        //si el objetivo(enemigo) desaparece sin ser tocado por la bala, la bala se destruye, desaparece
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        //direction de la bala respecto del objetivo
        Vector3 direction = target.position - transform.position;
        //distancia a movernos en el frame actual
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        //la distancia de la bala al objetivo es igual a direction.magnitude
        //si direction.magnitude es menor o igual que distancethisframe significa que tocamos al objetivo
        if (direction.magnitude <= distanceThisFrame) {
            hitTarget();
            return;
        }
        //para mover la bala, en este punto todavia no la hemos tocado
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        //tomando la funcionalidad de que la torreta mire/apunte al objetivo que dispara
        //se lo agregamos a la bala, para que apunte al objetivo que persigue, de este modo
        //el misil o cualquier otra bala que tenga punta como cabeza, vera dicha apuntada al objetivo
        transform.LookAt(target);
	}

    void hitTarget() {
        //instanciacion de las particulas cuando una bala toque un objetivo
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //destruccion de las particulas luego de 2s
        Destroy(effectIns, 1f);

        if (missileSound != null && !kinematicBullet) {
            missileSound.GetComponent<AudioSource>().maxDistance = Random.Range(10, 20);
            missileSound.GetComponent<AudioSource>().volume = Random.Range(0.4f, 1);
            GameObject sonido = Instantiate(missileSound, transform.position, Quaternion.identity);
            Destroy(sonido, 1);
        }

        if (explosionRadius > 0f) {
            explode();
        } else {
            Damage(target);
        }

        Destroy(gameObject);
    }

    //metodo para realizar la cuenta del daño que realizamos
    void Damage(Transform enemy) {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if(enemyComponent != null) {
            enemyComponent.takeDamage(bulletDamage);
            if (enemyComponent.health <= 0 && enemyComponent.experienciaParaElBeacon.Contains(transform.parent.name)) {
                //Experiencia para el mortero
                turretComponent.experienciaActual += enemyComponent.experiencia + (enemyComponent.experiencia * turretComponent.finalExpPerc);
            } else if (enemyComponent.health <= 0 && turretComponent.tag == "Tower" && turretComponent.kinematicBullet == false) {
                //Experiencia para todas las torretas excepto AoE y mortero
                turretComponent.experienciaActual += enemyComponent.experiencia + (enemyComponent.experiencia * turretComponent.finalExpPerc);
                turretComponent.killCount += 1;
            } else if (enemyComponent.health >= 0 && turretComponent.tag == "Tower" && turretComponent.kinematicBullet == false) {
                turretComponent.experienciaActual += (enemyComponent.experiencia / 3) + (enemyComponent.experiencia * turretComponent.finalExpPerc);
            } else {
                //Experiencia obtenida por atacar al enemigo y no matarlo
                turretComponent.experienciaActual += (enemyComponent.experiencia * 0.05f) + (enemyComponent.experiencia * turretComponent.finalExpPerc);
            }
        }
    }

    //se listan los colliders existentes dentro del rango de la bala(si es que tiene rango)
    //y aquellos con el tag de enemigo se ven afectados por el daño de la explosion
    void explode() {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidersInRange) {
            if (collider.tag == "Enemy") {
                Damage(collider.transform);
            }
        }
    }

    /*
     * 
     * Codigo Bala de mortero
     * 
     */

    private void OnTriggerEnter(Collider other) {
        if (other.name == transform.parent.name + "Beacon" || other.tag == "Floor") {
            transform.gameObject.SetActive(false);
            GameObject explosion = Instantiate(impactEffect, transform.position, transform.rotation);

            if (missileSound != null) {
                missileSound.GetComponent<AudioSource>().maxDistance = Random.Range(10, 20);
                missileSound.GetComponent<AudioSource>().volume = Random.Range(0.01f, 0.1f);
                GameObject sonido = Instantiate(missileSound, transform.position, Quaternion.identity);
                Destroy(sonido, 1);
            }

            explode();
            Destroy(explosion, 0.5f);
            transform.position = transform.parent.position + new Vector3(0, 0.5f, 0);
            transform.gameObject.SetActive(true);
            StartCoroutine(esperaEntreDisparosDeMortero());
            GetComponent<Rigidbody>().Sleep();
        }
    }

    IEnumerator esperaEntreDisparosDeMortero() {
        yield return new WaitForSeconds(1);
        //cambiamos la variable estadoLanzamiento a verdadero del objeto padre de esta bala de mortero
        //parentComponent.estadoLanzamiento = true;
        turretComponent.estadoLanzamiento = true;
    }



    //dibuja un gizmo alrededor de la bala si esta tiene un rango mayor a cero
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
