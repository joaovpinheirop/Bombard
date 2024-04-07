using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawn : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public Vector2 timeInterval = new Vector2(1, 1);
    public GameObject spwanPoint;
    public GameObject Target;
    public float rangeInDregress = 10;
    public float heightInDregress = 45;
    public Vector2 force;

    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        // ignore if game is game over
        if (GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;

        if (cooldown < 0f)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);
            Fire();
        }
    }

    void Fire()
    {
        // Get Prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        // Create Bomb
        GameObject bomb = Instantiate(bombPrefab, spwanPoint.transform.position, bombPrefab.transform.rotation);

        // Aply Force
        Rigidbody bombRigidBody = bomb.GetComponent<Rigidbody>();

        Vector3 inpulseVector = Target.transform.position - spwanPoint.transform.position;
        inpulseVector.Scale(new Vector3(1, 0, 1));
        inpulseVector.Normalize();
        inpulseVector += new Vector3(0, heightInDregress / 180f, 0);
        inpulseVector.Normalize();

        inpulseVector = Quaternion.AngleAxis(heightInDregress * Random.Range(-1f, 1f), Vector3.up) * inpulseVector;
        inpulseVector *= Random.Range(force.x, force.y);
        bombRigidBody.AddForce(inpulseVector, ForceMode.Impulse);
    }
}
