using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInScene : MonoBehaviour {

    public Image imageToFade;

	// Use this for initialization
	void Start () {
        StartCoroutine(fadeIn());
	}

    public void fadeTo(string scene) {
        StartCoroutine(fadeOut(scene));
    }
	
	IEnumerator fadeIn() {
        float t = 1f;

        while (t > 0) {
            t -= Time.deltaTime;
            imageToFade.color = new Color(0f, 0f, 0f, t);
            
            yield return 0;
        }
    }

    IEnumerator fadeOut(string scene) {
        float t = 0f;

        while (t < 1f) {
            t += Time.deltaTime;
            imageToFade.color = new Color(0f, 0f, 0f, t);

            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
