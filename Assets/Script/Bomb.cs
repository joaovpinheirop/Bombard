using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject PrefabExplosion;
    public GameObject WoodBreakPrefab;
    public float ExplosionDelay = 5f;
    public float blastRadius = 2.5f;
    public int blastDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCuroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ExplosionCuroutine()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        Explode();
    }

    private void Explode()
    {
        // Create Explosion
        Instantiate(PrefabExplosion, transform.position, transform.rotation);

        // Destroy Plataform
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
        {
            // GameObject Coliider
            GameObject hitObject = collider.gameObject;

            if (hitObject.CompareTag("Plataform"))
            {
                // Life Object
                Life life = hitObject.GetComponent<Life>();

                if (life != null)
                {
                    //Distance 
                    float distance = (hitObject.transform.position - transform.position).magnitude;

                    //Damage
                    float distanceRate = Mathf.Clamp(distance / blastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                    int damage = (int)Mathf.Ceil(damageRate * blastDamage);
                    life.health -= damage;

                    if (life.health <= 0)
                    {
                        Instantiate(WoodBreakPrefab, hitObject.gameObject.transform.position, hitObject.gameObject.transform.rotation);
                        Destroy(collider.gameObject);
                    }
                }

            }
        }
        // Destroy Bomb
        Destroy(gameObject);
    }
}
