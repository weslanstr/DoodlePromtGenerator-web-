using System;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Generates procedural ASCII patterns in the console.
///
/// Entry points:
///   AsciiPatternGenerator.GenerateRandom()              – 50 % chance: defined pattern, 50 % chance: procedural automaton
///   AsciiPatternGenerator.GenerateRandom("wave")        – force a specific named pattern
///   AsciiPatternGenerator.GenerateRandom(seed: 42)      – reproducible result
///   AsciiPatternGenerator.AvailablePatterns()           – list of valid names
/// </summary>
public static class AsciiPatternGenerator
{
    // ── Public API ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Main entry point. Everything goes through here.
    ///
    /// • If <paramref name="patternName"/> is supplied and recognised, that
    ///   defined pattern is rendered directly.
    /// • Otherwise a coin is flipped:
    ///     Heads (50 %) → one of the 7 defined patterns, each with equal probability (1/7)
    ///     Tails (50 %) → a fully procedural cellular-automaton pattern
    /// </summary>
    /// <param name="patternName">
    ///   Optional. One of: "wave", "diamond", "spiral", "maze",
    ///   "checkerboard", "zigzag", "ripple". Pass null / omit to let the
    ///   method decide randomly.
    /// </param>
    /// <param name="lines">Height of the output in console rows (default 50).</param>
    /// <param name="width">Width of the output in characters (default 80).</param>
    /// <param name="seed">
    ///   Optional RNG seed. Same seed always produces the same output.
    /// </param>
    public static string GenerateRandom(
        string? patternName = null,
        int lines = 50,
        int width = 120,
        int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random();

        // ── Routing ─────────────────────────────────────────────────────────────

        // If the caller explicitly named a pattern, honour it without a coin flip.
        if (patternName is not null && IsKnownPattern(patternName))
            return Render(Resolve(patternName), lines, width);

        // 50 / 50 coin flip.
        if (rng.Next(2) == 0)
        {
            // ── Defined-pattern branch (50 % of all calls) ───────────────────
            // Each of the 7 named patterns has equal probability (1/7) within
            // this branch, so each is 1/14 of all calls overall.
            string[] names = AvailablePatterns();
            string chosen = names[rng.Next(names.Length)];
            return Render(Resolve(chosen), lines, width);
        }
        else
        {
            // ── Procedural cellular-automaton branch (50 % of all calls) ────
            return RenderProcedural(rng, lines, width);
        }
    }

    /// <summary>Returns the names of all built-in defined patterns.</summary>
    public static string[] AvailablePatterns() =>
        new[] { "wave", "diamond", "spiral", "maze", "checkerboard", "zigzag", "ripple",
                "crosshatch", "pulse", "weave", "sierpinski", "dune", "helix", "static", "vortex" };

    // ── Routing helpers ────────────────────────────────────────────────────────

