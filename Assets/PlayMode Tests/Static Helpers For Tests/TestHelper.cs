using Mathc3Project.Classes.Commands;
using Mathc3Project.Interfaces.Cells;
using Mathc3Project.Interfaces.Command;
using Tests.Enum;

namespace Tests.Static
{
    public static class TestHelper
    {
        public static ICommand GetSwapCommand(SwapTypes swapType, ICell cell)
        {
            switch (swapType)
            {
                case SwapTypes.Up:
                    return new SwipeUpCommand(cell);

                case SwapTypes.Down:
                    return new SwipeDownCommand(cell);

                case SwapTypes.Left:
                    return new SwipeLeftCommand(cell);

                case SwapTypes.Right:
                    return new SwipeRightCommand(cell);

                default:
                    return null;

            }
        }

    }
}