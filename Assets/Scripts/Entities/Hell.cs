using UnityEngine;

public class Hell: MonoBehaviour{

  private Vector3 startPos;
  public float amplitude;
  private void Update(){
    startPos = transform.localPosition;
    float yPos = Mathf.Sin(Time.time) * amplitude;
    transform.localPosition = startPos + new Vector3(0, yPos , 0);
  }
}
