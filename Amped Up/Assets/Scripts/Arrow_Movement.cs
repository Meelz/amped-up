using UnityEngine;
using System.Collections;

public class Arrow_Movement : MonoBehaviour {

    public GameObject arrowBack;
    public GameObject particle;

    private Step_Generator gen;
    private float arrowSpeed = 0;
    private Score_Handler scoreHandler;
    private Heartbeat_Controller heartbeatController;
    private bool scoreApplied = false;

    public direction dir;
    public enum direction {  a, b, spc, c, d };

    //strum offset should be 0.8f for 1080p
    private const float strumOffset = 0.8f; //0.075f;
    private const float despawnTime = 1.5f;

	// Use this for initialization
	void Start () {
        gen = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Step_Generator>();
        scoreHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score_Handler>();
        heartbeatController = GameObject.FindGameObjectWithTag("Player").GetComponent<Heartbeat_Controller>();

        switch(GetComponent<SpriteRenderer>().sprite.name)
        {
            case "arrowsheet_a":
                dir = direction.a;
                break;
            case "arrowsheet_b":
                dir = direction.b;
                break;
            case "arrowsheet_spc":
                dir = direction.spc;
                break;
            case "arrowsheet_c":
                dir = direction.c;
                break;
            case "arrowsheet_d":
                dir = direction.d;
                break;
        }
	}

    // Update is called once per frame
    void Update() {

        arrowSpeed = gen.arrowSpeed;
        Vector3 tempPos = transform.position;
        tempPos.y -= arrowSpeed;
        transform.position = tempPos;

        if (Input.GetKeyDown(KeyCode.D) && dir == direction.a)
        {
            CheckLocation();
        }

        if (Input.GetKeyDown(KeyCode.F) && dir == direction.b)
        {
            CheckLocation();
        }

        if (Input.GetKeyDown(KeyCode.Space) && dir == direction.spc)
        {
            CheckLocation();
        }

        if (Input.GetKeyDown(KeyCode.J) && dir == direction.c)
        {
            CheckLocation();
        }
        if (Input.GetKeyDown(KeyCode.K) && dir == direction.d)
        {
            CheckLocation();
        }

        //Missed
        if (transform.position.y < arrowBack.transform.position.y - strumOffset)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.0f, 0.0f));
            StartCoroutine(DespawnArrow());
            if (!scoreApplied)
            {
                scoreApplied = true;
                scoreHandler.SendMessage("LoseScore");
                scoreHandler.SendMessage("LoseCombo");
                heartbeatController.SendMessage("loseLife");
            }
        }
    }

    void CheckLocation()
    {
        if (transform.position.y >= arrowBack.transform.position.y - strumOffset && transform.position.y <= arrowBack.transform.position.y + strumOffset)
        {
            arrowBack.GetComponent<Animator>().SetBool("isLit", true);
            scoreHandler.SendMessage("AddScore");
            scoreHandler.SendMessage("AddCombo");
            heartbeatController.SendMessage("gainLife");
            Vector3 particleSpawn = new Vector3(arrowBack.transform.position.x, arrowBack.transform.position.y, transform.position.z);
            Instantiate(particle, particleSpawn, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DespawnArrow()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }
}
