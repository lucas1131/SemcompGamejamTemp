using UnityEngine;
using System.Collections;

public class ManageBars : MonoBehaviour {

    public GameObject healthBar;
    public GameObject sanityBar;
    public GameObject realityBar;
    public GameObject player;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerHealth();
	}

    void UpdatePlayerHealth()
    {
        Player playerScript = player.GetComponent<Player>();

        RectTransform rt = healthBar.GetComponent<RectTransform>();
        rt.localScale = new Vector3(playerScript.hitpoints / 100, rt.localScale.y, rt.localScale.z);

        rt = sanityBar.GetComponent<RectTransform>();
        rt.localScale = new Vector3(playerScript.sanity / 100, rt.localScale.y, rt.localScale.z);

        rt = realityBar.GetComponent<RectTransform>();
        rt.localScale = new Vector3(RealityController.rs / 100, rt.localScale.y, rt.localScale.z);
    }
}
