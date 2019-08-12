using UnityEngine;

public class back2Game : MonoBehaviour {

	public void infiniteReplays()
    {
        if (Input.GetButtonUp("toInfinityNBeyond") || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
