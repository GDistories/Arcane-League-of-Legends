using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;
    // Start is called before the first frame update
    void Start()
    {
        GameController.canSickle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (GameController.canSickle)
            {
                GameController.canSickle = false;
                Instantiate(sickle, transform.position, transform.rotation);
            }
        }
    }
}
