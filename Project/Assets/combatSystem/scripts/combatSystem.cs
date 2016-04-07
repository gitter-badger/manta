using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class combatSystem : MonoBehaviour {
    public Button attbtn;
    private GameObject player;
    public GameObject enemy;
    private RaycastHit hit;

	void Start () {
        
	}

    void Update() {
        //if (enemy != null) {
        //    attbtn.interactable = true;
        //} else {
        //    attbtn.interactable = false;
        //}
        selectEnemy();
    }

    void inflictDamage() {
        
    }

    void normAttack() {
        
    }

    void selectEnemy() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("click");
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            Debug.Log("mouse down and ray cast " + hit.collider.gameObject.name);
            Collider _col = hit.collider;
            if (_col.gameObject.tag == "Player" &&
                _col.gameObject.layer == LayerMask.NameToLayer("player") &&
                _col.gameObject.layer != LayerMask.NameToLayer("localPlayer")) {
                enemy = _col.gameObject;
                Debug.Log("enemy selected " + _col.gameObject.name);
            } else if (_col.gameObject.tag == "Player" &&
                _col.gameObject.layer != LayerMask.NameToLayer("player") &&
                _col.gameObject.layer == LayerMask.NameToLayer("localPlayer")) {
                enemy = null;
            }
        }
    }
}
