using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform selfTransform; //Stocker la référence du transform pour éviter que le moteur le recalcule à chaque fois
    [SerializeField] private Transform spawnerTransform;
    
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float movementSpeed=1f;
    [SerializeField] private float cameraSensibility=1f;

    [SerializeField] private Transform spheresHolderTransform;

    [SerializeField] private Rigidbody playerRigidbody;
    
    private float yawn = 0f;

    private float pitch = 0f;
    
    private GameObject target;
    private float posx_target;
    private float posy_target;
    private float posz_target;
    private int numberTargets = 0;
    
    private const float lifeSpan = 3f; //temps en secondes au bout de laquelle une nouvelle cible va appraître
    private float timer = 0f; //temps qui va être comparé au lifeSpan, dès qu'il va l'atteindre : instanciation d'une nouvelle cible
    
    [SerializeField] private Transform targetTransform;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Permet de cacher le curseur quand on est dans le jeu
    }
    
    private void Update()
    {
        movePlayer();
        
        RotateCamera();

        Shoot();
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            posx_target = Random.Range(-10f, 10f);
            posy_target = Random.Range(10f, 15f);
            posz_target = Random.Range(-10f, 10f);
            numberTargets++;
            Instantiate(Resources.Load("Target"),
                new Vector3(posx_target,posy_target,posz_target),
                Quaternion.identity,
                targetTransform).name="Target "+numberTargets;
            timer = 0;
        }
    }

   

    public void movePlayer()
    {
        //Cache calculs des vecteurs Avant et Droit
        Vector3 cameraRight = cameraTransform.right;
        Vector3 cameraForward = cameraTransform.forward;
        
        
        //Calcul du déplacement du joueur
        Vector3 deltaPosition =
            new Vector3(cameraRight.x,0f,cameraRight.z) * Input.GetAxis("Horizontal") 
            + new Vector3(cameraForward.x,0f,cameraForward.z) * Input.GetAxis("Vertical");
        //Somme des 2 vecteurs pour droite/avant pour que si on appuie à la fois sur haut et droite, la caméra bouge
        //"en diagonale" (en avant vers la droite)
        //deltaPosition = nouvelle position du joueur (déplacement), pour le déplacement, on additionne donc les 2 vecteurs
        // de caméra (droite et gauche) en multipliant par les axes pour que ça varie selon les mouvements au clavier
        
        
        //Déplacement du joueur
        playerRigidbody.MovePosition(playerRigidbody.position + deltaPosition* movementSpeed);
    }

    public void RotateCamera()
    {
        //Rotation de la caméra
        //Clamp permet de définir les valeurs max et min

        float pitch = -Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch,-90f,90f); //min = -90°, max = 90°
        
        
        cameraTransform.localEulerAngles += new Vector3(
            pitch, //Pareil ici, si on met yawn en X et pitch en Y, va être inversé
            Input.GetAxis("Mouse X"), 
            0f)*cameraSensibility;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject sphere= (GameObject) Instantiate( //On instancie des sphères à chaque fois pour simuler le tir
                Resources.Load("Sphere"),
                spawnerTransform.position, //Point d'apparition = au niveau du spawnerTransform
                Quaternion.identity, //Pour les angles de rotation
                spheresHolderTransform); //Pour mieux organiser la hiérarchie : on fait apparaître les entités en enfant du
                                        //GameObject correspondant au spheresHolderTransform
            
            sphere.GetComponent<Rigidbody>().AddForce(cameraTransform.forward*5000f); //On ajoute une force à la sphère que l'on tire
        }
        
        
    }
    

    
}
