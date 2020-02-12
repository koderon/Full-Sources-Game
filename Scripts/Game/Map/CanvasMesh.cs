using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
public class CanvasMesh : MonoBehaviour
{
    private CanvasRenderer _canvasRenderer;

    [SerializeField]
    private Material _material;

    private Mesh _mesh;

    /*
    [SerializeField]
    private Vector3[] _newVertices;

    [SerializeField]
    private Vector2[] _newUV;

    [SerializeField]
    private int[] _newTriangles;
    /**/
    

    [SerializeField]
    private Vector2[] points;

    /*
    [SerializeField]
    private bool _updateNew;
    [SerializeField]
    private bool _updateOld;
    /**/

    // Use this for initialization
    void Start()
    {
        Init();

        //_updateNew = true;
    }

    private void Init()
    {
        if (_canvasRenderer != null)
            return;


        _canvasRenderer = GetComponent<CanvasRenderer>();
        _canvasRenderer.SetMaterial(_material, null);
    }

    public void SetMesh(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, Color color)
    {
        Init();

        points = new Vector2[]
        {
            point1, point2, point3, point4
        };

        var vectors = new Vector3[]
        {
            point1,
            point2,
            point3,
            point4
        };

        var uv = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(1,1), 
        };

        var triangles = new int[]
        {
            0,1,3,3,2,1
        };

        if(_mesh == null)
            _mesh = new Mesh();

        _mesh.Clear();
        _mesh.vertices = vectors;
        _mesh.uv = uv;
        _mesh.triangles = triangles;

        _canvasRenderer.SetMesh(_mesh);

        _material.color = color;
    }

    public void SetColor(Color color)
    {
        if(_material != null)
            _material.color = color;
    }
    /*
    public void OldMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _newVertices;
        _mesh.uv = _newUV;
        _mesh.triangles = _newTriangles;
        _canvasRenderer.SetMesh(_mesh);
    }
    /*
    
    public void Update()
    {
        if (_updateNew)
        {
            _updateNew = false;
            if(points != null)
                SetMesh(points[0], points[1], points[2], points[3], Color.red);
        }

        if (_updateOld)
        {
            _updateOld = false;
            OldMesh();
        }
    }
    /**/
}