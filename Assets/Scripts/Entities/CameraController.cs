using UnityEngine;
﻿using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	private void Update()
	{
		transform.Translate(Vector3.up * 1.5f * Time.deltaTime);
	}
}
