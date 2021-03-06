﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PR_RNA_02
{


    class Program
    {

        public static string ReplaceBase(string dnaString, char replaceWhat, char replaceWith)
        {
            return dnaString.Replace(replaceWhat, replaceWith);
        }

        static void Main(string[] args)
        {
            string strPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string[] files = Directory.GetFiles(strPath +  @"\Rosalind", "*.txt");
            string[] lines = { "" };
            foreach (string file in files)
            {
                lines = File.ReadAllLines(file);
            }

            Console.WriteLine(ReplaceBase(lines[0], 'T', 'U'));

            Console.ReadLine();
        }
    }
}
