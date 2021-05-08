using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //variavel pra instanciar o flocking manager
    public FlockingManager myManager;
    //velocidade do peixe
    float speed;
    bool turning = false;
    void Start()
    {
        //referencia da variavel local de velocida a velocidade que será escolhida no manager
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //limita o espaço dos peixes
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        //criação do raycast dos peixes
        RaycastHit hit = new RaycastHit();
        //direção dos peixes
        Vector3 direction = myManager.transform.position - transform.position;

        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))//usa o raycast para não acontecer a colisão com o pilar
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else turning = false;

        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            //define a velocidade dos peixes de forma aleatoria
            if (Random.Range(0, 100) < 10) speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0, 100) < 20) ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        //pega a variavel allfish do flocking manager
        GameObject[] gos;
        gos = myManager.allfish;
        //calcula o ponto medio entre os peixes
        Vector3 vcentre = Vector3.zero;
        //evita a colisão entrei os peixes
        Vector3 vavoid = Vector3.zero;
        //aciona a movimentação do grupo
        float gSpeed = 0.01f;
        //float da distancia entre os peixe
        float nDistance;
        //tamanho do grupo
        int groupSize = 0;
        //pega os peixes da matriz referenciada ao allfish
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    //verifica a distancia e evitar as colisões entre os peixes
                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)//verifica se o grupo é maior que 0 é aplica as rotações dos peixes
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}