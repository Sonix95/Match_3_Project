using Mathc3Project.Classes.StaticClasses;
using Mathc3Project.Interfaces;

namespace Mathc3Project.Classes
{
    public class LevelsManager : ILevelsManager
    {
        private ILevel[] _levels;

        public LevelsManager()
        {
            _levels = new ILevel[]
            {
                new Level(1, 6, 6,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_REDCIRCLE, 2)
                    }),

                new Level(2, 6, 6,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_YELLOWUPTRIANGLE, 6),
                    }),

                new Level(3, 7, 5,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_GREENDOWNTIRANGLE, 7),
                        new LevelTask(Strings.TAG_REDCIRCLE, 5)

                    }),

                new Level(4, 8, 8,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_ORANGEBOX, 12),
                        new LevelTask(Strings.TAG_BLUEMULTIANGLE, 8)
                    }),

                new Level(5, 7, 9,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_ORANGEBOX, 15),
                        new LevelTask(Strings.TAG_REDCIRCLE, 15)
                    }),

                new Level(6, 9, 10,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_REDCIRCLE, 5),
                        new LevelTask(Strings.TAG_GREENDOWNTIRANGLE, 8),
                        new LevelTask(Strings.TAG_BLUEMULTIANGLE, 12)
                    }),

                new Level(7, 10, 11,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_REDCIRCLE, 5),
                        new LevelTask(Strings.TAG_BLUEMULTIANGLE, 12)
                    }),
                new Level(8, 12, 12,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_GREENDOWNTIRANGLE, 25),
                        new LevelTask(Strings.TAG_BLUEMULTIANGLE, 25),
                        new LevelTask(Strings.TAG_REDCIRCLE, 13)

                    }),
                new Level(9, 10, 9,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.TAG_BLUEMULTIANGLE, 12),
                        new LevelTask(Strings.TAG_ORANGEBOX, 24),
                        new LevelTask(Strings.TAG_GREENDOWNTIRANGLE, 10)
                    }),
            };
        }

        public ILevel[] Levels
        {
            get { return _levels; }
            set { _levels = value; }
        }
        
    }
}