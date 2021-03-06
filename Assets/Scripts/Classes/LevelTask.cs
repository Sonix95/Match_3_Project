using System;
using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Mathc3Project.Classes
{
    public class LevelTask : ILevelTask
    {
        private readonly string _elementName;
        private int _count;
        private readonly Sprite _spriteElement; //TODO ADD IN SPRITES_TASKS_MANAGER
        private bool _completed;

        public LevelTask(string elementName, int count)
        {
            _elementName = elementName;
            _count = count;
            _spriteElement = Resources.Load<Sprite>(Strings.SPRITE_ELEMENTS + elementName);
            _completed = false;
        }

        public override string ToString()
        {
            return _elementName + " | " + _count;
        }

        public string ElementName
        {
            get { return _elementName; }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                if (_count <= 0)
                {
                    _count = 0;
                    _completed = true;
                }
            }
        }

        public Sprite SpriteElement
        {
            get { return _spriteElement; }
        }

        public bool Completed
        {
            get { return _completed; }
        }
    }
}
