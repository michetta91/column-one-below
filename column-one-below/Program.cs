// See https://aka.ms/new-console-template for more information
using column_one_below;

Console.Write("Insert path of the file to elaborate: ");
var filePath = Console.ReadLine();
Console.WriteLine();
if (filePath is null) return;
if (!File.Exists(filePath))
{
    Console.WriteLine($"File [{filePath}] not found");
    
    return;
}

var fileImporter = new FileImporter(filePath);
Console.WriteLine($"Headers are: {fileImporter.HeaderMap}");
Console.WriteLine("Insert column indeces to merge, separated by ',' or column range separated by '-': ");
var columnIndeces = Console.ReadLine();
Console.WriteLine();
if (columnIndeces is null) return;

var newColumn = fileImporter.MergeValues(columnIndeces);

Console.Write("Insert path of the output file: ");
var outputFilePath = Console.ReadLine();
Console.WriteLine();
if (outputFilePath is null) return;
if (File.Exists(outputFilePath)) File.Delete(outputFilePath);
File.WriteAllLines(outputFilePath, newColumn);
Console.WriteLine($"Column exported to {outputFilePath}");