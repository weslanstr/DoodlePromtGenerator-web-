using System;
using System.Text;

namespace DoodlePromptGenerator
{
    public static class PromptFormatter
    {
        public static string BuildPromptWithAscii(PromptBuilder promptBuilder)
        {
            string prompt = promptBuilder.GeneratePrompt();
            string ascii = AsciiPatternGenerator.GenerateRandom();
            return CenterPromptInAscii(ascii, prompt);
        }

        private static string CenterPromptInAscii(string ascii, string prompt)
        {
            const int TAB_WIDTH = 4;

            string[] lines = ascii.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0) return ascii;

            int lineWidth = lines[0].Length;
            int boxInnerWidth = TAB_WIDTH + prompt.Length + TAB_WIDTH;

            if (boxInnerWidth > lineWidth)
                boxInnerWidth = lineWidth;

            int boxStart = (lineWidth - boxInnerWidth) / 2;
            int boxEnd = boxStart + boxInnerWidth;

            int midLine = lines.Length / 2;
            int blankAbove = Math.Max(0, midLine - 1);
            int textLine = midLine;
            int blankBelow = Math.Min(lines.Length - 1, midLine + 1);

            lines[blankAbove] = OverwriteRegion(lines[blankAbove], boxStart, boxEnd, "");
            lines[blankBelow] = OverwriteRegion(lines[blankBelow], boxStart, boxEnd, "");
            lines[textLine] = OverwriteRegion(lines[textLine], boxStart, boxEnd, prompt, TAB_WIDTH);

            return string.Join(Environment.NewLine, lines);
        }

        private static string OverwriteRegion(
            string line,
            int boxStart,
            int boxEnd,
            string content,
            int contentPadding = 0)
        {
            var sb = new StringBuilder(line.Length);

            for (int col = 0; col < line.Length; col++)
            {
                if (col < boxStart || col >= boxEnd)
                {
                    sb.Append(line[col]);
                }
                else
                {
                    int localCol = col - boxStart;
                    int contentStart = contentPadding;
                    int contentEnd = contentPadding + content.Length;

                    if (content.Length > 0 && localCol >= contentStart && localCol < contentEnd)
                        sb.Append(content[localCol - contentStart]);
                    else
                        sb.Append(' ');
                }
            }

            return sb.ToString();
        }
    }
}