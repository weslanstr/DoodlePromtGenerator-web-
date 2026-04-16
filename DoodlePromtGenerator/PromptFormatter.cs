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

        static string OverwriteCenteredLine(string asciiLine, string text, int startCol)
        {
            char[] chars = asciiLine.ToCharArray();

            for (int i = 0; i < text.Length && startCol + i < chars.Length; i++)
            {
                if (startCol + i >= 0)
                {
                    chars[startCol + i] = text[i];
                }
            }

            return new string(chars);
        }

        static string ClearRegion(string line, int startCol, int endCol)
        {
            char[] chars = line.ToCharArray();

            for (int i = startCol; i < endCol && i < chars.Length; i++)
            {
                if (i >= 0)
                    chars[i] = ' ';
            }

            return new string(chars);
        }

        static string WriteText(string line, string text, int startCol)
        {
            char[] chars = line.ToCharArray();

            for (int i = 0; i < text.Length && startCol + i < chars.Length; i++)
            {
                if (startCol + i >= 0)
                    chars[startCol + i] = text[i];
            }

            return new string(chars);
        }

        static string CenterPromptInAscii(string ascii, string prompt)
        {
            const int TAB_WIDTH = 4;

            string[] asciiLines = ascii.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            if (asciiLines.Length == 0)
                return ascii;

            string[] promptLines = prompt.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.None);

            int canvasWidth = asciiLines[0].Length;
            int canvasHeight = asciiLines.Length;

            // Longest text line determines box width
            int longestPromptLine = promptLines.Max(line => line.Length);

            // Box width = left padding + text + right padding
            int boxWidth = TAB_WIDTH + longestPromptLine + TAB_WIDTH;

            if (boxWidth > canvasWidth)
                boxWidth = canvasWidth;

            // Box height = blank line above + prompt lines + blank line below
            int boxHeight = 1 + promptLines.Length + 1;

            if (boxHeight > canvasHeight)
                boxHeight = canvasHeight;

            int boxStartRow = (canvasHeight - boxHeight) / 2;
            int boxEndRow = boxStartRow + boxHeight;

            int boxStartCol = (canvasWidth - boxWidth) / 2;
            int boxEndCol = boxStartCol + boxWidth;

            // 1. Clear the full box region
            for (int row = boxStartRow; row < boxEndRow; row++)
            {
                if (row >= 0 && row < asciiLines.Length)
                {
                    asciiLines[row] = ClearRegion(asciiLines[row], boxStartCol, boxEndCol);
                }
            }

            // 2. Write each prompt line inside the box
            // row 0 inside box is blank padding
            // row 1 starts first text line
            for (int i = 0; i < promptLines.Length; i++)
            {
                int row = boxStartRow + 1 + i;

                if (row < 0 || row >= asciiLines.Length)
                    continue;

                string promptLine = promptLines[i];

                // Center each prompt line inside the box
                int textStartCol = boxStartCol + (boxWidth - promptLine.Length) / 2;

                asciiLines[row] = WriteText(asciiLines[row], promptLine, textStartCol);
            }

            return string.Join(Environment.NewLine, asciiLines);
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