using System;
using System.Collections;
using UnityEngine;
using Mathc3Project.Interfaces;

namespace Mathc3Project
{
    public class GameElement : MonoBehaviour, IGameElement
    {
        #region Fields

        private float _size;
        private Color _color;
        private string _name;
        private bool _isUpdate;
        private Vector3 _currentPosition;

        public float Size { get { return _size; } set { _size = value; } }
        public Color Color { get { return _color; } set { _color = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool IsUpdate { get { return _isUpdate; } set { _isUpdate = value; } }
        public Vector3 CurrentPosition { get { return _currentPosition; } set { _currentPosition = value; } }

        #endregion

        #region Methods

        private void Start()
        {
            transform.localScale *= _size;
            GetComponent<Renderer>().material.color = _color;
            name = _name;
            transform.position = _currentPosition;
        }

        public void SetUpdate(bool isUpdate)
        {
            _isUpdate = isUpdate;
        }
        

        public void SetPosition()
        {
            transform.position = CurrentPosition;
        }

        #endregion
    }
}
