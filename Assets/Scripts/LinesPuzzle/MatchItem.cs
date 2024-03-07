using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchItem hoverItem;
    public GameObject linePrefab;
    public int id;
    public GameObject line;

    public bool matched = false;

    public Canvas canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Instantiate the line inside of the canvas
        if (!this.matched)
        {
            line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
            UpdateLine(eventData.position);
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!this.matched)
        {
            UpdateLine(eventData.position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!this.matched){
            if (!this.Equals(hoverItem) && id == hoverItem.id)
            {
                UpdateLine(hoverItem.transform.position);
                LinesPuzzleController.instance.CorrectMatch();

                this.matched = true;
                hoverItem.matched = true;
            }
            else {
                Destroy(line);
            }
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverItem = this;
    }

    // Update is called once per frame
    void UpdateLine(Vector3 position)
    {
        // Update direction
        Vector3 direction = position - transform.position;
        line.transform.right = direction;

        // Update scale
        line.transform.localScale = new Vector3(direction.magnitude / canvas.scaleFactor, 1, 1);
        
    }
}
