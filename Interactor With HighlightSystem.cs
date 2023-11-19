using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorWithHighlight : MonoBehaviour
{
    public LayerMask InteractableLayermask;
    public Image interactImage;
    public Sprite defualtIcon;
    public Vector2 defualtIconSize;
    public Sprite defualtInteractIcon;
    public Vector2 defualtInteractIconSize;
    public float maxHighlightDistance = 2f;
    private Interactable currentInteractable;
    private Transform highlightedObject;

    void Update()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxHighlightDistance, InteractableLayermask);

        // Handle Interactable Objects
        if (hitSomething)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                HandleInteractable(interactable);
            }
        }
        else
        {
            ResetInteractImageToDefault();
        }

        // Handle Highlighting
        if (highlightedObject != null)
        {
            var outline = highlightedObject.GetComponent<Outline>();
            if (outline != null) outline.enabled = false;
        }

        if (hitSomething && hit.collider.CompareTag("Selectable"))
        {
            highlightedObject = hit.transform;
            var outline = highlightedObject.GetComponent<Outline>();
            if (outline == null)
            {
                outline = highlightedObject.gameObject.AddComponent<Outline>();
                outline.OutlineColor = Color.magenta;
                outline.OutlineWidth = 7.0f;
            }
            outline.enabled = true;
        }
        else
        {
            highlightedObject = null;
        }
    }

    void HandleInteractable(Interactable interactable)
    {
        if (currentInteractable == null || currentInteractable.ID != interactable.ID)
        {
            currentInteractable = interactable;
        }

        if (interactable.interactIcon != null)
        {
            interactImage.sprite = interactable.interactIcon;
            interactImage.rectTransform.sizeDelta = interactable.iconSize != Vector2.zero ? interactable.iconSize : defualtInteractIconSize;
        }
        else
        {
            interactImage.sprite = defualtInteractIcon;
            interactImage.rectTransform.sizeDelta = defualtInteractIconSize;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.onInteract.Invoke();
        }
    }

    void ResetInteractImageToDefault()
    {
        if (interactImage.sprite != defualtIcon)
        {
            interactImage.sprite = defualtIcon;
            interactImage.rectTransform.sizeDelta = defualtIconSize;
        }
        currentInteractable = null;
    }
}