    private static bool IsKnownPattern(string name)
    {
        foreach (var n in AvailablePatterns())
            if (string.Equals(n, name, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    private static PatternDef Resolve(string name) => name.ToLower() switch
    {
        "wave" => WavePattern(),
        "diamond" => DiamondPattern(),
        "spiral" => SpiralPattern(),
        "maze" => MazePattern(),
        "checkerboard" => CheckerboardPattern(),
        "zigzag" => ZigzagPattern(),
        "ripple" => RipplePattern(),
        "crosshatch" => CrosshatchPattern(),
        "pulse" => PulsePattern(),
        "weave" => WeavePattern(),
        "sierpinski" => SierpinskiPattern(),
        "dune" => DunePattern(),
        "helix" => HelixPattern(),
        "static" => StaticPattern(),
        "vortex" => VortexPattern(),
        _ => WavePattern()
    };

    // ── Rendering ──────────────────────────────────────────────────────────────

    /// Builds a defined pattern into a string, one row per line.
    private static string Render(PatternDef pattern, int lines, int width)
    {
        var sb = new StringBuilder((width + Environment.NewLine.Length) * lines);
        var row_sb = new StringBuilder(width);
        for (int row = 0; row < lines; row++)
        {
            row_sb.Clear();
            for (int col = 0; col < width; col++)
                row_sb.Append(pattern.CharAt(col, row, width, lines));
            sb.AppendLine(row_sb.ToString());
        }
        return sb.ToString();
    }

    /// Builds a procedural cellular-automaton pattern into a string.
    private static string RenderProcedural(Random rng, int lines, int width)
    {
        // ── 1. Build palette (3–10 chars, no repeats) ───────────────────────────
        char[] master = { ' ', '.', ':', '-', '~', '=', '+', 'o', '*', 'x', '#', '%', '@', '|', '/', '\\' };
        int paletteSize = rng.Next(3, 11);
        var palette = new char[paletteSize];
        var pool = new List<char>(master);
        for (int i = 0; i < paletteSize; i++)
        {
            int pick = rng.Next(pool.Count);
            palette[i] = pool[pick];
            pool.RemoveAt(pick);
        }

        // ── 2. Pick an evolution style ──────────────────────────────────────────
        // 0 = weighted-neighbourhood   smooth flowing bands
        // 1 = XOR-shift                diagonal stripes / plaid
        // 2 = wave-drift               irrational superposition, never repeats
        // 3 = majority-vote            soft blobs
        int style = rng.Next(4);

        // ── 3. Randomised parameters (fixed once per call) ──────────────────────
        double wL = rng.NextDouble() * 2 - 0.5;
        double wS = rng.NextDouble() * 2 + 0.5;   // biased: self matters most
        double wR = rng.NextDouble() * 2 - 0.5;
        double globalPhase = rng.NextDouble() * Math.PI * 2;
        double driftSpeed = (rng.NextDouble() * 0.4 + 0.05) * (rng.Next(2) == 0 ? 1 : -1);
        double waveFreq = rng.NextDouble() * 0.3 + 0.05;
        int xorShift = rng.Next(1, 5);
        int xorMask = rng.Next(1, paletteSize);

        // ── 4. Seed row 0 with a structured wave ────────────────────────────────
        // Pure noise would look chaotic and evolve badly.
        // A wave gives the automaton interesting genetic material to work from.
        int[] current = new int[width];
        for (int col = 0; col < width; col++)
        {
            double t = col * waveFreq + globalPhase;
            double v = (Math.Sin(t) + Math.Sin(t * 2.3 + 1.1) * 0.5 + 1.5) / 3.0;   // 0–1
            current[col] = (int)(v * (paletteSize - 1));
        }

        // ── 5. Row-by-row evolution loop ────────────────────────────────────────
        var output = new StringBuilder((width + Environment.NewLine.Length) * lines);
        var row_sb = new StringBuilder(width);
        int[] next = new int[width];

        for (int row = 0; row < lines; row++)
        {
            // Append current row to output
            row_sb.Clear();
            for (int col = 0; col < width; col++)
                row_sb.Append(palette[current[col]]);
            output.AppendLine(row_sb.ToString());

            // Compute the next row from the current one
            double rowPhase = row * driftSpeed;   // slow drift prevents locked repeats

            for (int col = 0; col < width; col++)
            {
                int L = current[(col - 1 + width) % width];   // left  neighbour (wraps)
                int C = current[col];                          // self
                int R = current[(col + 1) % width];   // right neighbour (wraps)
                int P = paletteSize;

                int newIdx = style switch
                {
                    // Weighted sum of neighbourhood + sine modulation
                    0 => (int)Math.Abs(
                             (wL * L + wS * C + wR * R
                              + Math.Sin(col * waveFreq + rowPhase) * (P * 0.25)
                             ) % P),

                    // XOR-shift: bitwise mixing → diagonal / plaid structures
                    1 => ((C ^ (L + xorShift)) + (R ^ xorMask) + (int)(rowPhase * 0.5)) % P,

                    // Two irrational-frequency waves superimposed → never locks into repeat
                    2 => (int)Math.Abs(
                             (Math.Sin(col * waveFreq + rowPhase)
                              + Math.Sin(col * waveFreq * 1.618 - rowPhase * 0.7) * 0.5
                              + 1.5) / 3.0 * (P - 1)),

                    // Majority vote: neighbourhood average + wave nudge for tiebreak
                    _ => (int)Math.Round(
                             (L + C + R) / 3.0
                             + Math.Sin(col * 0.2 + rowPhase) * 0.8) % P,
                };

                next[col] = Math.Abs(newIdx) % P;
            }

            (current, next) = (next, current);   // swap buffers, no allocation
        }

        return output.ToString();
    }

    // ── Defined Pattern Definitions ────────────────────────────────────────────

    /// Sine-based travelling wave with crest/trough density shading.
    private static PatternDef WavePattern()
    {
        const int period = 16;
        char[] shade = { ' ', '.', ':', '-', '=', '+', '*', '#', '@' };
        return new PatternDef("wave", (col, row, w, h) =>
        {
            double phase = (col * Math.PI * 2.0) / period;
            double wave1 = Math.Sin(phase + row * 0.3);
            double wave2 = Math.Sin(phase * 0.5 - row * 0.2) * 0.5;
            double value = (wave1 + wave2 + 1.5) / 3.0;
            int idx = (int)(value * (shade.Length - 1));
            return shade[Math.Clamp(idx, 0, shade.Length - 1)];
        });
    }

    /// Repeating diamond / rhombus grid.
    private static PatternDef DiamondPattern()
    {
        const int size = 10;
        return new PatternDef("diamond", (col, row, w, h) =>
        {
            int cx = col % size, cy = row % (size / 2);
            int dx = Math.Abs(cx - size / 2), dy = Math.Abs(cy - size / 4);
            int dist = dx + dy;
            return dist switch { 0 => '@', 1 => '#', 2 => '*', 3 => '+', 4 => '-', 5 => '.', _ => ' ' };
        });
    }

    /// Concentric square rings that tile across the canvas.
    private static PatternDef SpiralPattern()
    {
        const int cell = 12;
        char[] chars = { '#', '+', 'o', '.', ' ', '.', 'o', '+' };
        return new PatternDef("spiral", (col, row, w, h) =>
        {
            int lx = col % cell, ly = row % cell;
            int ring = Math.Min(Math.Min(lx, cell - 1 - lx), Math.Min(ly, cell - 1 - ly));
            bool horizontal = lx <= ly && lx <= cell - 1 - ly;
            int idx = (ring * 2 + (horizontal ? 0 : 1)) % chars.Length;
            return chars[idx];
        });
    }

    /// Pseudo-random maze walls via a coordinate hash — deterministic, no RNG.
    private static PatternDef MazePattern()
    {
        const int cell = 6;
        return new PatternDef("maze", (col, row, w, h) =>
        {
            int gx = col / cell, gy = row / cell;
            int lx = col % cell, ly = row % cell;
            bool wallH = Hash(gx, gy) % 3 == 0;
            bool wallV = Hash(gx + 7, gy) % 3 == 0;
            if (lx == 0 || ly == 0) return '#';
            if (wallH && ly == cell / 2) return '#';
            if (wallV && lx == cell / 2) return '#';
            return ' ';
        });
    }

    /// Checkerboard whose symbol pairs cycle through four tiers.
    private static PatternDef CheckerboardPattern()
    {
        const int sq = 4;
        char[,] symbols = { { '#', '.' }, { '+', ' ' }, { 'o', '-' }, { '*', '~' } };
        return new PatternDef("checkerboard", (col, row, w, h) =>
        {
            int tier = ((col / sq) + (row / sq)) / 4 % 4;
            int parity = ((col / sq) + (row / sq)) % 2;
            return symbols[tier, parity];
        });
    }

    /// Sine-shifted vertical bands producing a zigzag flow.
    private static PatternDef ZigzagPattern()
    {
        const int amplitude = 8, period = 20, bands = 5;
        char[] bandChars = { '|', '/', '-', '\\', '+' };
        return new PatternDef("zigzag", (col, row, w, h) =>
        {
            int wave = (int)(amplitude * Math.Sin((row * Math.PI * 2.0) / period));
            int band = ((col + wave + w) / (w / bands)) % bands;
            bool onEdge = Math.Abs((col + wave + w) % (w / bands) - (w / bands) / 2) > (w / bands) / 2 - 2;
            return onEdge ? bandChars[band] : ' ';
        });
    }

    /// Circular ripple rings tiling the canvas.
    private static PatternDef RipplePattern()
    {
        const int spacing = 4, tileW = 24, tileH = 14;
        char[] ring = { '@', '#', '*', '+', 'o', '.', ' ', '.', 'o', '+', '*', '#' };
        return new PatternDef("ripple", (col, row, w, h) =>
        {
            int lx = col % tileW - tileW / 2;
            int ly = row % tileH - tileH / 2;
            double dist = Math.Sqrt(lx * lx * 0.5 + ly * ly);   // scaled to keep rings round
            int idx = (int)(dist / spacing) % ring.Length;
            return ring[idx];
        });
    }

    // ── New Pattern Definitions ────────────────────────────────────────────────

    /// Overlapping diagonal lines at 45° forming an X-grid crosshatch.
    private static PatternDef CrosshatchPattern()
    {
        char[] shade = { ' ', '.', '+', '#', '@' };
        return new PatternDef("crosshatch", (col, row, w, h) =>
        {
            const int spacing = 6;
            int diag1 = (col + row) % spacing;   // top-left → bottom-right
            int diag2 = (col - row + w * 2) % spacing;   // top-right → bottom-left
            int density = 0;
            if (diag1 == 0) density += 2;
            if (diag2 == 0) density += 2;
            if (diag1 == 1 || diag2 == 1) density += 1;
            return shade[Math.Min(density, shade.Length - 1)];
        });
    }

    /// Concentric rectangles expanding from the canvas centre — radar / pulse effect.
    private static PatternDef PulsePattern()
    {
        char[] ring = { '@', '#', '=', '-', '.', ' ', '.', '-', '=', '#' };
        return new PatternDef("pulse", (col, row, w, h) =>
        {
            // Chebyshev distance from canvas centre (makes rectangular rings)
            int dx = Math.Abs(col - w / 2);
            int dy = Math.Abs(row - h / 2) * 2;   // ×2 compensates for monospace aspect ratio
            int dist = Math.Max(dx, dy);
            return ring[dist % ring.Length];
        });
    }

    /// Basket-weave: interlocking horizontal and vertical strands.
    private static PatternDef WeavePattern()
    {
        return new PatternDef("weave", (col, row, w, h) =>
        {
            const int strand = 3;   // thickness of each strand
            int block = strand * 2;
            int phase = (col / block + row / block) % 2;   // alternates which strand is "over"
            int lc = col % block;
            int lr = row % block;

            if (phase == 0)
            {
                // Horizontal strand on top in even blocks
                if (lr < strand) return lr == 0 || lr == strand - 1 ? '|' : '-';
                if (lc < strand) return lc == 0 || lc == strand - 1 ? '-' : '|';
                return '+';
            }
            else
            {
                // Vertical strand on top in odd blocks
                if (lc < strand) return lc == 0 || lc == strand - 1 ? '-' : '|';
                if (lr < strand) return lr == 0 || lr == strand - 1 ? '|' : '-';
                return '+';
            }
        });
    }

    /// Sierpiński triangle fractal — uses the bitwise AND trick: (col & row) != 0.
    private static PatternDef SierpinskiPattern()
    {
        char[] on = { '#', '@', '*' };
        char[] off = { ' ', '.', ':' };
        return new PatternDef("sierpinski", (col, row, w, h) =>
        {
            // Tile the fractal by wrapping coordinates into a power-of-two cell
            const int cell = 32;
            int tc = col % cell;
            int tr = row % (cell / 2);
            // Classic Sierpiński condition
            bool filled = (tc & tr) == 0;
            int tier = ((col / cell) + (row / (cell / 2))) % on.Length;
            return filled ? on[tier] : off[tier];
        });
    }

    /// Diagonal wood-grain / sand-dune bands that drift slowly across the canvas.
    private static PatternDef DunePattern()
    {
        char[] shade = { ' ', '`', '.', '-', '~', '=', '#' };
        return new PatternDef("dune", (col, row, w, h) =>
        {
            // Two diagonal waves at slightly different angles, superimposed
            double v1 = Math.Sin((col * 0.08) + (row * 0.18));
            double v2 = Math.Sin((col * 0.13) - (row * 0.07) + 1.5) * 0.5;
            double v = (v1 + v2 + 1.5) / 3.0;   // normalise 0-1
            int idx = (int)(v * (shade.Length - 1));
            return shade[Math.Clamp(idx, 0, shade.Length - 1)];
        });
    }

    /// Double-helix: two sine ribbons offset by π, flowing down the canvas.
    private static PatternDef HelixPattern()
    {
        return new PatternDef("helix", (col, row, w, h) =>
        {
            const double freq = 0.25;   // vertical frequency of the helix turn
            double angle = row * freq * Math.PI;
            // The two strands sit at ±amplitude, projected onto the column axis
            double amp = w * 0.35;
            double cx = w / 2.0;
            double strand1 = cx + amp * Math.Sin(angle);
            double strand2 = cx + amp * Math.Sin(angle + Math.PI);   // opposite phase

            double d1 = Math.Abs(col - strand1);
            double d2 = Math.Abs(col - strand2);
            double closest = Math.Min(d1, d2);

            if (closest < 1.0) return '@';
            else if (closest < 2.0) return '#';
            else if (closest < 3.5) return '*';
            else if (closest < 5.0) return '.';
            else return ' ';
        });
    }

    /// Structured static: hash-driven noise with visible grain texture.
    private static PatternDef StaticPattern()
    {
        char[] chars = { ' ', ' ', ' ', '.', '.', ':', '+', '*', '#', '@' };
        return new PatternDef("static", (col, row, w, h) =>
        {
            // Mix multiple hash frequencies to create layered grain
            int h1 = Hash(col, row) % chars.Length;
            int h2 = Hash(col / 2, row / 2) % chars.Length;
            int h3 = Hash(col / 4, row / 4) % chars.Length;
            int idx = (h1 * 3 + h2 * 2 + h3) / 6;   // weighted blend
            return chars[Math.Clamp(idx, 0, chars.Length - 1)];
        });
    }

    /// Vortex: polar swirl calculated from angle and distance to canvas centre.
    private static PatternDef VortexPattern()
    {
        char[] ring = { ' ', '.', ':', '-', '+', '*', '#', '@' };
        return new PatternDef("vortex", (col, row, w, h) =>
        {
            double cx = w / 2.0;
            double cy = h / 2.0;
            double dx = (col - cx) / (w * 0.5);   // normalised -1..1
            double dy = (row - cy) / (h * 0.5) * 2.5;   // aspect-ratio corrected
            double dist = Math.Sqrt(dx * dx + dy * dy);
            double angle = Math.Atan2(dy, dx);        // -π..π
            // Swirl: add a rotation proportional to distance from centre
            double swirl = angle + dist * 3.5;
            // Map swirl + distance to a palette index
            double v = (Math.Sin(swirl * 2.0) * 0.5 + dist * 0.5);
            int idx = (int)(Math.Abs(v) * (ring.Length - 1)) % ring.Length;
            return ring[idx];
        });
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    /// Deterministic integer hash of two coordinates (used by MazePattern).
    private static int Hash(int x, int y)
    {
        unchecked
        {
            int h = x * 374761393 + y * 668265263;
            h = (h ^ (h >> 13)) * 1274126177;
            return Math.Abs(h ^ (h >> 16));
        }
    }

    // ── Inner type ─────────────────────────────────────────────────────────────

    private sealed class PatternDef
    {
        public string Name { get; }
        private readonly Func<int, int, int, int, char> _charAt;

        public PatternDef(string name, Func<int, int, int, int, char> charAt)
        {
            Name = name;
            _charAt = charAt;
        }

        public char CharAt(int col, int row, int totalWidth, int totalLines) =>
            _charAt(col, row, totalWidth, totalLines);
    }
}