using TMPro;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
	Rigidbody2D rb;
	public LineRenderer liner;
	float mouseDownX, mouseDownY;
	bool clicked = false;
	public float force;
	public static float currentY;
	public GameObject platform;
	public TextMeshProUGUI scoreText, highScoreText;
	public static int score;
	private void Start()
	{
		score = 0;
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(SpawnLoop());
	}

	private void Update()
	{
		scoreText.text = score.ToString();
		highScoreText.text = "BEST: " + GameManager.manager.highScore.ToString();
		liner.SetPosition(0, transform.position);
		if (Input.GetMouseButtonDown(0) && rb.velocity == Vector2.zero)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, Mathf.Infinity);
			if(hit.transform != null){
				if(hit.transform.CompareTag("Player")){
					clicked = true;
					Vector2 mousePos = Camera.main.ScreenToWorldPoint(
						Input.mousePosition);
					mouseDownX = mousePos.x;
					mouseDownY = mousePos.y;
					AudioManager.audio.Play("click");
				}
			}


		}

		if (clicked)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
				Input.mousePosition);
			liner.SetPosition(1, mousePos);
		}
		else
		{
			liner.SetPosition(1, transform.position);
		}

		if (Input.GetMouseButtonUp(0) && clicked)
		{
			AudioManager.audio.Play("launch");
			clicked = false;
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(
						Input.mousePosition);
			float forceX = mousePos.x - mouseDownX;
			float forceY = mousePos.y - mouseDownY;
			rb.AddForce(new Vector2(forceX * force, forceY * force));
			GetComponent<Animator>().SetBool("Landed", false);
		}


	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
		}
		if (collision.CompareTag("WallUp"))
		{
			rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
		}
		if (collision.CompareTag("Hell"))
		{
			AudioManager.audio.Play("die");
			SaveSystem.Save();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Platform"))
		{
			rb.velocity = Vector2.zero;
			GetComponent<Animator>().SetBool("Landed", true);
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if (collision.transform.CompareTag("Platform"))
		{
			GetComponent<Animator>().SetBool("Landed", false);
		}
	}

	private IEnumerator SpawnLoop()
	{
		yield return new WaitForSeconds(1f);
		float x = UnityEngine.Random.Range(-10, 10);
		currentY += 5;
		Vector2 newPos = new Vector2(x, currentY);
		Instantiate(platform, newPos, Quaternion.identity);
		yield return SpawnLoop();
	}
}
