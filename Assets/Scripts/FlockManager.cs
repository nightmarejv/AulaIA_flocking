using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
                                                                                                    

    public GameObject fishPrefab;                                                                   //Puxa o objeto prefab do peixe

    public int numFish = 20;                                                                        //Numero de peixes

    public GameObject[] allFish;                                                                    //Cria um array para mais peixes

    public Vector3 swinLimits = new Vector3(5, 5, 5);                                               //Cria um limite de espaço

    public Vector3 goalPos;

    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]                                    
    public float minSpeed = 0;                                 
    [Range(0.0f, 5.0f)]                                                                             //Configura uma velocidade 
    public float maxSpeed = 0;
    [Range(1.0f, 10.0f)]                                                                            //Configura o distanciamento
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]                                                                             //Configura uma velocidade para a rotação
    public float rotationSpeed;



    private void Start()
    {
        allFish = new GameObject[numFish];                 
        for(int i = 0; i < numFish; i++)                                                            //Cria aleatoriamente peixes de acordo com os outros peixes
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }





    private void Update()
    {
        goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));  
        if (Random.Range(0, 100) < 10)                                                            
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), Random.Range(-swinLimits.y, swinLimits.y), Random.Range(-swinLimits.z, swinLimits.z));
        }
    
    }
}
