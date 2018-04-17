using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour {

	public List<GameObject> nodes;
	public List<GameObject> mainNodes;
	public List<string> prefixes;
	public List<GameObject> edges;
	public int widthFactor;
	public GameObject nodePrefab;
	public GameObject edgePrefab;
	public List<string> hydrocarbonNames;
	public int currentChainValue;
	public Dropdown chainUIDropdown;
	public GameObject chainUI;
	public bool chainAdded = false;
	public Text name_;
	void Start () {
		chainUIDropdown = chainUI.GetComponent<Dropdown> ();
	}
	

	void Update () {

		if(Input.GetMouseButtonDown(0))
		{

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray,out hit))
			{

				if (hit.transform.gameObject.tag == "Node") 
				{
					
					GameObject g = hit.transform.gameObject;
					for (int n = 0; n < nodes.Count; n++)
					{
						
						if (mainNodes[n].GetComponent<Node>().value == g.GetComponent<Node>().value) 
						{
							
							currentChainValue = n;
							chainUI.transform.position = new Vector3 (g.transform.position.x + 1, g.transform.position.y, 0);
							
						}
					}
				}
			}

		}

		if (mainNodes.Count > 1 && chainAdded == false) 
		{
			name_.text = hydrocarbonNames [mainNodes.Count - 1] + "ane";
		}
		
	}


	public void spawnChain(int length)
	{
		addChain (length, currentChainValue);
		Debug.Log ("name changed");

	}
	public void addChain(int sizeOfChain,int chainNumber)
	{
		if(sizeOfChain == 0)
		{
			return;
		}
		Debug.Log ("size of chain " + sizeOfChain + "  " + "chain number " + chainNumber); 
		chainUI.transform.position = new Vector3 (100, 100, 100);
		chainUIDropdown.value = 0;
		chainAdded = true;
		name_.text = (chainNumber + 1) + "-" + prefixes [sizeOfChain] + " " + name_.text;

		float upOrDown;
		if (chainNumber % 2 == 0) {
			upOrDown = 2f;
		} else
		{
			upOrDown = -1f;
		}
		Vector3 upVector = new Vector3 (nodes[chainNumber].transform.position.x,nodes[chainNumber].transform.position.y + upOrDown,0 );
		GameObject nodeObject = Instantiate (nodePrefab, upVector, Quaternion.identity);
		nodes [chainNumber].GetComponent<Node> ().setNextNode (nodeObject);
		nodeObject.GetComponent<Node> ().connected = true;
		nodes.Add (nodeObject);
		JoinTwoNodes (nodes [chainNumber], nodeObject);
		SpawnNodesZigZag (sizeOfChain-1, new Vector3 (nodeObject.transform.position.x,nodeObject.transform.position.y,0));

		for (int i = nodes.Count - 1; i < nodes.Count - sizeOfChain; i--)
		{
			nodes [i].GetComponent<Node> ().connected = true;
		}
	}


	public void createChain(int chainLength)
	{
		destroyListElements (nodes);
		destroyListElements (mainNodes);
		destroyListElements (edges);
		nodes.Clear ();
		mainNodes.Clear ();
		edges.Clear ();

	
		if (chainLength > 1) {
			SpawnNodesZigZag (chainLength, new Vector3 (0, 0, 0));
			MoveCameraToCenterOfNodes ();
		}
		nodes [nodes.Count - 1].GetComponent<Node> ().connected = true;
		chainAdded = false;
	}

	void destroyListElements(List<GameObject> l)
	{
		for (int i = 0; i < l.Count; i++) {
			DestroyImmediate (l [i]);
		}
	}

	void MoveCameraToCenterOfNodes()
	{
		float midX;
		midX = (nodes [0].transform.position.x + nodes [nodes.Count - 1].transform.position.x) / 2;
		Camera.main.transform.position = new Vector3 (midX, Camera.main.transform.position.y, -10);
	}

	void JoinTwoNodes(GameObject startNode,GameObject endNode)
	{

		Debug.Log ("joining node");
		GameObject edgeObject = Instantiate (edgePrefab, Vector3.zero, Quaternion.identity);
		edges.Add (edgeObject);
		Edge edge = edgeObject.GetComponent<Edge> ();
		edge.setStartNode (startNode);
		edge.setEndNode(endNode);
		edges.Add (edgeObject);
		startNode.GetComponent<Node> ().connected = true;


	}

	void SpawnNodesZigZag(int chainLen,Vector3 startPosition)
	{	
		int x = 0;		
		for ( x = 0; x < chainLen; x++)
		{
			float y;
			if (x % 2 == 0)
			{
				y = 1f;
			} 
			else
			{
				y = -1f;
			}
			Vector3 nextPosition = Vector3.zero;
			if (startPosition != Vector3.zero) {

				if (startPosition.y > 0 && x == 0) {
					nextPosition = new Vector3 (startPosition.x +  widthFactor, startPosition.y - y, 0);
					}
				if (startPosition.y < 0 && x == 0) {
					nextPosition = new Vector3 (startPosition.x +  widthFactor, startPosition.y - y, 0);
				}
				if (x != 0) 
				{
					
					if( (x+1) % 2 == 0)
					{
						y = 0f;
					}
					else
					{
						y = -1f;
					}
					nextPosition = new Vector3 (startPosition.x + (x+1f) * widthFactor, startPosition.y + y, 0);
				}
			}
			else {
				nextPosition = new Vector3 (startPosition.x + x * widthFactor, startPosition.y + y, 0);
			}
			GameObject node = Instantiate (nodePrefab, nextPosition, Quaternion.identity);
			node.GetComponent<Node> ().value = node.GetInstanceID ();
			nodes.Add(node);
		
			if(startPosition == new Vector3(0,0,0))
			{
				mainNodes.Add (node);
			}

		}
			
			for (int z = nodes.Count - 1; z > 0; z--) 
				{
				GameObject tempNodeA = nodes [z];
				GameObject tempNodeB = nodes [z - 1];
			if (tempNodeA.GetComponent<Node> ().connected == false) {
					JoinTwoNodes (tempNodeA, tempNodeB);
				}


			}
	
	}
	public void Home()
	{
		Application.LoadLevel (0);
	}
}

