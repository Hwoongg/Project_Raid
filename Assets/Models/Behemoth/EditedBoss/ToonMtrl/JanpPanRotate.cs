using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanpPanRotate : MonoBehaviour
{
    [SerializeField] float RotSpeed;
    float randomValue;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(RotSpeed, RotSpeed, 0));

        timer += Time.deltaTime;

        if (timer > 1.0f)
            DrawNumber();
    }

    void DrawNumber()
    {
        Debug.Log("Rot Change");
        timer = 0;
        randomValue = Random.Range(-100, 100);
    }
}
