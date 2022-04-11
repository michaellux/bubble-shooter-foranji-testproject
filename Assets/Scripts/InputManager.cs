using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class InputManager : MonoBehaviour
{
    private GraphicRaycaster rc;
    private PointerEventData pt;
    private EventSystem eventSystem;
    void Start()
    {
        rc = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        pt = new PointerEventData(eventSystem);
        pt.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        rc.Raycast(pt, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log(result.gameObject.name);
        }
    }

    public void BackToMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
