using System;

namespace DoodlePromptGenerator
{
    public sealed record FormattedPrompt(string Display, string Text);

    public static class PromptFormatter
    {
        public static FormattedPrompt BuildPromptWithAscii(PromptBuilder promptBuilder, int width, int height)
        {
            width = Math.Clamp(width, 36, 140);
            height = Math.Clamp(height, 18, 60);

            string prompt = promptBuilder.GeneratePrompt();
            string ascii = AsciiPatternGenerator.GenerateRandom(lines: height, width: width);
            return new FormattedPrompt(CenterPromptInAscii(ascii, prompt), prompt);
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
            if (startCol < 0 || startCol >= line.Length)
                return line;

            int availableWidth = line.Length - startCol;
            string fittedText = FitToDisplayWidth(text, availableWidth);
            int replacedWidth = GetDisplayWidth(fittedText);

            return string.Concat(
                line.AsSpan(0, startCol),
                fittedText,
                line.AsSpan(startCol + replacedWidth));
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
            int textWidth = Math.Max(12, canvasWidth - 12);

            promptLines = promptLines
                .SelectMany((line, index) => index == 0
                    ? new[] { line }
                    : WrapLine(line, textWidth))
                .ToArray();

            // Longest text line determines box width
            int longestPromptLine = promptLines.Max(GetDisplayWidth);

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
            BorderStyle border = BorderStyles[Random.Shared.Next(BorderStyles.Length)];

            // 1. Clear the full box region
            for (int row = boxStartRow; row < boxEndRow; row++)
            {
                if (row >= 0 && row < asciiLines.Length)
                {
                    asciiLines[row] = ClearRegion(asciiLines[row], boxStartCol, boxEndCol);
                }
            }

            DrawBorder(asciiLines, boxStartRow, boxEndRow, boxStartCol, boxEndCol, border);

            // Write each prompt line inside the box.
            for (int i = 0; i < promptLines.Length; i++)
            {
                int row = boxStartRow + 1 + i;

                if (row < 0 || row >= asciiLines.Length)
                    continue;

                string promptLine = promptLines[i];

                // Center each prompt line inside the box
                int textStartCol = boxStartCol + (boxWidth - GetDisplayWidth(promptLine)) / 2;

                asciiLines[row] = WriteText(asciiLines[row], promptLine, textStartCol);
            }

            return string.Join(Environment.NewLine, asciiLines);
        }

        static void DrawBorder(
            string[] lines,
            int startRow,
            int endRow,
            int startCol,
            int endCol,
            BorderStyle border)
        {
            if (border.IsEmpty || endRow - startRow < 2 || endCol - startCol < 2)
                return;

            int lastRow = endRow - 1;
            int lastCol = endCol - 1;

            lines[startRow] = WriteText(lines[startRow], BuildBorderLine(border.TopLeft, border.Horizontal, border.TopRight, endCol - startCol), startCol);
            lines[lastRow] = WriteText(lines[lastRow], BuildBorderLine(border.BottomLeft, border.Horizontal, border.BottomRight, endCol - startCol), startCol);

            for (int row = startRow + 1; row < lastRow; row++)
            {
                lines[row] = WriteText(lines[row], border.Vertical.ToString(), startCol);
                lines[row] = WriteText(lines[row], border.Vertical.ToString(), lastCol);
            }
        }

        static string BuildBorderLine(char left, string horizontal, char right, int width)
        {
            int fillWidth = Math.Max(0, width - 2);
            return $"{left}{RepeatToLength(horizontal, fillWidth)}{right}";
        }

        static string RepeatToLength(string pattern, int length)
        {
            if (length <= 0)
                return "";

            return string.Concat(Enumerable.Range(0, length).Select(index => pattern[index % pattern.Length]));
        }

        static int GetDisplayWidth(string text)
        {
            int width = 0;
            var elements = System.Globalization.StringInfo.GetTextElementEnumerator(text);

            while (elements.MoveNext())
                width++;

            return width;
        }

        static string FitToDisplayWidth(string text, int maxWidth)
        {
            var result = new System.Text.StringBuilder();
            int width = 0;
            var elements = System.Globalization.StringInfo.GetTextElementEnumerator(text);

            while (elements.MoveNext())
            {
                string element = elements.GetTextElement();
                const int elementWidth = 1;

                if (width + elementWidth > maxWidth)
                    break;

                result.Append(element);
                width += elementWidth;
            }

            return result.ToString();
        }
        static IEnumerable<string> WrapLine(string line, int maxWidth)
        {
            if (line.Length <= maxWidth)
            {
                yield return line;
                yield break;
            }

            var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var currentLine = "";

            foreach (string word in words)
            {
                if (word.Length > maxWidth)
                {
                    if (currentLine.Length > 0)
                    {
                        yield return currentLine;
                        currentLine = "";
                    }

                    for (int start = 0; start < word.Length; start += maxWidth)
                        yield return word.Substring(start, Math.Min(maxWidth, word.Length - start));

                    continue;
                }

                string candidate = currentLine.Length == 0 ? word : $"{currentLine} {word}";

                if (candidate.Length <= maxWidth)
                {
                    currentLine = candidate;
                }
                else
                {
                    yield return currentLine;
                    currentLine = word;
                }
            }

            if (currentLine.Length > 0)
                yield return currentLine;
        }

        private sealed record BorderStyle(
            char TopLeft,
            string Horizontal,
            char TopRight,
            char Vertical,
            char BottomLeft,
            char BottomRight,
            bool IsEmpty = false);

        private static readonly BorderStyle[] BorderStyles =
        {
            new(' ', " ", ' ', ' ', ' ', ' ', true),
            new('╔', "═", '╗', '║', '╚', '╝'),
            new('┌', "─", '┐', '│', '└', '┘'),
            new('╭', "─", '╮', '│', '╰', '╯'),
            new('┏', "━", '┓', '┃', '┗', '┛'),
            new('[', "=", ']', '|', '[', ']'),
            new('~', "^~", '~', '|', '~', '~'),
            new('+', "-", '+', '|', '+', '+'),
            new('*', "-=", '*', '|', '*', '*'),
            new('#', "=", '#', '#', '#', '#'),
            new('/', "\\/", '\\', '|', '\\', '/'),
            new('<', "->", '>', ':', '<', '>')
        };
    }
}
