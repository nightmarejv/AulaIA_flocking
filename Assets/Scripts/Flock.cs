using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;                                                               //Pega o flockmanager

    public float speed;                                                                          //Aplica uma velocidade para os peixes

    bool turning = false;


    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);                            //Puxa as configurações feitas de velocidade do manager para o speed
    }



    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2); 

        RaycastHit hit = new RaycastHit();                                                       //Aplica um raycast
        Vector3 direction = myManager.transform.position - transform.position;         
        


        if(!b.Contains(transform.position))                                                      //Retira a colisão entre os peixes
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }


        else if(Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            turning = false;



        if(turning)       
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }

        else
        {
            if (Random.Range(0, 100) < 10)                                                        //Aplica uma speed para o peixe rotacionar e voltar para o resto 
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if(Random.Range(0, 100)< 20) 
                ApplyRules();
        }
       


        transform.Translate(0, 0, Time.deltaTime * speed);                                        
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;                                                                    //Puxa as informaçoes contidas no allfish

        Vector3 vcentro = Vector3.zero;                                                             //Detecta o ponto central
        Vector3 vavoid = Vector3.zero;                                                              //Retira a colisão 
        float gSpeed = 0.01f;                                                                       //Movimenta
        float nDistance;                                                                            //Faz um checkagem da distancia
        int groupSize = 0;                                                                          //Caso estiver com um distanciamento vai criar outro grupo
   
        foreach(GameObject go in gos)      
        {
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentro += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }
        if(groupSize>0)                                                                             //Verifica se o grupo é maior que 0 e coloca rotaçoes
        {
            vcentro = vcentro / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentro + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
            }


        }
    
    }
}

