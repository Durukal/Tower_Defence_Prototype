using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretShopManager : MonoBehaviour {
    [SerializeField] private GameObject turretCardPrefab;
    [SerializeField] private Transform turretPanelContainer;

    [Header("Turret Settings")]
    [SerializeField] private TurretSettings[] turrets;
    [SerializeField] List<GameObject> nodes;

    private Node _currentNodeSelected;
    
    private void Start() {
        for (int index = 0; index < turrets.Length; index++) {
            CreateTurretCard(turrets[index]);
        }

        nodes = GameObject.FindGameObjectsWithTag("Node").ToList();
    }

    private void Update() {
        nodes = GameObject.FindGameObjectsWithTag("Node").ToList();
    }

    private void CreateTurretCard(TurretSettings turretSettings) {
        GameObject newInstance = Instantiate(turretCardPrefab, turretPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardButton = newInstance.GetComponent<TurretCard>();
        cardButton.SetupTurretButton(turretSettings);
    }

    private void PlaceTurret(TurretSettings turretLoaded) {
        if (_currentNodeSelected != null) {
            GameObject turretInstance = Instantiate(turretLoaded.TurretPrefab);
            turretInstance.transform.localPosition = _currentNodeSelected.transform.position;
            turretInstance.transform.parent = _currentNodeSelected.transform;
            
            Turret turretPlaced = turretInstance.GetComponent<Turret>();
            _currentNodeSelected.SetTurret(turretPlaced);
        }
    }

    public void SelectNode() {
        if (nodes.Count != 0) {
            int index = Random.Range(0, nodes.Count);
            Node selectedNode = nodes[index].gameObject.GetComponent<Node>();
            selectedNode.SelectNode();
            selectedNode.gameObject.tag = "Filled Node";
            nodes.Remove(selectedNode.gameObject);
        }
    }

    private void NodeSelected(Node nodeSelected) {
        _currentNodeSelected = nodeSelected;
    }
    
    private void TurretSold() {
        _currentNodeSelected.gameObject.tag = "Node";
        _currentNodeSelected = null;
    }
    
    private void OnEnable() {
        Node.OnNodeSelected += NodeSelected;
        Node.OnTurretSold += TurretSold;
        TurretCard.OnPlaceTurret += PlaceTurret;
    }

    private void OnDisable() {
        Node.OnNodeSelected -= NodeSelected;
        Node.OnTurretSold -= TurretSold;
        TurretCard.OnPlaceTurret -= PlaceTurret;
    }
}
