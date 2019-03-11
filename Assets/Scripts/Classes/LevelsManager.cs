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
                new Level(1, 3, 4,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_RedCircle, 5)
                    }),

                new Level(2, 4, 5,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_YellowUpTriangle, 7),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 5)
                    }),

                new Level(3, 5, 6,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_RedCircle, 5),
                        new LevelTask(Strings.Tag_GreenDownTriangle, 8),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 12)
                    }),

                new Level(4, 6, 6,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_OrangeBox, 12),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 3)
                    }),

                new Level(5, 7, 8,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_OrangeBox, 15),
                        new LevelTask(Strings.Tag_RedCircle, 15)
                    }),

                new Level(6, 12, 5,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_GreenDownTriangle, 20),
                        new LevelTask(Strings.Tag_RedCircle, 17)
                    }),

                new Level(7, 9, 12,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_GreenDownTriangle, 25),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 25),
                        new LevelTask(Strings.Tag_RedCircle, 13)
                    }), 
                new Level(8, 9, 10,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_YellowUpTriangle, 2),
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