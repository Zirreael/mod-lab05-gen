using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using generator;
namespace NET
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            StreamWriter sw1 = new StreamWriter("bigramm_generator.txt");
            BigrammGen g = new BigrammGen();
            g.GetVer();
            char ch = g.getSym();
            sw1.Write(ch);
            for (int i = 0; i < 1000; i++)
            {
                ch = g.Roll(ch);
                sw1.Write(ch);
            }
            sw1.Close();
            int res = g.lengh;
            Assert.AreEqual(res, 1000);
        }
        [TestMethod]
        public void TestMethod2()
        {
            StreamWriter sw1 = new StreamWriter("bigramm_generator.txt");
            BigrammGen g = new BigrammGen();
            g.GetVer();
            char ch = g.getSym();
            sw1.Write(ch);
            for (int i = 0; i < 1000; i++)
            {
                ch = g.Roll(ch);
                sw1.Write(ch);
            }
            sw1.Close();
            StreamReader sr1 = new StreamReader("bigramm_generator.txt");
            string line = sr1.ReadToEnd();
            int res;
            if (line.Contains("аааа"))
            {
                res = -1;
            }
            else
            {
                res = 0;
            }
            sr1.Close();
            Assert.AreEqual(res, 0);
        }   
        [TestMethod]
        public void TestMethod3()
        {
            WordGen gen = new WordGen();
            for (int i = 0; i < 1000; i++)
            {
                string word = gen.GetWord();
                sw2.Write(word);
                sw2.Write(" ");
            }
            sw2.Close();
            int res = gen.lengh;
            Assert.AreEqual(res, 1000);
        }  
        [TestMethod]
        public void TestMethod4()
        {
            WordGen gen = new WordGen();
            for (int i = 0; i < 1000; i++)
            {
                string word = gen.GetWord();
                sw2.Write(word);
                sw2.Write(" ");
            }
            sw2.Close();
            StreamReader sr2 = new StreamReader("word_generator.txt");
            string line2 = sr2.ReadToEnd();
            int res = CountWords("и", line2);
            sr2.Close();
            Assert.IsTrue(res > 90);
        }  
        [TestMethod]
        public void TestMethod5()
        {
            WordBigramm gen2 = new WordBigramm();
            string word2 = gen2.GetWord();
            sw3.Write(word2 + " ");
            for (int i = 0; i < 1000; i++)
            {
                word2 = gen2.NextWord(word2);
                sw3.Write(word2 + " ");
            }
            sw3.Close();
            int res = gen2.lengh;
            Assert.AreEqual(res, 1000);
        } 
        [TestMethod]
        public void TestMethod6()
        {
            WordBigramm gen2 = new WordBigramm();
            string word2 = gen2.GetWord();
            sw3.Write(word2 + " ");
            for (int i = 0; i < 1000; i++)
            {
                word2 = gen2.NextWord(word2);
                sw3.Write(word2 + " ");
            }
            sw3.Close();
            StreamReader sr3 = new StreamReader("words_generator.txt");
            string line3 = sr3.ReadToEnd();
            int res = CountWords("потому что", line3);
            sr3.Close();
            Assert.AreTrue(res > 3);
        }  
        }
