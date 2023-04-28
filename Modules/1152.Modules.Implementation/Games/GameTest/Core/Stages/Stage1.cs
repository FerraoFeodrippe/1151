using _1152.Modules.Application.Games.GameTest.Art;

namespace _1152.Modules.Application.Games.GameTest.Core.Stages
{
    public static class Stage1
    {
        public static void Load(char[][] field, int maxW, int maxH)
        {
            field[0][0] = ElementsBase.UlCornerPipe;
            field[maxW - 1][ 0] = ElementsBase.UrCornerPipe;
            field[0][ maxH - 1] = ElementsBase.LlCornerPipe;
            field[maxW - 1][maxH - 1] = ElementsBase.LrCornerPipe;

            for (int i = 1; i < maxW - 1; i++)
            {
                field[i][0] = ElementsBase.HorizontalPipe;
            }

            for (int i = 1; i < maxW - 1; i++)
            {
                field[i][maxH - 1] = ElementsBase.HorizontalPipe;
            }

            for (int i = 1; i < maxH - 1; i++)
            {
                field[0][i] = ElementsBase.VerticalPipe;
            }

            for (int i = 1; i < maxH - 1; i++)
            {
                field[maxW - 1][i] = ElementsBase.VerticalPipe;
            }
        }
    }
}
