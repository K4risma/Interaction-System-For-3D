using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interactor : MonoBehaviour
{

    public LayerMask InteractableLayermask;
    Interactable Interactable;
    public Image interactImage;
    public Sprite defualtIcon;
    public Vector2 defualtIconSize;
    public Sprite defualtInteractIcon;
    public Vector2 defualtInteractIconSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, 2, InteractableLayermask)) 
        {
            if(hit.collider.GetComponent<Interactable>() != false) 
            {
                 if(Interactable == null || Interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                 {
                    Interactable = hit.collider.GetComponent<Interactable>();
                    
                 }
                 if(Interactable.interactIcon != null)
                 {
                     interactImage.sprite = Interactable.interactIcon;
                     if(Interactable.iconSize == Vector2.zero)
                     {
                        interactImage.rectTransform.sizeDelta = defualtInteractIconSize;
                     }
                     else
                     {
                        interactImage.rectTransform.sizeDelta = Interactable.iconSize;
                     }
                 }
                 else 
                 {
                    interactImage.sprite = defualtInteractIcon;
                    interactImage.rectTransform.sizeDelta = defualtInteractIconSize;
                 }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Interactable.onInteract.Invoke();
                }
            }
        }
        else
        {
            if(interactImage.sprite != defualtIcon)
            {
                interactImage.sprite = defualtIcon;
                interactImage.rectTransform.sizeDelta = defualtIconSize;
            }
        }
    }
}
