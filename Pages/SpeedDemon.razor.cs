using System;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Threading;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Portfolio.Pages
{
	/// <summary>
	/// Represents a speed demon object
	/// </summary>
	public partial class SpeedDemon
	{
		private const string bulletSpawnerCode = "private void Spawn()\r\n{\r\n\tGameObject bullet = GetObject();\r\n\tif (bullet != null)\r\n\t{\r\n\t\tbullet.transform.SetPositionAndRotation(transform.position + transform.forward, transform.rotation);\r\n\t\tbullet.SetActive(true);\r\n\t}\r\n\r\n\tspawnTimer = 0;\r\n}\r\n\r\nprivate GameObject GetObject()\r\n{\r\n\tforeach (GameObject obj in pooledBullets)\r\n\t{\r\n\t\tif (!obj.activeInHierarchy)\r\n\t\t{\r\n\t\t\treturn obj;\r\n\t\t}\r\n\t}\r\n\r\n\tvar newBullet = Instantiate(bullet);\r\n\tnewBullet.SetActive(false);\r\n\tpooledBullets.Add(newBullet);\r\n\treturn newBullet;\r\n}";

		private const string cameraCode = "void Update()\r\n{\r\n\tif (gameController.finish)\r\n\t\tzoomLevel = 5f;\r\n\r\n\tcameraOrbit = (gameController.isSlowMo && gameController.slowMoTimer - minSlowMo > 0) || gameController.finish || gameController.dead;\r\n\r\n\tmouseX = Input.GetAxisRaw(\"Mouse X\") * multiplierLeftRight;\r\n\tmouseY = Input.GetAxisRaw(\"Mouse Y\") * multiplierUp;\r\n\r\n\txRotation += mouseX;\r\n\tyRotation -= mouseY;\r\n\r\n\tyRotation = Mathf.Clamp(yRotation, yClamping.x, yClamping.y);\r\n\r\n\tif (!cameraOrbit)\r\n\t{\r\n\t\ttransform.position = player.transform.position + cameraOffset;\r\n\r\n\t\ttransform.localEulerAngles = Vector3.right * yRotation;\r\n\t\tplayer.transform.Rotate(Vector3.up * mouseX);\r\n\r\n\t\treturn;\r\n\t}\r\n\r\n\tVector3 newRotation = new(yRotation, xRotation);\r\n\r\n\tcurrentRotation = Vector3.SmoothDamp(currentRotation, newRotation, ref timeToSnap, smoothness);\r\n\tplayer.gameObject.transform.localEulerAngles = new Vector3(0, currentRotation.y, 0);\r\n\ttransform.localEulerAngles = new Vector3(currentRotation.x, 0, currentRotation.z);\r\n\r\n\ttransform.position = target.position + Vector3.up * 2 - transform.forward * zoomLevel;\r\n}";

		private const string playerCode = "private void Update()\r\n{\r\n\tif (gameController.dead || gameController.finish)\r\n\t\treturn;\r\n\r\n\tisGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);\r\n\r\n\tif (Input.GetKeyDown(KeyCode.Space) && isGrounded)\r\n\t{\r\n\t\trigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);\r\n\t\tmoving = true;\r\n\t}\r\n\r\n}\r\n\r\nprivate void FixedUpdate()\r\n{\r\n\tmoving = false;\r\n\r\n\tif (gameController.dead || gameController.finish)\r\n\t\treturn;\r\n\r\n\tGetRayCasts();\r\n\tKeyPressUpdate();\r\n\tSlowMoUpdate();\r\n\tControlDrag();\r\n}";


		private readonly List<string> images =
		[
			"images/Speed Demon/Corridor.png",
			"images/Speed Demon/Prefab Pooling.png",
			"images/Speed Demon/Final Stretch.png",
		];
	}
}