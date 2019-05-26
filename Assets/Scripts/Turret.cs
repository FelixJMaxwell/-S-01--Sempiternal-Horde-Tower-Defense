using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    //objetivo al que apuntar
    private Transform target;
    //para no sobrecargar el juego a la hora de hacer daño con el laser
    //cargamos el GameObject con el componente Enemy desde aca
    //y lo usamos en updateTarget()
    private Enemy targetEnemy;

    [Header("General")]
    //rango de la torreta
    public string nombreTorreta;
    public bool kinematicBullet = false;
    public bool buffActivated = false;
    public float startRango = 5f, startFireRate = 1f, startTurnSpeed = 10f, startBulletDamage = 5000000f, startExplosionRadiusBullet = 0f, costoBase = 0, reducedUpgradeCostFromSkillTree = 0;
    [HideInInspector]
    public float extraRangoPorUpgrades = 0, extraFireRatePorUpgrades = 0, fireRate, turnSpeed, bulletDamage, rango, buffExtraDmg = 0;
    public float extraDmgPorUpgrades = 0;

    //Extras por nivel
    [HideInInspector]
    public float extraDmgPorSegundoPorNivel = 0, extraDmgPorNivel = 0, extraRalentizacionPorNivel = 0;

    //[HideInInspector]
    public float nivelActualPorExperiencia = 0, experienciaActual = 0, multiplicadorDeExperienciaPorNivel = 0, experienciaSigNivel = 0;

    [Header("Niveles por Upgrades")]
    public float upgradesNivelActual = 0;
    public float costoUpgradeSigNivel = 0;
    [HideInInspector]
    public float fireCountdown = 0f;

    [Header("Area of effect Tower")]
    //RALENTIZADORA
    public bool aoeRalentizacion = false;
    [Header("")]
    public bool experienciaParaRalentizadora = true;
    public float StartPorcentajeDeRalentizacion = .5f;
    public float startExtraRalentizacionPorUpgrades = 0;
    public float startExtraExperienciaPorNivelSlowTurret;
    public float startExtraExperienciaPorUpgradesSlowTurret;
    [HideInInspector]
    public float extraRalentizacionPorUpgrades, extraExperienciaPorNivelSlowTurret, extraExperienciaPorUpgradesSlowTurret;

    [Header("")]
    //DAÑO DE AREA POR SEGUNDO
    public bool aoeDamage = false;
    public float startDanioPorSegundo = 30;
    public float startExtraDmgPorSegundoPorUpgrades = 5;
    [HideInInspector]
    public float porcentajeDeRalentizacion, dañoPorSegundo, extraDmgPorSegundoPorUpgrades = 0;
    //BASE
    public bool aoeDamageAndRalent = false;

    [Header("AoE Buff For Other Towers")]
    public bool aoeBuff = false;
    public float startExtraDmgForBulletsWithBuff = 0.1f;
    public float extraDmgForBulletsWithBuff = 0;
    public bool experienciaParaBuffTower = true;
    public float startExtraExperienciaPorNivelBuffTower = 0.025f;
    public float startExtraExperienciaPorUpgradesBuffTower = 0.5f;
    [HideInInspector]
    public float extraExperienciaPorNivelBuffTower = 0, extraBuffPorNivel = 0, extraExperienciaPorUpgradesBuffTower = 0;

    [Header("Unity Setup")]
    //asignamos el "tag" a una variable para facil acceso
    public string enemyTag = "Enemy";
    //parte de la torreta que rotara
    public Transform partToRotate;
    //lugar de donde sale la bala
    public Transform firePoint;
    //Instancia de la bala
    public GameObject bulletPrefab;

    [Header("Atributos del Mortero")]
    //public float startExplosionRadiusMortarBullet = 5f;
    public float startExplosionDamage = 10f;
    public float startVelocidadDeCaida = -10;
    [Header("Configuraciones Inspector")]
    public GameObject balaDeMorteroGO;
    public Rigidbody balaDeMortero;
    public Transform beacon;
    public float alturaMaxima = 10;
    //public float rango = 5f;
    [Header("GRAVEDAD: VALOR NEGATIVO")]
    public float gravedad = -18;
    public bool estadoLanzamiento = true;
    private Bullet componentBullet;
    public int killCount = 0;
    public bool newCapabilitiesBool = false;
    private AudioSource D1;
    private bool maxVolumen;

    //Skilltreevariables
    [HideInInspector]
    public float dmgFromSkillTree = 0, rangeFromSkillTree = 0, fireRatetFromSkillTree = 0, moreLevelFromSkillTree = 0, extraBuffFromSkillTree = 0;
    [HideInInspector]
    public float extraDmgPrSecondFromSkillTree = 0, extraSlowdownFromSkillTree = 0, extraXplosionRangeFromSkillTree = 0, extraPorcentajeDeExperiencia = 0;
    [HideInInspector]
    public float finalDmgFromSkillTree = 0, finalRangeFromSkillTree = 0, finalFireRateST = 0, finalBuffST = 0, finalDpsST = 0, finalSDST = 0, finalXplosionRST = 0, finalExpPerc = 0, finalRedUpCostST = 0;

    public bool previewTurret = false;

    private PlayerStats playerStatsComp;
    // Use this for initialization
    void Start () {
        D1 = GetComponent<AudioSource>();
        playerStatsComp = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        if (transform.name.Contains("Cubo")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaCubo[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaCubo[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaCubo[2];
            dmgFromSkillTree = playerStatsComp.vTorretaCubo[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaCubo[4];
            rangeFromSkillTree = playerStatsComp.vTorretaCubo[5];
        } else if (transform.name.Contains("MG")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaMG[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaMG[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaMG[2];
            dmgFromSkillTree = playerStatsComp.vTorretaMG[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaMG[4];
            rangeFromSkillTree = playerStatsComp.vTorretaMG[5];
        } else if (transform.name.Contains("Sniper")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaSniper[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaSniper[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaSniper[2];
            dmgFromSkillTree = playerStatsComp.vTorretaSniper[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaSniper[4];
            rangeFromSkillTree = playerStatsComp.vTorretaSniper[5];
        } else if (transform.name.Contains("Trapecio")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaTrapecio[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaTrapecio[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaTrapecio[2];
            dmgFromSkillTree = playerStatsComp.vTorretaTrapecio[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaTrapecio[4];
            rangeFromSkillTree = playerStatsComp.vTorretaTrapecio[5];
        } else if (transform.name.Contains("AoEBase")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaAoEBase[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaAoEBase[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaAoEBase[2];
            extraDmgPrSecondFromSkillTree = playerStatsComp.vTorretaAoEBase[3];
            extraSlowdownFromSkillTree = playerStatsComp.vTorretaAoEBase[4];
            rangeFromSkillTree = playerStatsComp.vTorretaAoEBase[5];
        } else if (transform.name.Contains("AoEBuff")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaAoEBuff[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaAoEBuff[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaAoEBuff[2];
            extraBuffFromSkillTree = playerStatsComp.vTorretaAoEBuff[3];
            rangeFromSkillTree = playerStatsComp.vTorretaAoEBuff[4];
        } else if (transform.name.Contains("AoeDmg")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaAoEDmg[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaAoEDmg[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaAoEDmg[2];
            extraDmgPrSecondFromSkillTree = playerStatsComp.vTorretaAoEDmg[3];
            rangeFromSkillTree = playerStatsComp.vTorretaAoEDmg[4];
        } else if (transform.name.Contains("AoESlow")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaAoESlow[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaAoESlow[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaAoESlow[2];
            extraSlowdownFromSkillTree = playerStatsComp.vTorretaAoESlow[3];
            rangeFromSkillTree = playerStatsComp.vTorretaAoESlow[4];
        } else if (transform.name.Contains("PesadaBase")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaPesadaB[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaPesadaB[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaPesadaB[2];
            dmgFromSkillTree = playerStatsComp.vTorretaPesadaB[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaPesadaB[4];
            rangeFromSkillTree = playerStatsComp.vTorretaPesadaB[5];
        } else if (transform.name.Contains("Canon")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaCanon[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaCanon[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaCanon[2];
            dmgFromSkillTree = playerStatsComp.vTorretaCanon[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaCanon[4];
            rangeFromSkillTree = playerStatsComp.vTorretaCanon[5];
        } else if (transform.name.Contains("Mortero")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaMortero[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaMortero[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaMortero[2];
            dmgFromSkillTree = playerStatsComp.vTorretaMortero[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaMortero[4];
            extraXplosionRangeFromSkillTree = playerStatsComp.vTorretaMortero[5];
        } else if (transform.name.Contains("Missile")) {
            moreLevelFromSkillTree = playerStatsComp.vTorretaMissileLauncher[0];
            extraPorcentajeDeExperiencia = playerStatsComp.vTorretaMissileLauncher[1];
            reducedUpgradeCostFromSkillTree = playerStatsComp.vTorretaMissileLauncher[2];
            dmgFromSkillTree = playerStatsComp.vTorretaMissileLauncher[3];
            fireRatetFromSkillTree = playerStatsComp.vTorretaMissileLauncher[4];
            rangeFromSkillTree = playerStatsComp.vTorretaMissileLauncher[5];
            extraXplosionRangeFromSkillTree = playerStatsComp.vTorretaMissileLauncher[6];
        }

        StartPorcentajeDeRalentizacion = StartPorcentajeDeRalentizacion / 100;
        finalDmgFromSkillTree = startBulletDamage * (dmgFromSkillTree / 100);
        finalRangeFromSkillTree = startRango * (rangeFromSkillTree / 100);
        finalFireRateST = startFireRate * (fireRatetFromSkillTree / 100);
        finalBuffST = startExtraDmgForBulletsWithBuff * (extraBuffFromSkillTree / 100);
        finalDpsST = startDanioPorSegundo * (extraDmgPrSecondFromSkillTree / 100);
        finalSDST = StartPorcentajeDeRalentizacion * (extraSlowdownFromSkillTree / 100);
        finalXplosionRST = startExplosionRadiusBullet * (extraXplosionRangeFromSkillTree / 100);
        finalExpPerc = extraPorcentajeDeExperiencia / 10;

        if (moreLevelFromSkillTree > 0) {
            multiplicadorDeExperienciaPorNivel += moreLevelFromSkillTree;
        }

        nivelActualPorExperiencia = multiplicadorDeExperienciaPorNivel;
        experienciaSigNivel = Mathf.Round((multiplicadorDeExperienciaPorNivel * multiplicadorDeExperienciaPorNivel + multiplicadorDeExperienciaPorNivel + (multiplicadorDeExperienciaPorNivel * 0.5f)) * 3.128f);

        if (upgradesNivelActual == 0) {
            costoUpgradeSigNivel = costoBase * 0.8f;
        } else if (reducedUpgradeCostFromSkillTree > 0) {
            float costoTemp = costoBase * (0.8f * (upgradesNivelActual + 1));
            costoUpgradeSigNivel = costoTemp - (costoTemp * (reducedUpgradeCostFromSkillTree / 100));
        } else {
            costoUpgradeSigNivel = costoBase * (0.8f * (upgradesNivelActual + 1));
        }

        startFireRate += finalFireRateST;
        startRango += finalRangeFromSkillTree;
        rango = startRango;
        fireRate = startFireRate;
        turnSpeed = startTurnSpeed;
        bulletDamage = startBulletDamage;
        porcentajeDeRalentizacion = StartPorcentajeDeRalentizacion;
        dañoPorSegundo = startDanioPorSegundo;
        extraDmgForBulletsWithBuff = startExtraDmgForBulletsWithBuff;

        /*
         * Pal mortero
         */
        if (kinematicBullet) {
            GameObject temporaryBalaMor = Instantiate(balaDeMorteroGO);
            temporaryBalaMor.name = transform.name + "BalaMortero";
            temporaryBalaMor.transform.parent = transform;
            temporaryBalaMor.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            balaDeMortero = temporaryBalaMor.GetComponent<Rigidbody>();
            balaDeMortero.useGravity = false;
            componentBullet = temporaryBalaMor.GetComponent<Bullet>();
            if (transform.Find(transform.name + "Beacon")) {
                return;
            }
            GameObject temporaryBeacon = Instantiate(beacon.gameObject);
            temporaryBeacon.transform.parent = transform;
            temporaryBeacon.name = transform.name + "Beacon";
            beacon = temporaryBeacon.GetComponent<Transform>();
            beacon.transform.position = transform.position + new Vector3(0, 0, 2.0f);
        }
        
        //llamado a updateTarget para no cargar demasiado al update
        InvokeRepeating("updateTarget", 0f, 0.5f);
	}

    //buscamos objetivos cada x cantidad de tiempo para no sobrecargar todo
    void updateTarget() {
        if (enemyTag == "") {
            return;
        }

        //creamos un array con todos los enemigos en el mapa
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //distancia hasta el enemigo mas cercano, puesta en infinito por defecto
        float shortestEnemyDistance = Mathf.Infinity;
        //por defecto no tenemos un enemigo mas cercano, dentro del rango
        GameObject nearestEnemy = null;
        //por cada enemigo en el array anterior, realizaremos una comprobacion de la distancia
        //para saber si esta dentro del rango
        foreach (GameObject enemy in enemies) {
            //para saber la distancia hasta el enemigo
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //si la distancia al enemigo es menor que la distancia mas corta entonces
            //encontramos a nuestro enemigo para apuntar
            if (distanceToEnemy < shortestEnemyDistance) {
                shortestEnemyDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //si nearestEnemy es diferente de null, es decir encontramos un enemigo y
        //su distancia es menor que nuestro rango, significando que esta dentro del rango de la torreta
        //asignamos la posicion de ese enemigo como objetivo
        if(nearestEnemy != null && shortestEnemyDistance <= rango) {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else {
            target = null;
        }
    }

    public void LevelUp(float _multiplicadorDeExperienciaPorNivel) {

        experienciaSigNivel = Mathf.Round((_multiplicadorDeExperienciaPorNivel * _multiplicadorDeExperienciaPorNivel + _multiplicadorDeExperienciaPorNivel + (_multiplicadorDeExperienciaPorNivel * 0.5f)) * 3.128f);

        if (nivelActualPorExperiencia == 1) {
            return;
        } else {
            if (aoeDamageAndRalent) {
                if (porcentajeDeRalentizacion >= 0.35f) {
                    return;
                } else {
                    extraRalentizacionPorNivel = StartPorcentajeDeRalentizacion * (nivelActualPorExperiencia / 25);
                }
                extraDmgPorSegundoPorNivel = startDanioPorSegundo * (nivelActualPorExperiencia / 25);
            } else if (aoeDamage) {
                extraDmgPorSegundoPorNivel = startDanioPorSegundo * (nivelActualPorExperiencia / 25);
            } else if (aoeBuff) {
                extraExperienciaPorNivelBuffTower = startExtraExperienciaPorNivelBuffTower * (nivelActualPorExperiencia / 0.2f);
                extraBuffPorNivel += (startExtraDmgForBulletsWithBuff / 100);
            } else if (aoeRalentizacion) {
                if (porcentajeDeRalentizacion >= 0.65f) {
                    return;
                }else {
                    extraRalentizacionPorNivel = StartPorcentajeDeRalentizacion * (nivelActualPorExperiencia / 25);
                }
                extraExperienciaPorNivelSlowTurret = startExtraExperienciaPorNivelSlowTurret * (nivelActualPorExperiencia / 2.5f);
            } else {
                extraDmgPorNivel = startBulletDamage * (nivelActualPorExperiencia / 300);
            }
        }
    }

    public void newCapabilities() {
        newCapabilitiesBool = false;
        rango = startRango + extraRangoPorUpgrades;
        fireRate = startFireRate + extraFireRatePorUpgrades;
    }

    // Update is called once per frame
    void Update () {
        if (previewTurret) {
            return;
        }

        if (experienciaActual >= experienciaSigNivel) {
            nivelActualPorExperiencia += 1;
            multiplicadorDeExperienciaPorNivel += 1;
            experienciaActual = 0;
            LevelUp(multiplicadorDeExperienciaPorNivel);
        }

        if (newCapabilitiesBool) {
            newCapabilities();
        }

        //Asignacion de valores daño con los valores START
        if (buffActivated) {
            bulletDamage = startBulletDamage + extraDmgPorNivel + extraDmgPorUpgrades + finalDmgFromSkillTree + (startBulletDamage * buffExtraDmg);
            porcentajeDeRalentizacion = StartPorcentajeDeRalentizacion + extraRalentizacionPorNivel + extraRalentizacionPorUpgrades + finalSDST + (StartPorcentajeDeRalentizacion * extraDmgForBulletsWithBuff);
            dañoPorSegundo = startDanioPorSegundo + extraDmgPorSegundoPorNivel + extraDmgPorSegundoPorUpgrades + finalDpsST + (startDanioPorSegundo * buffExtraDmg);
        } else {
            bulletDamage = startBulletDamage + extraDmgPorNivel + extraDmgPorUpgrades + finalDmgFromSkillTree;
            porcentajeDeRalentizacion = StartPorcentajeDeRalentizacion + extraRalentizacionPorNivel + extraRalentizacionPorUpgrades + finalSDST;
            dañoPorSegundo = startDanioPorSegundo + extraDmgPorSegundoPorNivel + extraDmgPorSegundoPorUpgrades + finalDpsST;
        }

        if (kinematicBullet) {
            //Codigo para el mortero
            gravedad = startVelocidadDeCaida;
            //Si hay mas de 0 enemigos vivos Y encontramos un objeto con el nombre de este objeto + "Beacon"
            // Y luego si estadoLanzamiento es verdadero llamamos al metodo Lanzamiento()
            if (WaveSpawner.enemiesAlive > 0 && transform.Find(transform.name + "Beacon")) {
                if (estadoLanzamiento) {
                    lanzamiento();
                }
            }
            return;
        }
        if (aoeRalentizacion) {
            aoeRalenEnemy();
            return;
        } else if (aoeDamage) {
            aoeDamageEnemy();
            return;
        } else if (aoeDamageAndRalent) {
            aoeDmgRalent();
            return;
        } else if (aoeBuff) {
            extraDmgForBulletsWithBuff = startExtraDmgForBulletsWithBuff + finalBuffST + extraBuffPorNivel;
            buffForTowers();
            return;
        } else {
            if (target == null) {
                return;
            }
            //pa apuntar al enemigo
            lookOnTarget();
            if (fireCountdown <= 0f) {
                shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void lookOnTarget() {
        //hacia donde vamos a apuntar, y calculos para hacer rotar la torreta o una parte de ella
        //asignar todo esto a "PartToRotate" GameObject
        Vector3 directionToAim = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToAim);
        //Vector3 rotation = lookRotation.eulerAngles;
        //Para un mejor giro de la torreta al apuntar a un nuevo enemigo
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDestroy() {
        Collider[] towersInRange = Physics.OverlapSphere(transform.position, rango);
        foreach(Collider tower in towersInRange) {
            if(tower.tag == "Tower") {
                buffDeactivation(tower.transform);
            }
        }
    }

    void buffDeactivation(Transform tower) {
        Turret towerComponent = tower.GetComponent<Turret>();
        if (towerComponent != null) {
            towerComponent.buffActivated = false;
        }
    }

    void buffForTowers() {

        Collider[] turretsInRange = Physics.OverlapSphere(transform.position, rango);
        foreach (Collider turret in turretsInRange) {
            if (turret.tag == "Tower") {
                Turret towerComponent = turret.GetComponent<Turret>();
                if(towerComponent != null) {
                    towerComponent.buffActivated = true;
                    towerComponent.buffExtraDmg = extraDmgForBulletsWithBuff;
                }
                if (experienciaParaBuffTower) {
                    StartCoroutine(experienciaPorNivelParaBuffTurret());
                }
            }
        }
    }

    IEnumerator experienciaPorNivelParaBuffTurret() {
        experienciaParaBuffTower = false;
        transform.GetComponent<Turret>().experienciaActual += 1 + extraExperienciaPorNivelBuffTower + extraExperienciaPorUpgradesBuffTower + finalExpPerc;
        yield return new WaitForSeconds(1);
        experienciaParaBuffTower = true;
    }

    void aoeRalenEnemy() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, rango);
        foreach (Collider enemy in enemiesInRange) {

            Enemy enemyComp = enemy.GetComponent<Enemy>();
            float extraExpFromEnemies = 0;

            if (enemyComp != null) {
                extraExpFromEnemies = (enemyComp.experiencia / 10) + (enemyComp.experiencia * finalExpPerc);
            }

            if (enemy.tag == "Enemy") {
                ralentizacion(enemy.transform);
                if (experienciaParaRalentizadora) {
                    StartCoroutine(experienciaParaRalentizadoraTurret(extraExpFromEnemies));
                }
            }
        }
    }

    IEnumerator experienciaParaRalentizadoraTurret(float _EnemyExperience) {
        experienciaParaRalentizadora = false;
        transform.GetComponent<Turret>().experienciaActual += 1 + extraExperienciaPorNivelSlowTurret + extraExperienciaPorUpgradesSlowTurret + _EnemyExperience;
        yield return new WaitForSeconds(1);
        experienciaParaRalentizadora = true;
    }

    void ralentizacion(Transform enemy) {
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null) {
            enemyComponent.ralentizacion(porcentajeDeRalentizacion);
        }
    }

    void aoeDamageEnemy() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, rango);
        foreach (Collider enemy in enemiesInRange) {
            if (enemy.tag == "Enemy") {
                dmgPorSegundoEnemigos(enemy.transform);
            }
        }
    }

    void dmgPorSegundoEnemigos(Transform enemy) {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, rango);
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        Turret turretComponent = transform.GetComponent<Turret>();
        if (enemyComponent != null) {
            float distancia = Vector3.Distance(enemyComponent.transform.position, transform.position);
            enemyComponent.takeDamage(dañoPorSegundo * Time.deltaTime);
            if (enemyComponent.health <= 0) {
                turretComponent.experienciaActual += enemyComponent.experiencia + (enemyComponent.experiencia * turretComponent.finalExpPerc);
                turretComponent.killCount += 1;
            }
            if (distancia >= rango) {
                float dañoTotal = enemyComponent.startHealth - (enemyComponent.health - dañoPorSegundo * Time.deltaTime);
                turretComponent.experienciaActual += (enemyComponent.experiencia * ( dañoTotal / enemiesInRange.Length) / (enemiesInRange.Length * enemiesInRange.Length)) + (enemyComponent.experiencia * turretComponent.finalExpPerc);
            }
        }
    }

    void aoeDmgRalent() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, rango);
        foreach (Collider collider in enemiesInRange) {
            if (collider.tag == "Enemy") {
                dmgRalent(collider.transform);
            }
        }
    }

    void dmgRalent(Transform enemy) {
        //TORRETA AOE BASE 
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, rango);
        Turret turretComponent = transform.GetComponent<Turret>();
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null) {
            float distancia = Vector3.Distance(enemyComponent.transform.position, transform.position);
            enemyComponent.takeDamage(dañoPorSegundo * Time.deltaTime);
            enemyComponent.ralentizacion(porcentajeDeRalentizacion);
            if (enemyComponent.health <= 0) {
                turretComponent.experienciaActual += enemyComponent.experiencia + (enemyComponent.experiencia * turretComponent.finalExpPerc);
                turretComponent.killCount += 1;
            }
            if (distancia >= rango) {
                float dañoTotal = enemyComponent.startHealth - (enemyComponent.health - dañoPorSegundo * Time.deltaTime);
                turretComponent.experienciaActual += (enemyComponent.experiencia * dañoTotal / (enemiesInRange.Length * enemiesInRange.Length)) + (enemyComponent.experiencia * turretComponent.finalExpPerc);
            }
        }
    }

    void shoot() {
        //referencia para el script bullett.cs
        if (D1 != null) {
            if (maxVolumen) {
                D1.volume = 2;
            } else {
                D1.volume = Random.Range(0.5f, 1);
            }
            D1.maxDistance = Random.Range(15, 20);
            D1.Play();
        }
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Con la siguiente linea cambio el nombre de la bala para darle el nombre de la
        //torreta que la instancio bulletGO.name = this.name;
        bulletGO.transform.parent = this.transform;
        //instancia de la bala
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        //revisar que la bala no este vacia y llamar funcion seek para que la bala siga al enemigo
        if (bullet != null) {
            bullet.seek(target);
        }
    }

    /*
     * 
     * Codigo pal mortero
     * 
     */
    

    void lanzamiento() {
        //cambiamos la gravedad utilizada por Unity para poder establecer un valor nosotros mismos
        Physics.gravity = Vector3.up * gravedad;
        //si estadoLanzamiento es verdadero, habilitamos el uso de gravedad en el rigidbody de la bala
        //y asigamos la velocidad igual al resultado de CalcularVelocidadDeLanzamiento()

        if (D1.enabled) {
            if (maxVolumen) {
                D1.volume = 1;
            } else {
                D1.volume = 0.05f;
            }
            D1.maxDistance = Random.Range(5, 50);
            D1.Play();
        }

        if (estadoLanzamiento) {
            balaDeMortero.useGravity = true;
            balaDeMortero.velocity = CalcularVelocidadDeLanzamiento();
        }
    }

    Vector3 CalcularVelocidadDeLanzamiento() {
        //Seteamos estadoLanzamiento en falso para evitar que en el ciclo Update se este llamando
        //una ves cada frame, si no ponemo esta variable en falso la bala del mortero subira infinitamente
        estadoLanzamiento = false;
        float desplazamientoY = beacon.position.y - balaDeMortero.position.y;
        Vector3 desplazamientoXZ = new Vector3(beacon.position.x - balaDeMortero.position.x, 0, beacon.position.z -
            balaDeMortero.position.z);
        Vector3 velocidadY = Vector3.up * Mathf.Sqrt(-2 * gravedad * alturaMaxima);
        Vector3 velocidadXZ = desplazamientoXZ / (Mathf.Sqrt(-2 * alturaMaxima / gravedad) + Mathf.Sqrt(2 *
            (desplazamientoY - alturaMaxima) / gravedad));
        return velocidadXZ + velocidadY;
    }
    //TERMINA CODIGO DL MORTERO
    //TERMINA CODIGO DL MORTERO
    //TERMINA CODIGO DL MORTERO

    //Codigo para dibujar el "rango de la torreta en EDITMODE de unity
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //se dibuja en la escena de Unity un circulo que muestra el rango, alrededor
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}