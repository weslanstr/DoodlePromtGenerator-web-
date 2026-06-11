using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DoodlePromptGenerator
{
    public class PromptBuilder
    {
        private readonly Random random;

        private readonly Dictionary<string, string> objectNouns;
        private readonly Dictionary<string, string> foodNouns;
        private readonly Dictionary<string, string> countryNames;
        private readonly Dictionary<string, string> emotionAdjectives;
        private readonly Dictionary<string, string> genderDescriptors;
        private readonly Dictionary<string, string> characterNouns;
        private readonly Dictionary<string, string> creatureNouns;
        private readonly Dictionary<string, string> atmosphereAdjectives;
        private readonly Dictionary<string, string> propNouns;
        private readonly Dictionary<string, string> actionPhrases;

        public PromptBuilder()
        {
            random = new Random();

            objectNouns = new()
            {
                { "gun", "🔫" },
                { "revolver", "🔫" },
                { "rifle", "🔫" },
                { "AK-47", "🔫" },
                { "shotgun", "🔫" },
                { "sniper rifle", "🔫" },
                { "energy sword", "⚔️" },
                { "katana", "🗡️" },
                { "longsword", "⚔️" },
                { "dagger", "🗡️" },
                { "battle axe", "🪓" },
                { "war hammer", "🔨" },
                { "lantern", "🏮" },
                { "torch", "🔥" },
                { "camera", "📷" },
                { "polaroid camera", "📷" },
                { "video camera", "📹" },
                { "film reel", "🎞️" },
                { "book", "📖" },
                { "ancient tome", "📚" },
                { "journal", "📓" },
                { "scroll", "📜" },
                { "skull", "💀" },
                { "radio", "📻" },
                { "walkie-talkie", "📻" },
                { "helmet", "⛑️" },
                { "pilot helmet", "🪖" },
                { "gas mask", "" },
                { "mask", "🎭" },
                { "backpack", "🎒" },
                { "jetpack", "" },
                { "motorcycle", "🏍️" },
                { "sports car", "🏎️" },
                { "hover bike", "" },
                { "train", "🚆" },
                { "airplane", "✈️" },
                { "spaceship", "🛸" },
                { "rocket", "🚀" },
                { "cassette player", "📼" },
                { "headphones", "🎧" },
                { "vinyl record", "💿" },
                { "microphone", "🎤" },
                { "sunglasses", "🕶️" },
                { "watch", "⌚" },
                { "pocket watch", "⏱️" },
                { "clock", "🕰️" },
                { "compass", "🧭" },
                { "map", "🗺️" },
                { "treasure chest", "🧰" },
                { "coin", "🪙" },
                { "diamond", "💎" },
                { "computer", "💻" },
                { "phone", "📱" },
                { "tablet", "📱" },
                { "TV", "📺" },
                { "drone", "" },
                { "rose", "🌹" },
                { "sunflower", "🌻" },
                { "soccer ball", "⚽" },
                { "basketball", "🏀" },
                { "football", "🏈" },
                { "guitar", "🎸" },
                { "drum set", "🥁" },
                { "piano", "🎹" },
                { "flute", "🎶" },
                { "violin", "🎻" },
                { "crown", "👑" },
                { "ring", "💍" },
                { "umbrella", "☂️" },
                { "briefcase", "💼" },
                { "test tube", "🧪" },
                { "beaker", "⚗️" },
                { "dice", "🎲" },
                { "playing cards", "🃏" },
                { "key", "🔑" },
                { "money bag", "💰" },
                { "gift box", "🎁" },
                { "trophy", "🏆" },
                { "paintbrush", "🖌️" },
                { "sealed portal", "🚪" },
                { "weather machine", "⚡" },
                { "mechanical heart", "🫀" },
                { "impossible key", "🔑" },
                { "glass moon", "🌙" },
                { "memory jar", "🫙" },
                { "miniature city", "🏙️" },
                { "singing compass", "🧭" },
                { "broken constellation", "✨" },
                { "living blueprint", "📐" },
                { "portable sunset", "🌅" }
            };

            foodNouns = new()
            {
                { "pizza", "🍕" },
                { "burger", "🍔" },
                { "ramen", "🍜" },
                { "sushi", "🍣" },
                { "taco", "🌮" },
                { "hotdog", "🌭" },
                { "apple", "🍎" },
                { "banana", "🍌" },
                { "grapes", "🍇" },
                { "strawberry", "🍓" },
                { "watermelon", "🍉" },
                { "coffee", "☕" },
                { "tea", "🍵" },
                { "cake", "🍰" },
                { "cookie", "🍪" },
                { "donut", "🍩" },
                { "ice cream", "🍨" },
                { "chocolate", "🍫" },
                { "popcorn", "🍿" },
                { "steak", "🥩" },
                { "fried chicken", "🍗" },
                { "fries", "🍟" },
                { "rice bowl", "🍚" },
                { "dumplings", "🥟" }
            };

            countryNames = new()
            {
                { "Japan", "🇯🇵" },
                { "United States", "🇺🇸" },
                { "France", "🇫🇷" },
                { "Italy", "🇮🇹" },
                { "Brazil", "🇧🇷" },
                { "Mexico", "🇲🇽" },
                { "Canada", "🇨🇦" },
                { "United Kingdom", "🇬🇧" },
                { "Germany", "🇩🇪" },
                { "Russia", "🇷🇺" },
                { "China", "🇨🇳" },
                { "South Korea", "🇰🇷" },
                { "India", "🇮🇳" },
                { "Australia", "🇦🇺" },
                { "Egypt", "🇪🇬" },
                { "Greece", "🇬🇷" },
                { "Norway", "🇳🇴" },
                { "Sweden", "🇸🇪" },
                { "Spain", "🇪🇸" },
                { "Ireland", "🇮🇪" }
            };

            emotionAdjectives = new()
            {
                { "happy", "😊" },
                { "sad", "😢" },
                { "angry", "😠" },
                { "shocked", "😱" },
                { "crying", "😭" },
                { "laughing", "😂" },
                { "smug", "😏" },
                { "determined", "😤" },
                { "sleepy", "😴" },
                { "terrified", "😨" },
                { "confused", "😕" },
                { "love-struck", "😍" },
                { "cool", "😎" },
                { "evil grin", "😈" },
                { "dead inside", "😐" },
                { "hopeful", "🥹" },
                { "nervous", "😬" },
                { "embarrassed", "😳" },
                { "excited", "🤩" },
                { "stoic", "😶" }
            };

            creatureNouns = new()
            {
                { "wolf", "🐺" },
                { "cat", "🐱" },
                { "dog", "🐶" },
                { "fox", "🦊" },
                { "bear", "🐻" },
                { "owl", "🦉" },
                { "crow", "🐦" },
                { "raven", "🐦" },
                { "snake", "🐍" },
                { "dragon", "🐉" },
                { "frog", "🐸" },
                { "rabbit", "🐰" },
                { "deer", "🦌" },
                { "tiger", "🐯" },
                { "lion", "🦁" },
                { "shark", "🦈" },
                { "octopus", "🐙" },
                { "butterfly", "🦋" },
                { "spider", "🕷️" },
                { "bat", "🦇" },
                { "moth", "🦋" },
                { "axolotl", "🦎" },
                { "jellyfish", "🪼" },
                { "snail", "🐌" },
                { "phoenix", "🔥" },
                { "griffin", "🦅" },
                { "sea serpent", "🐍" },
                { "clockwork beetle", "🪲" },
                { "cloud whale", "🐋" },
                { "moon rabbit", "🐰" }
            };

            genderDescriptors = new()
            {
                { "male", "♂️" },
                { "female", "⚧️" },
                { "androgynous", "⚧" },
                { "", "" }
            };

            characterNouns = new()
            {
                { "bigfoot", "🦶" },
                { "alien", "👽" },
                { "samurai", "🗡️" },
                { "cowboy", "🤠" },
                { "astronaut", "👨‍🚀" },
                { "ballerina", "🩰" },
                { "ghost", "👻" },
                { "robot", "🤖" },
                { "furry", "🐾" },
                { "witch", "🧙" },
                { "knight", "🛡️" },
                { "punk", "🎸" },
                { "detective", "🕵️" },
                { "mercenary", "💰" },
                { "survivor", "🩹" },
                { "pirate", "🏴‍☠️" },
                { "ninja", "🥷" },
                { "vampire", "🧛" },
                { "werewolf", "🐺" },
                { "cyborg", "🤖" },
                { "scientist", "🧪" },
                { "soldier", "🪖" },
                { "pilot", "✈️" },
                { "racer", "🏎️" },
                { "bounty hunter", "💵" },
                { "monk", "🙏" },
                { "sniper", "🎯" },
                { "mechanic", "🔧" },
                { "hacker", "💻" },
                { "gambler", "🎲" },
                { "thief", "🕶️" },
                { "spy", "🕵️" },
                { "android", "🤖" },
                { "cryptid", "👣" },
                { "demon", "😈" },
                { "angel", "😇" },
                { "monster hunter", "⚔️" },
                { "explorer", "🧭" },
                { "nomad", "🎒" },
                { "gunslinger", "🔫" },
                { "dream courier", "💌" },
                { "storm chaser", "⚡" },
                { "memory thief", "🧠" },
                { "time traveler", "⌛" },
                { "mushroom knight", "🍄" },
                { "deep-sea librarian", "📚" },
                { "moon gardener", "🌙" },
                { "ghost conductor", "🚆" },
                { "wandering oracle", "🔮" }
            };

            atmosphereAdjectives = new()
            {
                { "melancholic", "😔" },
                { "dreamlike", "💭" },
                { "heroic", "🦸" },
                { "chaotic", "🌀" },
                { "mysterious", "🌫️" },
                { "lonely", "🌙" },
                { "hopeful", "✨" },
                { "post-apocalyptic", "☢️" },
                { "cozy", "☕" },
                { "cyberpunk", "🌃" },
                { "noir", "🌑" },
                { "romantic", "❤️" },
                { "surreal", "🫧" },
                { "tense", "⚡" },
                { "ominous", "🌩️" },
                { "peaceful", "🕊️" },
                { "eldritch", "🐙" },
                { "haunted", "👻" },
                { "futuristic", "🚀" },
                { "retro", "📼" },
                { "gritty", "🪨" },
                { "whimsical", "🦋" },
                { "ethereal", "☁️" },
                { "somber", "🖤" },
                { "mythic", "🐉" },
                { "liminal", "🚪" },
                { "bioluminescent", "✨" },
                { "analog", "📼" },
                { "rain-soaked", "🌧️" },
                { "celestial", "🌌" },
                { "storybook", "📖" },
                { "uncanny", "👁️" }
            };

            propNouns = new()
            {
                { "glowing orb", "🔮" },
                { "wilted flower", "🥀" },
                { "steaming coffee mug", "☕" },
                { "futuristic rifle", "" },
                { "broken helmet", "⛑️" },
                { "map", "🗺️" },
                { "candle", "🕯️" },
                { "bloodied knife", "🗡️" },
                { "sketchbook", "📓" },
                { "tiny creature", "🐾" },
                { "radio", "📻" },
                { "photograph", "🖼️" },
                { "smoking revolver", "🔫" },
                { "cracked phone", "📱" },
                { "lantern", "🏮" },
                { "katana", "🗡️" },
                { "guitar", "🎸" },
                { "robotic skull", "💀" },
                { "hologram tablet", "📱" },
                { "mysterious cube", "🧊" },
                { "bouquet of roses", "💐" },
                { "potion bottle", "🧪" },
                { "teddy bear", "🧸" },
                { "compass", "🧭" },
                { "drone controller", "🎮" },
                { "bottle of captured thunder", "⚡" },
                { "folded paper moon", "🌙" },
                { "jar of fireflies", "✨" },
                { "pocket-sized doorway", "🚪" },
                { "clock that runs backward", "🕰️" },
                { "map of a nonexistent country", "🗺️" },
                { "cassette tape labeled tomorrow", "📼" },
                { "umbrella full of stars", "☂️" }
            };

            actionPhrases = new()
            {
                { "running", "🏃" },
                { "jumping", "🤸" },
                { "floating", "☁️" },
                { "screaming", "😱" },
                { "laughing", "😂" },
                { "crying", "😭" },
                { "looking into the distance", "👀" },
                { "kneeling", "🧎" },
                { "reloading", "🔄" },
                { "painting", "🎨" },
                { "repairing something", "🔧" },
                { "sleeping", "😴" },
                { "staring at the sky", "🌌" },
                { "posing dramatically", "🕺" },
                { "walking through the rain", "🌧️" },
                { "aiming carefully", "🎯" },
                { "drawing in a notebook", "✏️" },
                { "driving at high speed", "🏎️" },
                { "falling", "🕳️" },
                { "meditating", "🧘" },
                { "playing guitar", "🎸" },
                { "watching the sunset", "🌅" },
                { "hiding in shadows", "🌑" },
                { "reaching toward the light", "✨" },
                { "standing over a defeated enemy", "⚔️" },
                { "riding into the horizon", "🌄" }
            };
        }

        private T GetRandomItem<T>(IReadOnlyList<T> items)
        {
            return items[random.Next(items.Count)];
        }

        private (string text, string emoji) GetRandomEmojiPair(Dictionary<string, string> map)
        {
            var item = map.ElementAt(random.Next(map.Count));
            return (item.Key, item.Value);
        }

        private string BuildEmojiLine(params string[] emojis)
        {
            return string.Concat(emojis.Where(e => !string.IsNullOrWhiteSpace(e)));
        }

        private string BuildEmojiLineForSentence(string sentence)
        {
            var maps = new[]
            {
                characterNouns,
                creatureNouns,
                objectNouns,
                propNouns,
                foodNouns,
                emotionAdjectives,
                atmosphereAdjectives,
                countryNames,
                actionPhrases
            };
            var matches = maps
                .SelectMany(map => map)
                .Where(pair => !string.IsNullOrWhiteSpace(pair.Value) &&
                    sentence.Contains(pair.Key, StringComparison.OrdinalIgnoreCase))
                .Select(pair => pair.Value)
                .Distinct()
                .Take(4)
                .ToList();

            if (matches.Count == 0)
                matches.Add(GetRandomEmojiPair(characterNouns).emoji);

            return BuildEmojiLine(matches.ToArray());
        }

        public string GeneratePrompt(int challenge = 5)
        {
            challenge = Math.Clamp(challenge, 0, 10);

            if (challenge >= 4 && random.Next(100) == 0)
                return $"✦ RARE PROMPT ✦{Environment.NewLine}{GetRandomItem(RarePrompts)}";

            string sentence = NormalizeGrammar(BuildChallengePrompt(challenge));
            return $"{BuildEmojiLineForSentence(sentence)}{Environment.NewLine}{sentence}";
        }

        private string BuildChallengePrompt(int challenge)
        {
            return challenge switch
            {
                0 => BuildSimpleSubjectPrompt(false),
                1 => BuildSimpleSubjectPrompt(true),
                2 => BuildModifierPrompt(),
                3 => BuildScenePrompt(false),
                4 => BuildScenePrompt(true),
                5 => random.Next(2) == 0 ? BuildStoryPrompt() : BuildCompositionPrompt(false),
                6 => random.Next(2) == 0 ? BuildStoryPrompt() : BuildWeirdPrompt(),
                7 => GetRandomItem(new Func<string>[] { BuildWeirdPrompt, BuildQuantityPrompt, () => BuildCompositionPrompt(true) })(),
                8 => GetRandomItem(new Func<string>[] { BuildQuantityPrompt, () => BuildCompositionPrompt(true), () => BuildExtremePrompt(false) })(),
                9 => random.Next(3) == 0 ? BuildCompactChallengePrompt() : BuildExtremePrompt(false),
                10 => random.Next(3) == 0 ? BuildCompactChallengePrompt() : BuildExtremePrompt(true),
                _ => throw new InvalidOperationException($"Unsupported challenge level: {challenge}.")
            };
        }

        private string BuildSimpleSubjectPrompt(bool withModifier)
        {
            string subject = random.Next(3) switch
            {
                0 => GetRandomEmojiPair(characterNouns).text,
                1 => GetRandomEmojiPair(creatureNouns).text,
                _ => GetRandomObjectNoun()
            };

            return withModifier
                ? $"A {GetRandomItem(AppearanceAdjectives)} {subject}."
                : $"A {subject}.";
        }

        private string BuildModifierPrompt()
        {
            string subject = random.Next(2) == 0
                ? GetRandomEmojiPair(characterNouns).text
                : GetRandomEmojiPair(creatureNouns).text;

            return random.Next(4) switch
            {
                0 => $"A {subject} holding a {GetRandomObjectNoun()}.",
                1 => $"A {GetRandomItem(AppearanceAdjectives)} {subject} wearing a {GetRandomEmojiPair(propNouns).text}.",
                2 => $"A {subject} with a {GetRandomEmojiPair(propNouns).text}.",
                _ => $"A {subject} {GetRandomItem(ActivityVerbs).Ing}."
            };
        }

        private string BuildScenePrompt(bool detailed)
        {
            string character = GetRandomEmojiPair(characterNouns).text;
            string action = GetRandomItem(ActivityVerbs).Ing;
            string place = GetRandomItem(Places);

            return detailed
                ? $"A {GetRandomItem(AppearanceAdjectives)} {character} {action} at {GetRandomItem(Times)} in a {place}."
                : $"A {character} {action} in a {place}.";
        }

        private string BuildStoryPrompt()
        {
            string character = GetRandomEmojiPair(characterNouns).text;
            string noun = GetRandomItem(Nouns);

            return random.Next(4) switch
            {
                0 => $"A {character} discovers the last {noun} on Earth.",
                1 => $"A {character} finds a {GetRandomItem(AppearanceAdjectives)} {GetRandomObjectNoun()} in a {GetRandomItem(Places)}.",
                2 => $"A {character} {GetRandomItem(ActivityVerbs).Ing}, unaware that {GetRandomItem(Twists)}.",
                _ => $"The day the {noun} began to {GetRandomItem(TransformativeVerbs).Base}."
            };
        }

        private string BuildCompositionPrompt(bool advanced)
        {
            string scene = GetRandomItem(Scenes);

            return advanced
                ? $"A {scene}, {GetRandomItem(CameraAngles)}, lit by {GetRandomItem(LightSources)}, with {GetRandomItem(Compositions)}."
                : $"A {scene}, lit by {GetRandomItem(LightSources)}, in a {GetRandomItem(Seasons)} color palette.";
        }

        private string BuildWeirdPrompt()
        {
            string character = GetRandomEmojiPair(characterNouns).text;
            string creature = GetRandomEmojiPair(creatureNouns).text;

            return random.Next(4) switch
            {
                0 => $"A {GetRandomItem(AppearanceAdjectives)} {creature} that is afraid of {GetRandomItem(Nouns)}.",
                1 => $"A tiny {creature} controlling a colossal {GetRandomObjectNoun()}.",
                2 => $"A {character} made of {GetRandomItem(Materials)} trying to {GetRandomItem(SimpleActions)}.",
                _ => $"A {GetRandomItem(Scenes)} where everything is {GetRandomItem(UnexpectedConstraints)}."
            };
        }

        private string BuildQuantityPrompt()
        {
            int number = GetRandomItem(ChallengeNumbers);
            string pluralCreature = GetRandomItem(PluralCreatures);

            return random.Next(3) switch
            {
                0 => $"{number} {pluralCreature} {GetRandomItem(ActivityVerbs).Ing} around a {GetRandomItem(AppearanceAdjectives)} {GetRandomObjectNoun()}.",
                1 => $"A crowded {GetRandomItem(Places)} filled with {pluralCreature} and {GetRandomItem(PluralObjects)}.",
                _ => $"Draw {number} unique {GetRandomItem(PluralObjects)} arranged with {GetRandomItem(Compositions)}."
            };
        }

        private string BuildCompactChallengePrompt()
        {
            return random.Next(4) switch
            {
                0 => $"Draw {GetRandomItem(ChallengeNumbers)} hands holding different {GetRandomItem(PluralObjects)}.",
                1 => $"Draw a {GetRandomItem(Places)} using only {GetRandomItem(Shapes)}.",
                2 => $"Draw a {GetRandomEmojiPair(creatureNouns).text} from {GetRandomItem(CameraAngles)}.",
                _ => $"Draw one room at {GetRandomItem(ChallengeNumbers)} different times of day."
            };
        }

        private string BuildExtremePrompt(bool nightmare)
        {
            int number = nightmare ? GetRandomItem(ExtremeNumbers) : GetRandomItem(ChallengeNumbers);
            string pluralCreature = GetRandomItem(PluralCreatures);
            string action = GetRandomItem(TransitiveVerbs).Ing;
            string place = GetRandomItem(Places);
            string weather = GetRandomItem(WeatherAdjectives);
            string light = GetRandomItem(LightSources);

            string prompt = $"{number} {pluralCreature} {action} a {GetRandomObjectNoun()} in a {place} while the weather turns {weather}, lit by {light}";

            if (nightmare)
                prompt += $", {GetRandomItem(CameraAngles)}, with {GetRandomItem(Compositions)}, where everything is {GetRandomItem(UnexpectedConstraints)}";

            return $"{prompt}.";
        }

        private string GetRandomGender()
        {
            return GetRandomItem(genderDescriptors.Keys.Where(gender => !string.IsNullOrWhiteSpace(gender)).ToArray());
        }

        private string GetRandomObjectNoun()
        {
            var category = random.Next(3);

            return category switch
            {
                0 => GetRandomEmojiPair(objectNouns).text,
                1 => GetRandomEmojiPair(propNouns).text,
                _ => GetRandomEmojiPair(foodNouns).text
            };
        }

        private static string NormalizeGrammar(string sentence)
        {
            sentence = Regex.Replace(sentence, @"\s+", " ").Trim();
            sentence = Regex.Replace(sentence, @"\bA ([aeiouAEIOU])", "An $1");
            sentence = Regex.Replace(sentence, @"\ba ([aeiouAEIOU])", "an $1");
            return sentence;
        }

        private static readonly string[] AppearanceAdjectives =
        {
            "ancient", "tiny", "colossal", "forgotten", "glowing", "rusted", "elegant", "wild",
            "sleepy", "mechanical", "haunted", "cheerful", "fragile", "mysterious", "storm-worn",
            "ornate", "lonely", "cosmic", "crystalline", "patchwork", "impossible", "enchanted",
            "bioluminescent", "overgrown", "holographic", "porcelain", "ink-stained", "weathered",
            "iridescent", "moss-covered", "celestial", "stitched", "molten", "paper-thin"
        };

        private static readonly string[] WeatherAdjectives =
        {
            "electric", "violent", "unnaturally still", "purple", "full of falling stars",
            "thick with fog", "weightless", "crystalline", "radioactive", "dreamlike"
        };

        private static readonly string[] Descriptors =
        {
            "masked", "one-eyed", "clockwork", "shadowy", "golden", "scarred", "hooded",
            "miniature", "towering", "transparent", "armored", "lost", "grinning"
        };

        private static readonly string[] Nouns =
        {
            "city", "door", "machine", "memory", "moon", "tower", "forest", "ocean", "dream",
            "signal", "garden", "storm", "library", "planet", "statue", "bridge", "kingdom",
            "island", "mirror", "star", "monster", "song", "map", "crown", "clock"
        };

        private static readonly VerbForm[] TransitiveVerbs =
        {
            new("build", "building", "builds"), new("chase", "chasing", "chases"),
            new("carry", "carrying", "carries"), new("discover", "discovering", "discovers"),
            new("paint", "painting", "paints"), new("repair", "repairing", "repairs"),
            new("follow", "following", "follows"), new("summon", "summoning", "summons"),
            new("steal", "stealing", "steals"), new("protect", "protecting", "protects"),
            new("open", "opening", "opens"), new("study", "studying", "studies"),
            new("challenge", "challenging", "challenges"), new("awaken", "awakening", "awakens"),
            new("transform", "transforming", "transforms"), new("decode", "decoding", "decodes"),
            new("forge", "forging", "forges"), new("photograph", "photographing", "photographs"),
            new("reassemble", "reassembling", "reassembles"), new("trade", "trading", "trades"),
            new("balance", "balancing", "balances"), new("examine", "examining", "examines")
        };

        private static readonly VerbForm[] ActivityVerbs =
        {
            new("wander", "wandering", "wanders"), new("dance", "dancing", "dances"),
            new("meditate", "meditating", "meditates"), new("wait", "waiting", "waits"),
            new("explore", "exploring", "explores"), new("dream", "dreaming", "dreams"),
            new("stargaze", "stargazing", "stargazes"), new("celebrate", "celebrating", "celebrates"),
            new("investigate", "investigating", "investigates"), new("perform", "performing", "performs"),
            new("rest", "resting", "rests"), new("listen", "listening", "listens"),
            new("sketch", "sketching", "sketches"), new("levitate", "levitating", "levitates"),
            new("signal", "signaling", "signals"), new("daydream", "daydreaming", "daydreams")
        };

        private static readonly VerbForm[] MovementVerbs =
        {
            new("walk", "walking", "walks"), new("run", "running", "runs"),
            new("drift", "drifting", "drifts"), new("crawl", "crawling", "crawls"),
            new("march", "marching", "marches"), new("float", "floating", "floats"),
            new("sneak", "sneaking", "sneaks"), new("charge", "charging", "charges"),
            new("glide", "gliding", "glides"), new("stumble", "stumbling", "stumbles")
        };

        private static readonly VerbForm[] TransformativeVerbs =
        {
            new("wake up", "waking up", "wakes up"), new("remember", "remembering", "remembers"),
            new("sing", "singing", "sings"), new("move", "moving", "moves"),
            new("grow", "growing", "grows"), new("dream", "dreaming", "dreams"),
            new("speak", "speaking", "speaks"), new("change", "changing", "changes"),
            new("breathe", "breathing", "breathes"), new("disappear", "disappearing", "disappears")
        };

        private static readonly string[] Emotions =
        {
            "happy", "sad", "angry", "afraid", "hopeful", "lonely", "excited", "confused",
            "calm", "jealous", "determined", "homesick", "curious", "embarrassed"
        };

        private static readonly string[] OverActions =
        {
            "arguing", "competing", "negotiating", "fighting", "laughing", "scheming",
            "playing chess", "telling stories"
        };

        private static readonly string[] Places =
        {
            "abandoned arcade", "floating city", "sunken library", "desert observatory",
            "neon alley", "haunted greenhouse", "moonlit forest", "underground station",
            "clockwork kingdom", "stormy coastline", "forgotten temple", "rooftop garden",
            "alien marketplace", "quiet museum", "frozen village"
        };

        private static readonly string[] Companions =
        {
            "robot companion", "ghost guide", "tiny dragon", "talking sword", "clockwork bird",
            "mysterious stranger", "loyal wolf", "floating lantern"
        };

        private static readonly string[] Professions =
        {
            "architect", "chef", "librarian", "cartographer", "blacksmith", "gardener",
            "astronomer", "mail carrier", "street musician", "train conductor", "archaeologist"
        };

        private static readonly string[] PluralCreatures =
        {
            "wolves", "cats", "dragons", "frogs", "rabbits", "ravens", "ghosts", "robots",
            "butterflies", "sharks", "spiders", "aliens"
        };

        private static readonly int[] Numbers = { 3, 4, 5, 7, 8, 10, 12, 20, 50, 100 };
        private static readonly int[] ChallengeNumbers = { 12, 20, 30, 40, 50, 75, 100 };
        private static readonly int[] ExtremeNumbers = { 50, 75, 100, 200, 300 };

        private static readonly string[] Times =
        {
            "sunrise", "midnight", "the end of time", "golden hour", "dusk", "3:17 AM",
            "the first morning of winter", "the final sunset"
        };

        private static readonly string[] Actions =
        {
            "battle", "escape", "transformation", "celebration", "discovery", "argument",
            "rescue", "ritual", "chase", "dance"
        };

        private static readonly string[] Materials =
        {
            "glass", "paper", "clouds", "rusted metal", "flowers", "starlight", "bones",
            "wood", "ice", "smoke", "clockwork parts", "living vines"
        };

        private static readonly string[] LightSources =
        {
            "a single candle", "neon signs", "moonlight", "a dying star", "fireflies",
            "a cracked television", "glowing mushrooms", "an open doorway"
        };

        private static readonly string[] Tools =
        {
            "paintbrush", "wrench", "spoon", "broken sword", "camera", "pencil",
            "sewing needle", "magnifying glass", "musical instrument"
        };

        private static readonly string[] Quirks =
        {
            "a fear of circles", "a terrible sense of direction", "an invisible shadow",
            "a habit of collecting buttons", "a voice that summons rain", "no reflection",
            "a backpack full of doors", "a tiny thundercloud overhead"
        };

        private static readonly string[] Scenes =
        {
            "city awakening after centuries", "picnic at the edge of space",
            "marketplace for impossible objects", "train crossing an endless ocean",
            "festival inside a giant machine", "quiet room after a storm",
            "garden growing on an abandoned spaceship", "duel beneath two moons",
            "village carried on the back of a giant", "last diner open at the end of the world"
        };

        private static readonly string[] ArtStyles =
        {
            "woodblock print", "retro science-fiction poster", "ink wash painting",
            "stained glass", "children's storybook", "noir comic", "pixel art",
            "surrealist collage", "technical blueprint", "ancient mural"
        };

        private static readonly string[] CameraAngles =
        {
            "a dramatic low angle", "a bird's-eye view", "an extreme close-up",
            "a wide cinematic shot", "an over-the-shoulder view", "an isometric view"
        };

        private static readonly string[] Compositions =
        {
            "strong symmetry", "heavy negative space", "a centered silhouette",
            "a sweeping diagonal composition", "layered foreground shapes", "a tiny distant subject"
        };

        private static readonly string[] Seasons = { "spring", "summer", "autumn", "winter", "monsoon", "festival" };

        private static readonly string[] UnexpectedConstraints =
        {
            "upside down", "made of fabric", "slightly transparent", "growing backward",
            "connected by red string", "floating one inch above the ground", "drawn as shadows",
            "built from musical instruments"
        };

        private static readonly string[] Twists =
        {
            "the map is watching them", "the city is moving", "they are the villain",
            "the treasure is already gone", "the moon is an eye", "time stopped yesterday",
            "their companion is imaginary", "the storm is following only them"
        };

        private static readonly string[] PowerfulCharacters =
        {
            "giant", "wizard", "dragon queen", "storm god", "superhero", "ancient machine",
            "immortal knight", "cosmic emperor"
        };

        private static readonly string[] SimpleActions =
        {
            "open a door", "tie their shoes", "tell a lie", "cross a bridge", "remember a name",
            "pick a flower", "sleep", "look behind them"
        };

        private static readonly string[] Purposes =
        {
            "protecting cities", "finding lost things", "creating music", "stopping time",
            "delivering messages", "making people happy", "exploring space", "ending wars"
        };

        private static readonly string[] Shapes =
        {
            "circles", "triangles", "squares", "spirals", "straight lines", "zigzags",
            "rectangles", "crescent shapes"
        };

        private static readonly string[] PluralObjects =
        {
            "keys", "masks", "robots", "spaceships", "swords", "houses", "chairs",
            "lanterns", "teacups", "monsters"
        };

        private static readonly string[] Concepts =
        {
            "hope", "silence", "gravity", "nostalgia", "time", "luck", "fear", "freedom",
            "curiosity", "loneliness", "music", "patience"
        };

        private static readonly string[] ImpossibleTasks =
        {
            "fold the ocean", "collect every shadow", "repair a broken sunset",
            "translate dreams", "measure infinity", "teach a mountain to dance",
            "deliver a letter to the past", "keep the moon awake"
        };

        private static readonly string[] NaturalLaws =
        {
            "gravity", "time", "shadows", "distance", "weather", "sleep", "reflections", "sound"
        };

        private static readonly string[] UnusualResources =
        {
            "bottled dreams", "giant mushrooms", "forgotten songs", "moonlight",
            "mechanical insects", "living clouds", "memories", "shadows"
        };

        private static readonly string[] RarePrompts =
        {
            "Draw the memory of a city.",
            "Draw your first dream.",
            "Draw a place that only exists when nobody is looking.",
            "Draw the last light left in the universe.",
            "Draw a machine that misses its creator.",
            "Draw the sound of a forgotten name.",
            "Draw tomorrow as remembered by someone from the past.",
            "Draw a doorway that refuses to open.",
            "Draw the safest place in a dangerous world.",
            "Draw something beautiful that should not exist."
        };

        private sealed record VerbForm(string Base, string Ing, string Third);
    }
}
