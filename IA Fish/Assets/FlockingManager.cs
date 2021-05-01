using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    //prefab do peixe que é instanciado na cena
    public GameObject fishprefab;
    //variavel que indica quantos peixes vão ser criados, pode ser alterada pra mais ou menos
    public int numFish = 20;
    //array que guarda os peixes instanciados
    public GameObject[] allfish;
    //variavel que determinar o tamanho minimo e maximo da onde os peixes vão nascer
    public Vector3 swinLimits = new Vector3(5, 5, 5);

    //variaveis com slider que determinam a velocidade minima e maxima que os peixes vão se locomover
    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    void Start()
    {
        // declaração que referencia o numero de peixes instanciados ao peixes que foram guardos no array
        allfish = new GameObject[numFish];
        //condição de repeticão para o instaciamento dos peixes
        for (int i = 0; i < numFish; i++)
        {
            //variavel da posição para ser usado no instantiate, que usa os swinlimits como parametros
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
            //cada gameobject de allfish é um "i" do nosso indice, e cada um é instanciado num local aleatorio
            allfish[i] = (GameObject)Instantiate(fishprefab, pos, Quaternion.identity);
            //em cada peixe, ele pega seu script de flock e declara como dele.
            allfish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
