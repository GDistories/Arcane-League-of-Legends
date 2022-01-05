using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
