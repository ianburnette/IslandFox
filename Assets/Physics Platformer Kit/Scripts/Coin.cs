using UnityEngine;

//class to add to collectible coins
[RequireComponent(typeof(SphereCollider))]
public class Coin : MonoBehaviour 
{

	public int seedType;
	public int seedSubType;

	//1 = random generic
	//2 = vine seed
	//3 = boat seed
	//4 = island seed
	//5 = house seed
	public GameObject particles;

	public GUIManager gui;									//GUIManager script to update with "coins collected"
	public AudioClip collectSound;							//sound to play when coin is collected
	public Vector3 rotation = new Vector3(0, 80, 0);		//idle rotation of coin
	public Vector3 rotationGain = new Vector3(10, 20, 10);	//added rotation when player gets near coin 
	public float startSpeed = 3f;							//how fast coin moves toward player when they get near
	public float speedGain = 0.2f;							//how fast coin accelerates toward player when they're near
	
	private bool collected;
	public Transform player;
	private TriggerParent triggerParent;	//this is a utility class, that lets us check if the player is close to the coins "bounds sphere trigger"
	
	//setup
	void Awake()
	{
		particles = Resources.Load ("getSeed") as GameObject;
		if(tag != "Coin")
		{
			tag = "Coin";
			Debug.LogWarning ("'Coin' script attached to object not tagged 'Coin', tag added automatically", transform);
		}
		GetComponent<Collider>().isTrigger = true;
		triggerParent = GetComponentInChildren<TriggerParent>();
		//if no trigger bounds are attached to coin, set them up
		if(!triggerParent)
		{
			GameObject bounds = new GameObject();
			bounds.name = "Bounds";
			bounds.AddComponent<SphereCollider>();
			bounds.GetComponent<SphereCollider>().radius = 7f;
			bounds.GetComponent<SphereCollider>().isTrigger = true;
			bounds.transform.parent = transform;
			bounds.transform.position = transform.position;
			bounds.AddComponent<TriggerParent>();
			triggerParent = GetComponentInChildren<TriggerParent>();
			triggerParent.tagsToCheck = new string[1];
			triggerParent.tagsToCheck[0] = "Player";
			Debug.LogWarning ("No pickup radius 'bounds' trigger attached to coin: " + transform.name + ", one has been added automatically", bounds);
		}
	}
	
	void Start()
	{
		player = GameObject.Find("Player").transform;
		if (seedType == 0) {
			collected = true;
		}
	}
	
	//move coin toward player when he is close to it, and increase the spin/speed of the coin
	void Update()
	{
		transform.Rotate (rotation * Time.deltaTime, Space.World);
		
		if(triggerParent.collided)
			collected = true;
		
		if (collected)
		{
			startSpeed += speedGain;
			rotation += rotationGain;
			transform.position = Vector3.Lerp (transform.position, player.position, startSpeed * Time.deltaTime);
		}	
	}
	
	//give player coin when it touches them
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || other.tag == "Boat") {
			CoinGet ();
			player = other.transform;
		}
	}
	
	void CoinGet()
	{
		print ("getting type " + seedType);
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if (seedType == 0) {
			player.GetComponent<PlayerInventory> ().GetSeedSmall (seedSubType);
			//print ("setup small seed associations");
		} else if (seedType == 1) {
			player.SendMessage ("GetVineSeed");
		} else if (seedType == 2) {
			player.SendMessage ("GetBoatSeed");
		} else if (seedType == 3) {
			player.SendMessage ("GetIslandSeed");
		} else if (seedType == 4) {
			player.SendMessage ("GetHouseSeed");
		} else if (seedType == 5) {
			player.SendMessage ("GetMastSeed");
		}

		Instantiate (particles, transform.position, Quaternion.identity);
		if (gui)
			gui.coinsCollected ++;
		Destroy(gameObject);
	}
}
