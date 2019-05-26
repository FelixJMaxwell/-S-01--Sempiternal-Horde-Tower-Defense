using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public static class saveLoadManager{
    
    public static void savePlayer(PlayerStats pStats) {
        BinaryFormatter bFormat = new BinaryFormatter();
        FileStream fStream = new FileStream(Application.persistentDataPath + "/player.sh", FileMode.Create);

        playerData pData = new playerData(pStats);

        bFormat.Serialize(fStream, pData);

        fStream.Close();
        Debug.Log("Guardado completado");
    }


    public static float[] loadFloatPlayer() {
        if (File.Exists(Application.persistentDataPath + "/player.sh")) {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(Application.persistentDataPath + "/player.sh", FileMode.Open);

            playerData pData = bFormatter.Deserialize(fStream) as playerData;
            fStream.Close();
            Debug.Log("Carga completada load");
            return pData.stats;
        } else {
            Debug.Log("Carga completada load");
            return null;
        }
        
    }
    
}

[Serializable]
public class playerData {

    public float[] stats;

    public float[] levelCheck;
    public float[] levelAssignations;

    public float[] valoresUnitarios;
    public float[] valoresUsuario;
    public float[] vTorretaCubo;
    public float[] vTorretaMG;
    public float[] vTorretaSniper;
    public float[] vTorretaTrapecio;
    public float[] vTorretaAoEBase;
    public float[] vTorretaAoEBuff;
    public float[] vTorretaAoEDmg;
    public float[] vTorretaAoESlow;
    public float[] vTorretaPesadaB;
    public float[] vTorretaCanon;
    public float[] vTorretaMortero;
    public float[] vTorretaMissileLauncher;

    public bool alternarZoom = false;
    public bool alternarMov = false;
    public float velocidadCamara = 0;

    private int contador = 7;
    private int sumaPrevia = 0;

    public playerData(PlayerStats pStats) {

        stats = new float[200];

        levelCheck = new float[5];
        levelAssignations = new float[5];

        valoresUnitarios = new float[100];
        valoresUsuario = new float[4];
        vTorretaCubo = new float[6];
        vTorretaMG = new float[6];
        vTorretaSniper = new float[6];
        vTorretaTrapecio = new float[6];
        vTorretaAoEBase = new float[6];
        vTorretaAoEBuff = new float[5];
        vTorretaAoEDmg = new float[5];
        vTorretaAoESlow = new float[5];
        vTorretaPesadaB = new float[6];
        vTorretaCanon = new float[6];
        vTorretaMortero = new float[6];
        vTorretaMissileLauncher = new float[7]; //74

        levelCheck[0] = 1;
        levelCheck[1] = 2;
        levelCheck[2] = 3;
        levelCheck[3] = 4;
        levelCheck[4] = 5;

        //stats[0] = pStats.goldAmount;
        stats[1] = pStats.silverAmount;

        for (int i = 0; i < levelCheck.Length; i++) {
            if (pStats.nivelesDesbloqueados[i].Contains(levelCheck[i].ToString())) {
                levelAssignations[i] = levelCheck[i];
            }
        }

        stats[2] = levelAssignations[0];
        stats[3] = levelAssignations[1];
        stats[4] = levelAssignations[2];
        stats[5] = levelAssignations[3];
        stats[6] = levelAssignations[4];

        for (int i = 0; i < valoresUnitarios.Length; i++) {
            valoresUnitarios[i] = pStats.valoresUnitarios[i];
        }
        for (int i = 0; i < valoresUsuario.Length; i++) {
            valoresUsuario[i] = pStats.valoresUsuario[i];
        }
        for (int i = 0; i < vTorretaCubo.Length; i++) {
            vTorretaCubo[i] = pStats.vTorretaCubo[i];
        }
        for (int i = 0; i < vTorretaMG.Length; i++) {
            vTorretaMG[i] = pStats.vTorretaMG[i];
        }
        for (int i = 0; i < vTorretaSniper.Length; i++) {
            vTorretaSniper[i] = pStats.vTorretaSniper[i];
        }
        for (int i = 0; i < vTorretaTrapecio.Length; i++) {
            vTorretaTrapecio[i] = pStats.vTorretaTrapecio[i];
        }
        for (int i = 0; i < vTorretaAoEBase.Length; i++) {
            vTorretaAoEBase[i] = pStats.vTorretaAoEBase[i];
        }
        for (int i = 0; i < vTorretaAoEBuff.Length; i++) {
            vTorretaAoEBuff[i] = pStats.vTorretaAoEBuff[i];
        }
        for (int i = 0; i < vTorretaAoEDmg.Length; i++) {
            vTorretaAoEDmg[i] = pStats.vTorretaAoEDmg[i];
        }
        for (int i = 0; i < vTorretaAoESlow.Length; i++) {
            vTorretaAoESlow[i] = pStats.vTorretaAoESlow[i];
        }
        for (int i = 0; i < vTorretaPesadaB.Length; i++) {
            vTorretaPesadaB[i] = pStats.vTorretaPesadaB[i];
        }
        for (int i = 0; i < vTorretaCanon.Length; i++) {
            vTorretaCanon[i] = pStats.vTorretaCanon[i];
        }
        for (int i = 0; i < vTorretaMortero.Length; i++) {
            vTorretaMortero[i] = pStats.vTorretaMortero[i];
        }
        for (int i = 0; i < vTorretaMissileLauncher.Length; i++) {
            vTorretaMissileLauncher[i] = pStats.vTorretaMissileLauncher[i];
        }

        for (int i = 0; i < valoresUnitarios.Length; i++) {
            stats[7 + i] = valoresUnitarios[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < valoresUsuario.Length; i++) {
            stats[sumaPrevia + i] = valoresUsuario[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaCubo.Length; i++) {
            stats[sumaPrevia + i] = vTorretaCubo[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaMG.Length; i++) {
            stats[sumaPrevia + i] = vTorretaMG[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaSniper.Length; i++) {
            stats[sumaPrevia + i] = vTorretaSniper[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaTrapecio.Length; i++) {
            stats[sumaPrevia + i] = vTorretaTrapecio[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaAoEBase.Length; i++) {
            stats[sumaPrevia + i] = vTorretaAoEBase[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaAoEBuff.Length; i++) {
            stats[sumaPrevia + i] = vTorretaAoEBuff[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaAoEDmg.Length; i++) {
            stats[sumaPrevia + i] = vTorretaAoEDmg[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaAoESlow.Length; i++) {
            stats[sumaPrevia + i] = vTorretaAoESlow[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaPesadaB.Length; i++) {
            stats[sumaPrevia + i] = vTorretaPesadaB[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaCanon.Length; i++) {
            stats[sumaPrevia + i] = vTorretaCanon[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaMortero.Length; i++) {
            stats[sumaPrevia + i] = vTorretaMortero[i];
            contador++;
        }
        sumaPrevia = contador;
        for (int i = 0; i < vTorretaMissileLauncher.Length; i++) {
            stats[sumaPrevia + i] = vTorretaMissileLauncher[i];
            contador++;
        }
        sumaPrevia = 0;
        contador = 7;

        if (pStats.alternarZoom) {
            stats[180] = 1;
        } else {
            stats[180] = 0;
        }

        if (pStats.alternarMov) {
            stats[181] = 1;
        } else {
            stats[181] = 0;
        }

        stats[182] = pStats.velocidadCamara;

        Debug.Log("Carga completada");
    }
}