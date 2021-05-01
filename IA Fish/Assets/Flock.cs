using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //variavel pra instanciar o flocking manager
    public FlockingManager myManager;
    //velocidade do peixe
    float speed;
    void Start()
    {
        //referencia da variavel local de velocida a velocidade que será escolhida no manager
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //realiza a movimentação do peixe
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
