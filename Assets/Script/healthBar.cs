using UnityEngine;

public class healthBar : MonoBehaviour {

	public shapes player;
	public Transform foregroundSprite;
	public SpriteRenderer forgroundRender;
	public Color MaxHealthColor = new Color (225 / 225f, 63 / 225f, 63 / 255f);
	public Color MinHealthColor = new Color (64 / 225f, 137 / 225f, 225 / 255f);

	 // Update is called once per frame
	void Update () {
		var healthPercent = player.health / (float) player.maxHealth;
		foregroundSprite.localScale = new Vector3 (healthPercent, 1, 1);
		forgroundRender.color = Color.Lerp (MaxHealthColor, MinHealthColor, healthPercent);
	}
}
