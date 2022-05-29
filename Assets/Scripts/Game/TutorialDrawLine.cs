using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDrawLine : MonoBehaviour
{
    public GameObject startObject;
    public GameObject endObject;    

    // Start is called before the first frame update
    void Start()
    {
        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawLine()
    {
        this.gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        this.gameObject.transform.position = new Vector2((endObject.transform.position.x + startObject.transform.position.x) / 2, (endObject.transform.position.y + startObject.transform.position.y) / 2);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), this.gameObject.GetComponent<RectTransform>().sizeDelta.y);
        //line.GetComponent<RectTransform>().Rotate(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        this.gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan((endObject.transform.position.y - startObject.transform.position.y) / (endObject.transform.position.x - startObject.transform.position.x)) * 180f / Mathf.PI);
        //line.GetComponent<MeshCollider>().size = new Vector2(Vector2.Distance(startObject.transform.position, endObject.transform.position), line.GetComponent<RectTransform>().sizeDelta.y);
    }
}
