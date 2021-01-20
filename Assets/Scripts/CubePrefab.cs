using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

public class CubePrefab : MonoBehaviour
{
    [SerializeField] private int clonesChosen = 0; //variable qui contient le nombre de clones que l'utilisateur souhaite instancier
    private float x=0;
    private float z=0;
    [SerializeField] private float rayonChosen = 5f; //pareil que pour clonesChosen mais avec le rayon
    private float rayon = 5f;
    private float angle = 0;
    private int clonesCreated = 0; //nombre de clones une fois qu'ils ont été instanciés
    private ArrayList cubesList=new ArrayList(); //liste qui contiendra l'ensemble des cubes
    private GameObject cube;
    private GameObject lastCube; //dernier cube de la liste
    private GameObject centerCube;
    private float xCenter; //position en x du cube du centre
    private float zCenter; //position en z du cube du centre

    // Start is called before the first frame update
    void Start()
    {
        cubesList.Add(Resources.Load("Cube")); //On ajoute notre cube du centre dans la liste
        centerCube = GameObject.Find("Cube");
        xCenter = centerCube.transform.position.x; //on stocke ses positions en x et en z dans les variables correspondantes
        zCenter = centerCube.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if ( (clonesCreated != clonesChosen) || (rayon!=rayonChosen) || (centerCube.transform.position.x != xCenter) || (centerCube.transform.position.z != zCenter))
            //Si l'utilisateur modifie le nombre de clones, le rayon ou la position en x ou en z du centre, on détruit tous les cubes
            //en ré-assignant le nouveau centre du cercle
        {
            xCenter = centerCube.transform.position.x;
            zCenter = centerCube.transform.position.z;
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
            x = (float) (rayon * Math.Cos(angle)) + xCenter;
            z = (float) (rayon * Math.Sin(angle)) +zCenter;
            angle += (float) (2 * Math.PI / clonesChosen); //On a un angle différent selon le cube (on incrémente donc "angle")
            cube = (GameObject) Instantiate(Resources.Load("Cube"), //on instancie notre préfab
                new Vector3(x, transform.position.y, z),
                Quaternion.identity);
            cubesList.Add(cube);
            clonesCreated += 1;
            cube.name ="Cube " + clonesCreated;
            
        }
    }
    
}
