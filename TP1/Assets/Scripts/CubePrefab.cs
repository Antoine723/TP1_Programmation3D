using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

public class CubePrefab : MonoBehaviour
{
    [SerializeField] private int clonesChosen = 0; //variable qui contient le nombre de clones que l'utilisateur souhaite instancier
    private float posx=0;
    private float posy = 0;
    private float posz=0;
    
    [SerializeField] private float rayonChosen = 5f; //pareil que pour clonesChosen mais avec le rayon
    private float rayon = 5f;
    private float angle = 0;
    private int clonesCreated = 0; //nombre de clones une fois qu'ils ont été instanciés
    private ArrayList cubesList=new ArrayList(); //liste qui contiendra l'ensemble des cubes
    private GameObject cube;
    private GameObject lastCube; //dernier cube de la liste
    private GameObject centerCube;
    private float posxCenter; //position en x du cube du centre
    private float posyCenter;
    private float poszCenter; //position en z du cube du centre
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        cubesList.Add(Resources.Load("Cube")); //On ajoute notre cube du centre dans la liste
        centerCube = GameObject.Find("Cube");
        posxCenter = centerCube.transform.position.x; //on stocke ses positions en x en y et en z dans les variables correspondantes
        posyCenter = centerCube.transform.position.y;
        poszCenter = centerCube.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if ( (clonesCreated != clonesChosen) || (rayon!=rayonChosen) || (centerCube.transform.position.x != posxCenter) || (centerCube.transform.position.y != posyCenter) || (centerCube.transform.position.z != poszCenter))
            //Si l'utilisateur modifie le nombre de clones, le rayon ou la position en x en y ou en z du centre, on détruit tous les cubes
            //en ré-assignant le nouveau centre du cercle
        {
            posxCenter = centerCube.transform.position.x;
            posyCenter = centerCube.transform.position.y;
            poszCenter = centerCube.transform.position.z;
            for (int i = cubesList.Count-1; i > 0; i--)
            {
                Destroy((Object) cubesList[i]);
                cubesList.RemoveAt(i);
            }

            clonesCreated = 0;
            rayon = rayonChosen;
        }
        while (clonesCreated < clonesChosen) //Ici on recrée tous les cubes en mettant à jour le bon nombre de cubes et le bon centre
        {
            lastCube = (GameObject)cubesList[cubesList.Count-1]; //on récupère le dernier cube de la liste à chaque fois pour pouvoir adapter
                                                                 //la position du nouveau cube en fonction de ce dernier
            posx = (float) (rayon * Math.Cos(angle)) + posxCenter;
            posy = posyCenter;
            posz = (float) (rayon * Math.Sin(angle)) +poszCenter;
            angle += (float) (2 * Math.PI / clonesChosen); //On a un angle différent selon le cube (on incrémente donc "angle")
            cube = (GameObject) Instantiate(Resources.Load("Cube"), //on instancie notre préfab
                new Vector3(posx, posy, posz),
                Quaternion.identity);
            cubesList.Add(cube);
            clonesCreated += 1;
            cube.name ="Cube " + clonesCreated;
            
        }
    }
    
}
