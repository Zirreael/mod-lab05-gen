using System;
using System.IO;

namespace generator
{
    class CharGenerator
    {
        private string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя";
        private char[] data;
        private int size;
        private Random random = new Random();
        public CharGenerator()
        {
            size = syms.Length;
            data = syms.ToCharArray();
        }
        public char getSym()
        {
            return data[random.Next(0, size)];
        }
    }

    class BigrammGen
    {
        private string syms = "абвгдежзийклмнопрстуфхцчшщьыэюя";
        private char[] data;
        private int size;
        private int[,] ver;
        private Random random = new Random();
        public int lengh;

        public void GetVer()
        {
            ver = new int[31, 31];
            StreamReader sr = new StreamReader("bigramm.txt");
            string line;
            string[] lines;
            for(int i = 0; i<31; i++)
            {
                line = sr.ReadLine();
                lines = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j<31; j++)
                {
                    ver[i, j] = int.Parse(lines[j]);
                }
            }
            sr.Close();
        }
        public BigrammGen()
        {
            size = syms.Length;
            data = syms.ToCharArray();
        }
        public char getSym()
        {
            return data[random.Next(0, size)];
            lengh++;
        }

        public char Roll(char sym)
        {
            int num = Array.IndexOf(data, sym);
            char result;
            int sum = 0;
            for (int i=0; i<31; i++)
            {
                sum += ver[num, i];
            }
            int[] roulette = new int[sum];
            int j = 0;
            for (int i=0; i<31; i++)
            {
                int k = ver[num, i];
                do
                {
                    if (j == sum)
                        break;
                    roulette[j] = i;
                    j++;
                    k--;
                } while (k > 0);
            }
            int r = random.Next(0, sum);
            result = data[roulette[r]];
            lengh++;
            return result;
        }
    }
    class WordGen
    {
        string[] words;
        int[] ver;
        int size = 100;
        private Random random = new Random();
        int[] roll;
        public int lengh;

        public WordGen()
        {
            StreamReader sr = new StreamReader("слова.txt");
            string line;
            words = new string[size];
            for(int i=0; i<size; i++)
            {
                line = sr.ReadLine();
                words[i] = line;
            }
            sr.Close();
            GetVer();
            GetRoll();
        }

        public void GetVer()
        {
            StreamReader sr2 = new StreamReader("частоты1.txt");
            double chance;
            ver = new int[size];
            for (int i = 0; i < size; i++)
            {
                chance = double.Parse(sr2.ReadLine());
                chance = Math.Round(chance);
                ver[i] = (int)chance;
            }
            sr2.Close();
        }

        public void GetRoll()
        {
            int sum = 0;
            for(int i = 0; i<size; i++)
            {
                sum += ver[i];
            }
            roll = new int[sum];
            int j = 0;
            for (int i=0; i<size; i++)
            {
                int k = ver[i];
                do
                {
                    if (j == sum)
                        break;
                    roll[j] = i;
                    j++;
                    k--;
                } while (k > 0);
            }
        }

        public string Roll()
        {
            int range = roll.Length;
            int r = random.Next(0, range);
            
            return words[roll[r]];
        }
        public string GetWord()
        {
            string word = Roll();
            lengh++;
            return word;
        }
    }

    class WordBigramm
    {
        string[] words1;
        string[] words2;
        int[,] ver;
        int size1 = 40;
        int size2 = 53;
        private Random random = new Random();
        public int lengh;
        public WordBigramm()
        {
            StreamReader sr = new StreamReader("words2.txt");
            string line;
            words2 = new string[size2];
            for (int i = 0; i < size2; i++)
            {
                line = sr.ReadLine();
                words2[i] = line;
            }
            sr.Close();
            StreamReader sr1 = new StreamReader("words1.txt");
            words1 = new string[size1];
            for (int i = 0; i < size1; i++)
            {
                line = sr1.ReadLine();
                words1[i] = line;
            }
            sr.Close();
            sr1.Close();
            GetVer();
        }
        public void GetVer()
        {
            ver = new int[size2, size1];
            StreamReader sr = new StreamReader("ver.txt");
            string line;
            string[] lines;
            for (int i = 0; i < size2; i++)
            {
                line = sr.ReadLine();
                lines = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < size1; j++)
                {
                    ver[i, j] = int.Parse(lines[j]);
                }
            }
            sr.Close();
        }
        public int Find(string[] words, string fword)
        {
            int rez = -1;
            string word;
            for(int i=0; i<words.Length; i++)
            {
                word = words[i];
                if (word == fword)
                {
                    rez = i;
                }
            }
            return rez;
        }
        public string Roll(int num)
        {
            string result;
            int sum = 0;
            for (int i = 0; i < size2; i++)
            {
                sum += ver[i, num];
            }
            int[] roulette = new int[sum];
            int j = 0;
            for (int i = 0; i < size2; i++)
            {
                int k = ver[i, num];
                do
                {
                    if (j == sum)
                        break;
                    roulette[j] = i;
                    j++;
                    k--;
                } while (k > 0);
            }
            int r = random.Next(0, sum);
            result = words2[roulette[r]];
            return result;
        }

        public string GetWord()
        {
            string result;
            int r = random.Next(0, size1);
            result = words1[r];
            return result;
        }
        public string NextWord(string word)
        {
            string result;
            int num = Find(words1, word);
            if (num == -1)
            {
                result = GetWord();
            }
            else
            {
                result = Roll(num);
            }
            lengh++;
            return result;
        }
    }
    class Program
    {
        static public int CountWords(string S, string S0)
        {
            string[] temp = S0.Split(new[] { S }, StringSplitOptions.None);
            return temp.Length - 1;
        }

        static void Main(string[] args)
        {
            StreamWriter sw1 = new StreamWriter("bigramm_generator.txt");
            StreamWriter sw2 = new StreamWriter("word_generator.txt");
            StreamWriter sw3 = new StreamWriter("words_generator.txt");
            BigrammGen g = new BigrammGen();
            g.GetVer();
            char ch = g.getSym();
            sw1.Write(ch);
            for (int i=0; i<1000; i++)
            {
                ch = g.Roll(ch);
                sw1.Write(ch);
            }
            sw1.Close();
            WordGen gen = new WordGen();
            for (int i = 0; i < 1000; i++)
            {
                string word = gen.GetWord();
                sw2.Write(word);
                sw2.Write(" ");
            }
            sw2.Close();
            WordBigramm gen2 = new WordBigramm();
            string word2 = gen2.GetWord();
            sw3.Write(word2 + " ");
            for (int i = 0; i < 1000; i++)
            {
                word2 = gen2.NextWord(word2);
                sw3.Write(word2 + " ");
            }
            sw3.Close();
        }
    }
}
