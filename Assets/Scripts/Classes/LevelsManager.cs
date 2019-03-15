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
                        new LevelTask(Strings.Tag_RedCircle, 2)
                    }),

                new Level(2, 6, 6,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_YellowUpTriangle, 6),
                    }),

                new Level(3, 7, 5,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_GreenDownTriangle, 7),
                        new LevelTask(Strings.Tag_RedCircle, 5)

                    }),

                new Level(4, 8, 8,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_OrangeBox, 12),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 8)
                    }),

                new Level(5, 7, 9,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_OrangeBox, 15),
                        new LevelTask(Strings.Tag_RedCircle, 15)
                    }),

                new Level(6, 9, 10,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_RedCircle, 5),
                        new LevelTask(Strings.Tag_GreenDownTriangle, 8),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 12)
                    }),

                new Level(7, 10, 11,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_RedCircle, 5),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 12)
                    }),
                new Level(8, 12, 12,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_GreenDownTriangle, 25),
                        new LevelTask(Strings.Tag_BlueMultiAngle, 25),
                        new LevelTask(Strings.Tag_RedCircle, 13)

                    }),
                new Level(9, 10, 9,
                    new ILevelTask[]
                    {
                        new LevelTask(Strings.Tag_BlueMultiAngle, 12),
                        new LevelTask(Strings.Tag_OrangeBox, 24),
                        new LevelTask(Strings.Tag_GreenDownTriangle, 10)
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