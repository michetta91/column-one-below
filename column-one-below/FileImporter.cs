using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace column_one_below
{
    public class FileImporter
    {
        private string FilePath { set; get; }
        private Dictionary<string, int>? Headers { set; get; }
        public string HeaderMap { get => GetHeadersMap(); }
        public FileImporter(string filePath)
        {
            FilePath = filePath;
            Headers = ImportHeaders(filePath);
        }

        private Dictionary<string, int>? ImportHeaders(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var counter = 0;
            var line = streamReader.ReadLine();
            var headersArray = line.Split(';');
            var headers = Enumerable.Range(0, headersArray.Length).ToDictionary(x => headersArray[x]);
            return headers;
        }

        public string[] MergeValues(string headersIndeces)
        {
            var indeces = Array.Empty<int>();    
            if (headersIndeces.Contains("-"))
            {
                var limit = headersIndeces.Split('-').ToArray();
                indeces = Enumerable.Range(int.Parse(limit[0]), int.Parse(limit[1]) - int.Parse(limit[0])+1).ToArray();
            }
            else
            {
                indeces = headersIndeces.Split(',').Select(index => int.Parse(index)).ToArray();
            }

            var columnValues = new List<string>[indeces.Length];
            for (int i = 0; i < indeces.Length; i++)
            {
                columnValues[i] = new List<string>();
            }

            using var fileStream = File.OpenRead(FilePath);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            var line = streamReader.ReadLine();
            while (true)
            {
                line = streamReader.ReadLine();
                if (string.IsNullOrEmpty(line)) break;                
                var fields = line.Split(';').ToArray();

                for (int i = 0; i< columnValues.Length; i++)
                {
                    columnValues[i].Add(fields[indeces[i]-1]);
                }
            }
            var newColumn = new List<string>();
            for (int i = 0; i < columnValues.Length; i++)
            {
                newColumn.AddRange(columnValues[i]);
            }
            return newColumn.ToArray();
        }

        private string GetHeadersMap()
        {
            var strings = new List<string>();
            foreach (var header in Headers)
            {
                strings.Add($"{header.Value + 1}: [{header.Key}]");
            }  
            return string.Join("; ", strings);    
        }

    }
}
