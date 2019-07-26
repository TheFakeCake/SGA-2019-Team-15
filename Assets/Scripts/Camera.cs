using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float limiteGauche = 0;
    public float limiteDroite = 100;

    private GameObject subMarine;

    // Start is called before the first frame update
    void Start()
    {
        subMarine = GameObject.Find("Sous-marin joueur");
    }

    // Update is called once per frame
    void Update()
    {
        // Modifie la position de la camera pour suivre le sous-marin s'il s'est trop éloigné
        Vector3 newPosition = transform.position;

        newPosition.x = Mathf.Lerp(transform.position.x, subMarine.transform.position.x, 0.5f);

        if (newPosition.x < limiteGauche) {
            newPosition.x = limiteGauche;
        }
        else if (newPosition.x > limiteDroite) {
            newPosition.x = limiteDroite;
        }

        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        // Dessine des indicateurs pour les limites de la camera
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(new Vector3(limiteGauche, transform.position.y, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(limiteDroite, transform.position.y, 0), 0.1f);
    }
}
