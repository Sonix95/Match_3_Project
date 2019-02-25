using Mathc3Project.Interfaces;
using UnityEngine;

namespace Mathc3Project
{
    public class CameraManager : ICameraManager
    {
        private Camera _mainCamera;
        private float _cameraZOffset;
        private float _aspectRatio;
        private float _padding;

        public CameraManager(int boardWidth, int boardHeight)
        {
            _mainCamera = Camera.main;
            _cameraZOffset = -10f;
            _aspectRatio = 0.675f;
            _padding = 2f;
            SetCamera(boardWidth, boardHeight);
        }

        private void SetCamera(int boardWidth, int boardHeight)
        {
            _mainCamera.transform.position = new Vector3(boardWidth / 2, boardHeight / 2, _cameraZOffset);

            if (boardWidth > boardHeight)
                _mainCamera.orthographicSize = (boardWidth / 2 + _padding) / _aspectRatio;
            else
                _mainCamera.orthographicSize = boardHeight / 2 + _padding;
        }

    }
}
