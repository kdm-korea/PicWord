using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    class Program {
        public static void Main(string[] args) {
            string word = "apple";
            char[] fstWord = null, alphabat = null;

            if (CompareWord(word, alphabat)) {
                DelFstWord(word, ref fstWord);
                Console.Write(word);
            }
        }



        private static string DelFstWord(string word, ref char[] fstWord) {
            fstWord = word.Substring(0, 1).ToCharArray();
            word = word.Substring(1, word.Length - 1);
            return word;
        }

        private static bool CompareWord(string word, char[] alphabat) {
            //들어온 단어을 비교
            if (alphabat.Equals(word[0])) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
