using System;
using System.IO;

namespace JsonReader
{
    /// <summary>
    /// A program that reads a JSON file and generate a HTML table.
    /// Sample usage:
    ///     JsonReader.exe Json_file_path Output_file_path
    /// Example:
    ///     JsonReader.exe D:\Tester\InterviewMaterial\Sample.json D:\Tester\InterviewMaterial\JsonReader\JsonReader\bin\Debug\net5.0\test.html    
    /// </summary>
    class Program
    {
        /// <summary>
        /// A varialbe that holds the JSON data file path.
        /// </summary>
        static string _samplePath = Environment.CurrentDirectory + "\\sample.json";
        /// <summary>
        /// A variable that holds the output file path.
        /// </summary>
        static string _outputPath = Environment.CurrentDirectory + "\\test.html";

        /// <summary>
        /// The main function that reads JSON data file and writes output data file.
        /// </summary>
        /// <param name="args">Contains user arguments. If there is no arguments provided, default value is used.</param>
        static void Main(string[] args)
        {
            try
            {
                if(args.Length == 2)
                {
                    _samplePath = args[0];
                    _outputPath = args[1];
                }
                else
                {
                    Console.WriteLine("Json file processor that generates HTML table");
                    Console.WriteLine("Usage:");
                    Console.WriteLine("     JsonReader.exe Json_file_path Output_file_path");
                    Console.WriteLine();
                    Console.WriteLine("     Json_fle_path = data file path.");
                    Console.WriteLine("     Output_file_path = HTML file output path.");
                }

                string jsonContent = File.ReadAllText(_samplePath);
                JsonToTable converter = new JsonToTable(jsonContent);
                string html_table = converter.ConvertToTable();

                File.WriteAllText(_outputPath, html_table);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine($"Done!!! HTML {_outputPath} is generated.");
        }
    }
}
