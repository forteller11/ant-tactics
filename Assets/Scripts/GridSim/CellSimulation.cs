using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Utilities.Editor;

namespace Ant.Grid
{
    public class CellSimulation : MonoBehaviour
    {
        [SerializeField] [ShouldNotBeNull] ComputeShader _computerShader;
        [SerializeField] [HideAtEditTime] RenderTexture _renderTexture1;
        [SerializeField] [HideAtEditTime] RenderTexture _renderTexture2;
        [SerializeField] private Vector2Int _textureDimensions = new Vector2Int(256, 256);
        [SerializeField] private Vector3Int _threadGroups = new Vector3Int(4,4,1);
        private int kernalId = -1;
        private int Texture1Id = -1;
        private int Texture2Id = -1;
        
        // Start is called befosre the first frame update
        void Start()
        {
            _renderTexture1 = new RenderTexture(_textureDimensions.x, _textureDimensions.y, 1, RenderTextureFormat.ARGB32);
            _renderTexture2 = new RenderTexture(_textureDimensions.x, _textureDimensions.y, 1, RenderTextureFormat.ARGB32);

            _renderTexture1.enableRandomWrite = true;
            _renderTexture2.enableRandomWrite = true;
            
            kernalId = _computerShader.FindKernel("diffuse");
            Texture1Id = Shader.PropertyToID("Texture1");
            Texture2Id = Shader.PropertyToID("Texture2");
            
            _computerShader.SetTexture(kernalId, Texture1Id, _renderTexture1);
            _computerShader.SetTexture(kernalId, Texture2Id, _renderTexture2);
        }

        // Update is called once per frame
        void Update()
        {
            _computerShader.Dispatch(kernalId, _threadGroups.x, _threadGroups.y, _threadGroups.z);
            var grid = new Grid();
            var screenPos = Input.mousePosition;
            int2 mousePixelPosition = grid.GetCellIndexFromScreenPosition(new int2(Screen.width, Screen.height), screenPos);
            _computerShader.SetVector("MousePosition", new Vector4(mousePixelPosition.x, mousePixelPosition.y, 0, 0));
            Debug.Log($"Mouse Pos: {screenPos}, pixel position: {mousePixelPosition}");
        }
    }
}
