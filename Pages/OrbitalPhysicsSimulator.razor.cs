namespace Portfolio.Pages
{
	/// <summary>
	/// Represents a orbital physics simulator object
	/// </summary>
	public partial class OrbitalPhysicsSimulator
	{
		private const string octopusCode = "using UnityEngine;\r\n\r\npublic class Octopus : Character\r\n{\r\n    [Header(\"Horizontal\")]\r\n    [SerializeField]\r\n    private float horizontalSpeed;\r\n    [SerializeField]\r\n    private float maxHorizontal;\r\n\r\n    [Header(\"Vertical\")]\r\n    [SerializeField]\r\n    private float verticalSpeed;\r\n    [SerializeField]\r\n    private float maxVertical;\r\n\r\n    private float initialHorizontal;\r\n    private float initialVertical;\r\n    private float timer;\r\n    private float prevsin;\r\n\r\n    void Start()\r\n    {\r\n        initialHorizontal = transform.position.x;\r\n        initialVertical = transform.position.y;\r\n    }\r\n\r\n    void Update()\r\n    {\r\n        timer += Time.deltaTime;\r\n\r\n        float sin = Mathf.Sin(timer * horizontalSpeed);\r\n        if ((sin > prevsin && !facingRight) || (sin < prevsin && facingRight))\r\n            Flip();\r\n\r\n        float horiz = sin * maxHorizontal + initialHorizontal;\r\n        prevsin = sin;\r\n\r\n        float vert = Mathf.Cos(timer * verticalSpeed) * maxVertical + initialVertical;\r\n\r\n        transform.position = new Vector2(horiz, vert);\r\n    }\r\n}";

		private const string cameraChangerCode = "using UnityEngine;\r\nusing Cinemachine;\r\n\r\npublic class CameraChanger : MonoBehaviour\r\n{\r\n    [SerializeField]\r\n    private CinemachineVirtualCamera mainFollowCam;\r\n\r\n    [SerializeField]\r\n    private CinemachineVirtualCamera newCam;\r\n\r\n    private void OnTriggerEnter2D(Collider2D collision)\r\n    {\r\n        if (collision.gameObject.CompareTag(\"Player\"))\r\n            newCam.Priority = mainFollowCam.Priority + 1;\r\n    }\r\n\r\n    private void OnTriggerExit2D(Collider2D collision)\r\n    {\r\n        if (collision.gameObject.CompareTag(\"Player\"))\r\n            newCam.Priority = mainFollowCam.Priority - 1;\r\n    }\r\n}";

		private const string characterCode = "using UnityEngine;\r\n\r\npublic class Character : MonoBehaviour\r\n{\r\n    [HideInInspector]\r\n    public bool alive = true;\r\n    [HideInInspector]\r\n    public int health;\r\n    [HideInInspector]\r\n    public bool facingRight;\r\n\r\n    [SerializeField]\r\n    protected Healthbar healthbar;\r\n    [SerializeField]\r\n    protected int maxHealth;\r\n\r\n    public void Hit()\r\n    {\r\n        health--;\r\n        healthbar.SetHealth(health, maxHealth);\r\n        \r\n        if (health <= 0)\r\n            Die();\r\n    }\r\n\r\n    public void Flip()\r\n    {\r\n        facingRight = !facingRight;\r\n        transform.Rotate(0f, 180f, 0f);\r\n    }\r\n\r\n    protected void Heal()\r\n    {\r\n        health++;\r\n        if (health > maxHealth)\r\n            health = maxHealth;\r\n        healthbar.SetHealth(health, maxHealth);\r\n    }\r\n\r\n    protected virtual void Die()\r\n    {\r\n        Debug.Log(\"Die\");\r\n        alive = false;\r\n        Destroy(gameObject);\r\n    }\r\n}";


		private readonly List<string> images =
		[
			"images/Orbital Phyics Simulator/3D.png",
			"images/Orbital Phyics Simulator/Multiple Planets.png",
			"images/Orbital Phyics Simulator/Optimisation.png",
		];
	}
}