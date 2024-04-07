using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{

    public float Delay = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginSelFdestruction());

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator BeginSelFdestruction()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
