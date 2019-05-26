using UnityEngine;

public class levelSelector : MonoBehaviour {

    public fadeInScene fadeToLevel;

	public void LevelSelection(string levelName) {
        fadeToLevel.fadeTo(levelName);
    }
}
