using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DoodlePromptGenerator
{
    public class PromptBuilder
    {
        private Random random;

        private Dictionary<string, string> things;
        private Dictionary<string, string> foods;
        private Dictionary<string, string> countries;
        private Dictionary<string, string> expressions;
        private Dictionary<string, string> foodsEmojiMap;
        private Dictionary<string, string> genders;
        private Dictionary<string, string> characters;
        private Dictionary<string, string> animals;
        private Dictionary<string, string> vibes;
        private Dictionary<string, string> holdingItems;
        private Dictionary<string, string> actions;

        public PromptBuilder()
        {
            random = new Random();

            things = new()
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
                { "paintbrush", "🖌️" }
            };

            foods = new()
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

            countries = new()
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

            expressions = new()
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

            animals = new()
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
                { "bat", "🦇" }
            };

            genders = new()
            {
                { "male", "♂️" },
                { "female", "⚧️" },
                { "androgynous", "⚧" },
                { "", "" }
            };

            characters = new()
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
                { "gunslinger", "🔫" }
            };

            vibes = new()
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
                { "mythic", "🐉" }
            };

            holdingItems = new()
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
                { "drone controller", "🎮" }
            };

            actions = new()
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

        private string GetRandomItem(string[] array)
        {
            int index = random.Next(array.Length);
            return array[index];
        }

        private (string text, string emoji) GetRandomEmojiPair(Dictionary<string, string> map)
        {
            var item = map.ElementAt(random.Next(map.Count));
            return (item.Key, item.Value);
        }

        private string BuildSubject(string gender, string character)
        {
            return $"{gender} {character}".Trim();
        }

        private string BuildEmojiLine(params string[] emojis)
        {
            return string.Concat(emojis.Where(e => !string.IsNullOrWhiteSpace(e)));
        }

        public string GeneratePrompt()
        {
            int sentenceType = random.Next(6);

            switch (sentenceType)
            {
                case 0:
                    {
                        var genderz = GetRandomEmojiPair(genders);
                        var character = GetRandomEmojiPair(characters);
                        var action = GetRandomEmojiPair(actions);
                        var heldItem = GetRandomEmojiPair(holdingItems);

                        string subject = BuildSubject(genderz.text, character.text);
                        string sentence = $"A {subject} {action.text} while holding a {heldItem.text}.";
                        string emojis = BuildEmojiLine(character.emoji, action.emoji, heldItem.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 1:
                    {
                        var vibe = GetRandomEmojiPair(vibes);
                        var character = GetRandomEmojiPair(characters);
                        var thing = GetRandomEmojiPair(things);

                        string sentence = $"A {vibe.text} {character.text} with a {thing.text}.";
                        string emojis = BuildEmojiLine(vibe.emoji, character.emoji, thing.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 2:
                    {
                        var character1 = GetRandomEmojiPair(characters);
                        var character2 = GetRandomEmojiPair(characters);
                        var action = GetRandomEmojiPair(actions);

                        string sentence = $"A {character1.text} and a {character2.text} {action.text} together.";
                        string emojis = BuildEmojiLine(character1.emoji, character2.emoji, action.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 3:
                    {
                        var genderz = GetRandomEmojiPair(genders);
                        var character = GetRandomEmojiPair(characters);
                        var action = GetRandomEmojiPair(actions);
                        var thing = GetRandomEmojiPair(things);

                        string subject = BuildSubject(genderz.text, character.text);
                        string sentence = $"A {subject} {action.text} near a {thing.text}.";
                        string emojis = BuildEmojiLine(character.emoji, action.emoji, thing.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 4:
                    {
                        var vibe = GetRandomEmojiPair(vibes);
                        var character = GetRandomEmojiPair(characters);
                        var heldItem = GetRandomEmojiPair(holdingItems);
                        var thing = GetRandomEmojiPair(things);

                        string sentence = $"A {vibe.text} scene of a {character.text} holding a {heldItem.text} beside a {thing.text}.";
                        string emojis = BuildEmojiLine(vibe.emoji, character.emoji, heldItem.emoji, thing.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 5:
                    {
                        var character1 = GetRandomEmojiPair(characters);
                        var character2 = GetRandomEmojiPair(characters);
                        var action = GetRandomEmojiPair(actions);
                        var heldItem = GetRandomEmojiPair(holdingItems);

                        string sentence = $"A {character1.text} and a {character2.text}, one {action.text} and one holding a {heldItem.text}.";
                        string emojis = BuildEmojiLine(character1.emoji, character2.emoji, action.emoji, heldItem.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 6:
                    {
                        var character1 = GetRandomEmojiPair(characters);
                        var character2 = GetRandomEmojiPair(characters);

                        string sentence = $"A {character1.text}, but with a baby {character2.text}.";
                        string emojis = BuildEmojiLine(character1.emoji, "👶", character2.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                case 7:
                    {
                        var character1 = GetRandomEmojiPair(characters);
                        var action1 = GetRandomEmojiPair(actions);
                        var character2 = GetRandomEmojiPair(characters);
                        var action2 = GetRandomEmojiPair(actions);

                        string sentence = $"A {character1.text} attempting to {action1.text} while a {character2.text} is {action2.text}.";
                        string emojis = BuildEmojiLine(character1.emoji, action1.emoji, character2.emoji, action2.emoji);

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }
                case 8:
                    {
                        var character1 = GetRandomEmojiPair(characters);
                        var action = GetRandomEmojiPair(actions);
                        var character2 = GetRandomEmojiPair(characters);

                        string sentence = $"A {character1.text} {action.text} for a {character2.text}.";
                        string emojis = $"{character1.emoji}{action.emoji}{character2.emoji}";

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }
                case 9:
                    {
                        var character = GetRandomEmojiPair(characters);
                        var item = GetRandomEmojiPair(actions);
                        var vibe = GetRandomEmojiPair(characters);

                        string sentence = $"A {vibe.text} {character.text} holding a {item.text}.";
                        string emojis = $"{vibe.emoji}{character.emoji}{item.emoji}";

                        return $"{emojis}{Environment.NewLine}{sentence}";
                    }

                default:
                    return $"BROKEM!.";
            }
        }
    }
}
