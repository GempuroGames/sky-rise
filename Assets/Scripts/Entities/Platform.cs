using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class Platform : MonoBehaviour
{
	private Rigidbody2D rb;
	public static float speed = 0.5f;
	public PlatformType type = PlatformType.SOLID;
	public bool initialPlatform;
	public Sprite fragileSprite;
	private void Start()
	{
		GameManager.manager.RaiseScore(1);
		rb = GetComponent<Rigidbody2D>();
		if (!initialPlatform)
		{
			type = (PlatformType)Random.Range(0, 2);

		}else{
			Player.currentY = transform.position.y;
		}

		if(type == PlatformType.SOLID){
			if(initialPlatform){
				return;
			}
			GetComponent<SpriteRenderer>().color = Color.white;
		}else{
			GetComponent<SpriteRenderer>().sprite = fragileSprite;
		}
	}

	private void Update()
	{

		if (transform.position.y < Camera.main.transform.position.y - 6.9f)
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator OnCollisionEnter2D(Collision2D other){
		if(other.transform.CompareTag("Player") && type == PlatformType.FRAGILE){
			GetComponent<Animator>().SetTrigger("Touch");
			yield return new WaitForSeconds(3f);
			Destroy(gameObject);
		}
		yield return null;
	}
}

public enum PlatformType
{
	SOLID,
	FRAGILE
}
