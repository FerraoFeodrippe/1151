using _1152.Modules.Application.Games.GameTest.Art;

namespace _1152.Modules.Application.Games.GameTest.Core.Stages
{
    public static class Stage1
    {
        public static void Load(char[][] field, int maxW, int maxH)
        {
            field[0][0] = ElementsBase.UlCornerPipe;
            field[0][maxW - 1] = ElementsBase.UrCornerPipe;
            field[maxH - 1][0] = ElementsBase.LlCornerPipe;
            field[maxH - 1][maxW - 1] = ElementsBase.LrCornerPipe;

            for (int i = 1; i < maxW - 1; i++)
            {
                field[0][i] = ElementsBase.HorizontalPipe;
            }

            for (int i = 1; i < maxW - 1; i++)
            {
                field[maxH - 1][i] = ElementsBase.HorizontalPipe;
            }

            for (int i = 1; i < maxH - 1; i++)
            {
                field[i][0] = ElementsBase.VerticalPipe;
            }

            for (int i = 1; i < maxH - 1; i++)
            {
                field[i][maxW - 1] = ElementsBase.VerticalPipe;
            }
        }
    }
}
