using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string InputText { get; set; }

    public string OutputText { get; set; }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(InputText))
        {
            OutputText = ProcessText(InputText);
        }
    }

    private string ProcessText(string input)
    {
        // ini ngapus spasi dulu biar jadi satu kata biar gampang hitung hurufnya
        string cleanedInput = input.Replace(" ", "");

        // ini hitung jumlah setiap huruf
        Dictionary<char, int> frequency = new Dictionary<char, int>();
        List<char> order = new List<char>();

        foreach (char ch in cleanedInput)
        {
            if (frequency.ContainsKey(ch))
                frequency[ch]++;
            else
            {
                frequency[ch] = 1;
                order.Add(ch); 
            }
        }

        //ini buat ngurutin huruf dengan jumlah terbanyak yang dimunculkan pertama
        var sortedChars = frequency
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => order.IndexOf(kvp.Key))
            .ToList();

        // ini tampungan buat output
        string repeatedChars = "";
        string singleChars = "";

        foreach (var kvp in sortedChars)
        {
            if (kvp.Value > 1)
                repeatedChars += kvp.Key.ToString() + kvp.Value; // ini nampung huruf yang lebih dari satu
            else
                singleChars += kvp.Key; // ini nampung huruf yang cuma muncul sekali
        }

        // ini hasil akhirnya 
        return repeatedChars + " " + singleChars;
    }
}