using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour{
  public TextMeshProUGUI highScoreText;

  private void Start(){
    AudioManager.audio.Play("splash");
    highScoreText.text = "Best: " + GameManager.manager.highScore.ToString();
  }

  private void Update(){
    if(Input.GetMouseButton(0)){
      AudioManager.audio.Play("select");
      SceneManager.LoadScene("Game");
    }
  }
}
